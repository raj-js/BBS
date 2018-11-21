using Autofac;
using Autofac.Extensions.DependencyInjection;
using EDoc2.FAQ.EventBus;
using EDoc2.FAQ.EventBus.Abstractions;
using EDoc2.FAQ.EventBus.RabbitMQ;
using EDoc2.FAQ.Notification.Mail;
using EDoc2.FAQ.Web.Data;
using EDoc2.FAQ.Web.Data.Identity;
using EDoc2.FAQ.Web.IntegrationEvents;
using EDoc2.FAQ.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using RabbitMQ.Client;
using System;

namespace EDoc2.FAQ.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connectionString = Configuration.GetConnectionString("AppDbContext");
            services.AddDbContext<AppDbContext>(
                    b => b.UseSqlServer(connectionString)
                        .UseLazyLoadingProxies())
                .AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //services.AddDbContext<AppIdentityContext>(
            //        b => b.UseSqlServer(Configuration.GetConnectionString("AppIdentityContext"))
            //            .UseLazyLoadingProxies())
            //    .AddIdentity<User, Role>()
            //    .AddEntityFrameworkStores<AppIdentityContext>()
            //    .AddDefaultTokenProviders();

            //services.AddDbContext<CommunityContext>(
            //        b => b.UseSqlServer(Configuration.GetConnectionString("CommunityContext"))
            //            .UseLazyLoadingProxies());

            var identityOptions = Configuration.GetSection("IdentityOptions");
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //英文数字字母的组合
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.User.AllowedUserNameCharacters = identityOptions.GetSection("User")["AllowedUserNameCharacters"];
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "EDoc2.FAQ.Identity";
                options.Cookie.HttpOnly = true;

                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            RegisterApplicationService(services);
            RegisterEventBus(services);
            ConfigureMail(services);

            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            ConfigureEventBus(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            loggerFactory.AddNLog();
            env.ConfigureNLog("NLog.config");


            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }

        private void RegisterApplicationService(IServiceCollection services)
        {
            services.AddTransient<IArticleManager, ArticleManager>();
            services.AddTransient<IUserManagerExt, UserManagerExt>();
            services.AddTransient<ISystemManager, SystemManager>();
            services.AddTransient<IMailService, MailService>();
        }

        private void RegisterEventBus(IServiceCollection services)
        {
            var eventBusConfig = Configuration.GetSection("EventBus");
            var retryCount = eventBusConfig.GetValue<int>("RetryCount");
            var hostName = eventBusConfig.GetValue<string>("HostName");
            var userName = eventBusConfig.GetValue<string>("UserName");
            var password = eventBusConfig.GetValue<string>("Password");
            var port = eventBusConfig.GetValue<int>("Port");

            services.AddSingleton<IRabbitMQConnection>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DefaultRabbitMQConnection>>();
                var factory = new ConnectionFactory
                {
                    HostName = hostName,
                    UserName = userName,
                    Password = password,
                    Port = port
                };
                return new DefaultRabbitMQConnection(factory, logger, retryCount);
            });

            services.AddSingleton<IEventBus>(provider =>
            {
                var subscriptionClientName = eventBusConfig["SubscriptionClientName"];
                var rabbitMQConnection = provider.GetRequiredService<IRabbitMQConnection>();
                var iLifetimeScope = provider.GetRequiredService<ILifetimeScope>();
                var logger = provider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = provider.GetRequiredService<IEventBusSubscriptionsManager>();
                return new EventBusRabbitMQ(rabbitMQConnection, eventBusSubscriptionsManager, logger, iLifetimeScope, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddTransient<MailSendEventHandler>();
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<MailSendEvent, MailSendEventHandler>();
        }

        private void ConfigureMail(IServiceCollection services)
        {
            var mailConfig = Configuration.GetSection("Mail");
            var retryCount = mailConfig.GetValue<int>("RetryCount");
            var host = mailConfig.GetValue<string>("Host");
            var port = mailConfig.GetValue<int>("Port");
            var useSsl = mailConfig.GetValue<bool>("UseSsl");
            var userName = mailConfig.GetValue<string>("UserName");
            var password = mailConfig.GetValue<string>("Password");

            services.AddTransient<IMailSender>(provider =>
            {
                var settings = new MailClientSetting
                {
                    Host = host,
                    Port = port,
                    UseSSL = useSsl,
                    UserName = userName,
                    Password = password
                };
                var logger = provider.GetRequiredService<ILogger<SMTPSender>>();
                return new SMTPSender(settings, logger, retryCount);
            });
        }
    }
}
