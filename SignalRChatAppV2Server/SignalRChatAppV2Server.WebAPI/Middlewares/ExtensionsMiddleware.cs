using Microsoft.AspNetCore.Identity;
using SignalRChatAppV2Server.WebAPI.Models;

namespace SignalRChatAppV2Server.WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (!userManager.Users.Any(p => p.Email == "eren@gmail.com"))
            {
                AppUser user = new()
                {
                    FirstName = "İhsan",
                    LastName = "Delibaş",
                    UserName = "ied",
                    Email = "eren@gmail.com",
                    IsDeleted = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };

                userManager.CreateAsync(user, "Password123*").Wait();
            }
        }
    }
}
