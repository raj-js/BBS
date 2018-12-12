using Autofac;
using Autofac.Extensions.DependencyInjection;
using EDoc2.FAQ.Api.Infrastructure;
using EDoc2.FAQ.Api.Infrastructure.Middlewares;
using EDoc2.FAQ.Api.Infrastructure.Modules;
using EDoc2.FAQ.Core.Application.Settings;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using NLog.Web;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

                setting.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
                setting.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT token", new SwaggerSecurityScheme
                {
                    Type = SwaggerSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = SwaggerSecurityApiKeyLocation.Header,
                    Description = "拷贝 'Bearer ' + 有效的 'JWT'"
                }));
            }).AddOpenApiDocument(doc => { doc.DocumentName = "openApi"; });

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
            })
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<CommunityContext>()
            .AddDefaultTokenProviders();

            services.Configure<JwtSetting>(Configuration.GetSection(nameof(JwtSetting)));
            var jwt = new JwtSetting();
            Configuration.Bind(nameof(JwtSetting), jwt);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Query.ContainsKey("access_token"))
                                context.Token = context.Request.Query["access_token"];

                            return Task.CompletedTask;
                        }
                    };

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.Secret)),

                        ValidateIssuer = true,
                        ValidIssuer = jwt.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwt.Audience,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(30)
                    };
                });

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

                options.Tokens.AuthenticatorTokenProvider = "";
            });

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Title = "EDoc2.FAQ.Community";
            //    options.Cookie.HttpOnly = true;

            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //    options.SlidingExpiration = true;
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //});

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

            app.UseCors(b =>
            {
                b.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.UseMvc();

            app.ConfigureEventBus();
        }
    }
}
