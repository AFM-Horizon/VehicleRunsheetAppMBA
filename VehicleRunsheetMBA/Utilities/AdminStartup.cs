﻿using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using VehicleRunsheetMBA.Configuration;

namespace VehicleRunsheetMBAProj
{
    public static class AdminStartup
    {
        public static async Task<IServiceCollection> AddAdmin(this IServiceCollection service, IServiceProvider provider)
        {
            await CreateRoles(provider);

            return service;
        }

        private static async Task CreateRoles(IServiceProvider service)
        {
            const string Admin = "Admin";
            const string Manager = "Manager";
            const string AppUser = "User";

            string[] _roleNames = { Admin, Manager, AppUser };

            var _roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = service.GetRequiredService<UserManager<IdentityUser>>();
            var _settings = service.GetRequiredService<Settings>();

            foreach (var role in _roleNames)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var user = await _userManager.FindByEmailAsync(_settings.AdminEmail);

            if (user == null)
            {
                var admin = new IdentityUser()
                {
                    UserName = Admin,
                    Email = _settings.AdminEmail
                };

                string adminPassword = _settings.AdminPassword;
                var createUser = await _userManager.CreateAsync(admin, adminPassword);

                if (createUser.Succeeded)
                {
                    var createdUser = await _userManager.FindByEmailAsync(_settings.AdminEmail);
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