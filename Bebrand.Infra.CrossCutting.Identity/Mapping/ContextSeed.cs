using Bebrand.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bebrand.Domain.Enums;
namespace Bebrand.Infra.CrossCutting.Identity.Mapping
{
    public static class ContextSeed
    {
      
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Salesdirector.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Teamleader.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Teammember.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Hr.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> _signInManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "admin@bebrand.tv",
                Email = "admin@bebrand.tv",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                ParentUserId = Guid.NewGuid(),
                Status =Status.Active
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    var Created = await userManager.CreateAsync(defaultUser, "Abc123**");
                    if (Created.Succeeded)
                    {
                        await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                       
                    }
                }

            }

        }

        public static async Task SeedSalesdirectorAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> _signInManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "Salesdirector@bebrand.tv",
                Email = "Salesdirector@bebrand.tv",
                ParentUserId = Guid.NewGuid(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Status = Status.Active
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    var Created = await userManager.CreateAsync(defaultUser, "Abc123**");
                    if (Created.Succeeded)
                    {
                        await userManager.AddToRoleAsync(defaultUser, Roles.Salesdirector.ToString());
                       
                    }
                  
                }

            }

        }

        public static async Task SeedHrAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> _signInManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "hr@bebrand.tv",
                Email = "hr@bebrand.tv",
                ParentUserId = Guid.NewGuid(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Status = Status.Active
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    var Created = await userManager.CreateAsync(defaultUser, "Abc123**");
                    if (Created.Succeeded)
                    {
                        await userManager.AddToRoleAsync(defaultUser, Roles.Hr.ToString());

                    }

                }

            }

        }
        public static async Task SeedTeamleaderAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> _signInManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "Teamleader@bebrand.tv",
                Email = "Teamleader@bebrand.tv",               
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                ParentUserId = Guid.NewGuid(),
                Status = Status.Active
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    var Created = await userManager.CreateAsync(defaultUser, "Abc123**");
                    if (Created.Succeeded)
                    {
                        await userManager.AddToRoleAsync(defaultUser, Roles.Teamleader.ToString());
                        //await _signInManager.SignInAsync(user, true);
                    }


                    //await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());

                }

            }



        }

        public static async Task SeedTeammemberAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> _signInManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "Teammember@bebrand.tv",
                Email = "Teammember@bebrand.tv",
                ParentUserId = Guid.NewGuid(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Status = Status.Active
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    var Created = await userManager.CreateAsync(defaultUser, "Abc123**");
                    if (Created.Succeeded)
                    {
                        await userManager.AddToRoleAsync(defaultUser, Roles.Teammember.ToString());
                        //await _signInManager.SignInAsync(user, true);
                    }
                }

            }



        }
    }
}
