using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VehicleRunsheetMBAProj
{
    /// <summary>
    /// A Class containing logic required to ensure configuration of Admin user, runs at startup.
    /// </summary>
    public static class AdminStartup
    {
        /// <summary>
        /// An <see cref="IServiceCollection"/> extension that encapsulates the logic required to create user roles
        /// at startup. 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="provider"></param>
        public static void AddAdmin(this IServiceCollection service, IServiceProvider provider)
        {
            CreateRoles(provider).GetAwaiter().GetResult();
        }

        private static async Task CreateRoles(IServiceProvider service)
        {
            const string Admin = "Admin";
            const string Manager = "Manager";
            const string AppUser = "User";
            

            string[] _roleNames = { Admin, Manager, AppUser };

            var _roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = service.GetRequiredService<UserManager<IdentityUser>>();
            //var _settings = service.GetRequiredService<Settings>();

            foreach (var role in _roleNames)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var user = await _userManager.FindByEmailAsync("test@test.com");

            if (user == null)
            {
                var admin = new IdentityUser()
                {
                    UserName = Admin,
                    Email = "test@test.com"
                };

                string adminPassword = "Admin_1234";
                var createUser = await _userManager.CreateAsync(admin, adminPassword);

                if (createUser.Succeeded)
                {
                    var createdUser = await _userManager.FindByEmailAsync("test@test.com");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(createdUser);
                    var result = await _userManager.ConfirmEmailAsync(createdUser, code);
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Email Could Not Be Confirmed for {createdUser.UserName}");
                    }

                    await _userManager.AddToRoleAsync(createdUser, Admin);
                    await _userManager.AddClaimAsync(createdUser, new Claim("Id", admin.Id));
                    await _userManager.AddClaimAsync(createdUser, new Claim("Role", Admin));
                    await _userManager.AddClaimAsync(createdUser, new Claim("UserName", Admin));
                }
            }
        }
    }
}