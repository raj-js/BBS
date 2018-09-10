using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace EDoc2.UAS
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("AccountApi", "Account Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "MVC",
                    ClientName = "Mvc Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    RequireConsent = false,                                                     //如果不需要显示是否同意授权， 则设置 false

                    RedirectUris = { "http://localhost:5000/signin-oidc" },                     //登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:5000/signout-callback-oidc" }, //注销登录后返回的客户端地址

                    AllowedScopes =                                                             //允许客户端获取的资源范围
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "AccountApi"
                    },
                    AllowOfflineAccess = true
                }
            };
        }
    }
}
