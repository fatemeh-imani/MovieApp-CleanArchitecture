using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Infrastructure.Authentication.Role;

namespace MovieApp.Infrastructure.Authentication.Identity;

internal static class IdentitySeeder
{
    public static async Task SeedAsync(
        IServiceProvider serviceProvider)
    {
        var roleManager =
            serviceProvider.GetRequiredService<
                RoleManager<IdentityRole<Guid>>>();

        var userManager =
            serviceProvider.GetRequiredService<
                UserManager<ApplicationUser>>();

        await SeedRoleAsync(
            roleManager,
            Roles.Admin);

        await SeedRoleAsync(
            roleManager,
            Roles.User);

        const string adminEmail = "admin@movieapp.com";
        const string adminPassword = "Admin123!";
        //fatemehimani.net@gmail.com  Abc12345!

        var admin =
            await userManager.FindByEmailAsync(adminEmail);

        if (admin is not null)
        {
            return;
        }

        admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result =
            await userManager.CreateAsync(
                admin,
                adminPassword);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(
                    ", ",
                    result.Errors.Select(
                        error => error.Description)));
        }

        await userManager.AddToRoleAsync(
            admin,
            Roles.Admin);
    }

    private static async Task SeedRoleAsync(
        RoleManager<IdentityRole<Guid>> roleManager,
        string role)
    {
        if (await roleManager.RoleExistsAsync(role))
        {
            return;
        }

        var result =
            await roleManager.CreateAsync(
                new IdentityRole<Guid>(role));

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(
                    ", ",
                    result.Errors.Select(
                        error => error.Description)));
        }
    }
}