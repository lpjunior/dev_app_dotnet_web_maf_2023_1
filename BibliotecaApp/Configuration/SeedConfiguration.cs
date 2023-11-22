using BibliotecaApp.Models;
using BibliotecaApp.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace BibliotecaApp.Configuration
{
    public static class SeedConfiguration
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrador")) {
                await roleManager.CreateAsync(new IdentityRole("Administrador"));
            }

            if (!await roleManager.RoleExistsAsync("Cliente"))
            {
                await roleManager.CreateAsync(new IdentityRole("Cliente"));
            }
        }

        public static async Task SeedUsers(UserManager<Usuario> userManager)
        {
            if(await userManager.FindByEmailAsync("admin@senac.com") == null)
            {
                var adminUser = new Usuario
                {
                    UserName = "Administrador do Sistema",
                    Email = "admin@senac.com",
                    Type = UsuarioType.Administrador,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Password123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrador");
                }
            }
        }
    }
}
