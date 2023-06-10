using E_Website.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace E_Website.Models.Data
{
    public class SeedData
    {
        //public static void SeedInitData(IApplicationBuilder applicationBuilder)
        //{
        //    using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //    {
        //        var context = serviceScope.ServiceProvider.GetService<ModelContext>();
        //        context.Database.EnsureCreated();
        //        if(!context..Any())
        //        {

        //        }
        //    }
        //}
        public static async Task SeedInitDataAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.Seller))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Seller));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<applicationUser>>();

                //super admin
                string SuperAdminEmail = "superadmin@yahoo.com";
                var SuperAdminUser = await userManager.FindByEmailAsync(SuperAdminEmail);
                if(SuperAdminUser == null)
                {
                    var newSuperAdminUser = new applicationUser()
                    {
                        firstName = "mutaz",
                        lastName = "alntsha",
                        UserName = "superAdmin",
                        Email = SuperAdminEmail,
                        EmailConfirmed = true,
                        PhoneNumber = "0785668985",
                    };
                    await userManager.CreateAsync(newSuperAdminUser, "super12345");
                    await userManager.AddToRoleAsync(newSuperAdminUser, UserRoles.SuperAdmin);
                }

                //admin
                string adminEmail = "admin@yahoo.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new applicationUser()
                    {
                        firstName = "mutaz",
                        lastName = "alntsha",
                        UserName = "admin",
                        Email = adminEmail,
                        EmailConfirmed = true,
                        PhoneNumber = "0785668985"
                    };
                    await userManager.CreateAsync(newAdminUser, "admin12345");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                //user
                string userEmail = "user@yahoo.com";
                var user = await userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    var newUser = new applicationUser()
                    {
                        firstName = "mutaz",
                        lastName = "alntsha",
                        UserName = "user",
                        Email = userEmail,
                        EmailConfirmed = false,
                        PhoneNumber = "0785668985"
                    };
                    await userManager.CreateAsync(newUser, "user12345");
                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }


    }
}
