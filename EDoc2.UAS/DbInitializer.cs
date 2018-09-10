using EDoc2.UAS.Models;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;

namespace EDoc2.UAS
{
    public class DbInitializer
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var provider = serviceScope.ServiceProvider;
                provider.GetRequiredService<PersistedGrantDbContext>()
                    .Database.Migrate();

                var context = provider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                //add some users
                var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
                var admin = userManager.FindByNameAsync("admin").Result;
                if (admin != null) return;

                admin = new ApplicationUser
                {
                    UserName = "admin"
                };
                var result = userManager.CreateAsync(admin, "Admin@123").Result;
                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Description);

                result = userManager.AddClaimsAsync(admin, new[]{
                    new Claim(JwtClaimTypes.Subject, admin.Id), 
                    new Claim(JwtClaimTypes.Name, "Administrator"),
                    new Claim(JwtClaimTypes.GivenName, "Admin"),
                    new Claim(JwtClaimTypes.FamilyName, "Admin"),
                    new Claim(JwtClaimTypes.Email, "admin@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://admin.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }).Result;

                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Description);

                Console.WriteLine("admin created");
            }
        }
    }
}
