using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models
{
    public class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "Admin_123";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();
            //program.cs deki servise ulaşılır
            if (context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(adminUser);

            if (user == null)
            {
                user = new AppUser//hazır kütüphane
                {                       //hazır bilgiler
                    FullName = "Furkan Kılıç",
                    UserName = adminUser,
                    Email = "admin@furkankilic.com",
                    PhoneNumber = "4444444"
                };

                await userManager.CreateAsync(user, adminPassword);//parola hash ile gizlenmiştir.
            }
        }
    }
}
