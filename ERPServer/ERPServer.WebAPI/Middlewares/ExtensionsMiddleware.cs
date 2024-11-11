using ERPServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ERPServer.WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication application)
    {
        using (var scoped = application.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Melih Can",
                    LastName = "Akgüneş",
                    EmailConfirmed = true
                };

                userManager.CreateAsync(user, "asd123").Wait();
            }
        }
    }
}
