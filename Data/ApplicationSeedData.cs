using Microsoft.AspNetCore.Identity;
using NBD_TractionFive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Data
{
    public static class ApplicationSeedData
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Management.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Designer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Sales.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Staff.ToString()));
        }
    }
}
