using Autofac;
using Autofac.Extensions.DependencyInjection;
using EDoc2.FAQ.Api.Infrastructure;
using EDoc2.FAQ.Api.Infrastructure.Modules;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Infrastructure;
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
using System;
using System.Reflection;
using EDoc2.FAQ.Api.Infrastructure.Middlewares;

namespace EDoc2.FAQ.Api
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

            //swagger ui
            services.AddSwaggerDocument(setting =>
            {
                setting.Title = "EDoc2问答社区Api文档";
                setting.DocumentName = "v1";
                setting.Description = "EDoc2问答社区Api文档";
            });

            //db context
            services.AddDbContext<CommunityContext>(options =>
            {
                options.UseSqlServer(
                        Configuration["ConnectionString"],
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        }).UseLazyLoadingProxies();

                //options.ConfigureWarnings(warning => 
                //{
                //    warning.Ignore(CoreEventId.DetachedLazyLoadingWarning);
                //});
            })
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<CommunityContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "EDoc2.FAQ.Community";
                options.Cookie.HttpOnly = true;

                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddEventBus(Configuration);
            services.UseMailSender(Configuration);

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var container = new ContainerBuilder();
            container.Populate(services);

            //container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule());

            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            loggerFactory.AddNLog();
            env.ConfigureNLog("NLog.config");

            app.UseSwagger()
               .UseSwaggerUi3();

            app.UseAuthentication();

            app.UseExceptionsHandler();

            app.UseCors(b => b.AllowAnyOrigin());

            app.UseMvc();

            app.ConfigureEventBus();
        }
    }
}
