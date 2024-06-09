using AuthServer.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Security.Claims;

namespace AuthServer.Extensions
{
    public class SeedUserData
    {
        public static void InsertSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();
            using(var serviceProvider=services.BuildServiceProvider())
            {
                using(var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    CreateUser(scope,"usman","tahir", "rawalpindi",Guid.NewGuid(), "usmanPass","Admin","utahir604@gmail.com");
                    CreateUser(scope, "umer", "abc", "rawalpindi",Guid.NewGuid(), "usmanPass","Visitor","t40196@gmail.com");
                }
            }
        }

        private static void CreateUser(IServiceScope scope, string firstName, string lastName, string address, Guid id, string password, string role, string email)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var user=userManager.FindByEmailAsync(email).Result;
            if(user is null)
            {
                user=new User()
                {
                    UserName=email,
                    Email=email,
                    FirstName=firstName,
                    LastName=lastName,
                    Address=address,
                    Id=id.ToString(),
                    EmailConfirmed=true
                };
                var result =  userManager.CreateAsync(user,password).Result;
                CheckResult(result);
                result= userManager.AddToRoleAsync(user,role).Result;
                CheckResult(result);
                result = userManager.AddClaimsAsync(user, new Claim[]
                {
                    new Claim(JwtClaimTypes.GivenName,user.FirstName),
                    new Claim(JwtClaimTypes.FamilyName,user.LastName),
                    new Claim(JwtClaimTypes.Role,role),
                    new Claim(JwtClaimTypes.Address,user.Address)
                }).Result;
                CheckResult(result);
            }
        }

        private static void CheckResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}
