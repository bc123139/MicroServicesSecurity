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
            new IdentityResources.Profile()
        };

        public static IEnumerable<Client> Clients() => new List<Client> {
            new Client
            {
                ClientId = "first-client",
                ClientSecrets =new []{ new Secret("mysecret".Sha512())},
                AllowedGrantTypes=GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId}
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
                    new Claim("last_name","tahir")
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
                    new Claim("last_name","abc")
                }
            }
        };
    }
}
