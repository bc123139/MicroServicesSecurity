using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthServer.Config
{
    public class MemoryConfig
    {
        public static IEnumerable<IdentityResource> IdentityResourecs() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Address(),
            new IdentityResource("roles","User role(s)",new List<string>{"role"})
        };

        public static IEnumerable<Client> Clients() => new List<Client> {
            new Client
            {
                ClientId = "first-client",
                ClientSecrets =new []{ new Secret("mysecret".Sha512())},
                AllowedGrantTypes=GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,"jobsApi.scope"}
            },
            new Client
            {
                ClientId ="mvc-client",
                ClientName = "MvcClient",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris=new List<string>{ "https://localhost:5201/signin-oidc" },
                AllowedScopes={IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Address,"roles","jobsApi.scope"},
                ClientSecrets={new Secret("mvcclientsecret".Sha512()) },
                RequirePkce=true,
                RequireConsent=true,
                PostLogoutRedirectUris=new List<string>{ "https://localhost:5201/signout-oidc" }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes() => new List<ApiScope>
        {
            new ApiScope("jobsApi.scope","Jobs Api")
        };

        public static IEnumerable<ApiResource> ApiResources() => new List<ApiResource>
        {
            new ApiResource("jobsApi","Jobs Api")
            {
                Scopes={"jobsApi.scope"},
                UserClaims =new List<string>{"role"}
            }
        };

        public static List<TestUser> TestUsers() => new List<TestUser>
        {
            new TestUser
            {
                SubjectId ="abc1",
                Username="usman",
                Password="usmanPass",
                Claims=new List<Claim>
                {
                    new Claim("given_name","usman"),
                    new Claim("last_name","tahir"),
                    new Claim("address","rawalpindi"),
                    new Claim("role","Admin")
                }
            },
            new TestUser
            {
                SubjectId ="abc2",
                Username="umer",
                Password="umerPass",
                Claims=new List<Claim>
                {
                    new Claim("given_name","umer"),
                    new Claim("last_name","abc"),
                    new Claim("address","rawalpindi"),
                    new Claim("role","Editor")
                }
            }
        };
    }
}
