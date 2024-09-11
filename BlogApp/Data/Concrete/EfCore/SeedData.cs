using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace BlogApp.Data.Concrete.EfCore
{
    public class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())// bir ya da birden fazla uygulanmamış migration varsa
                {
                    context.Database.Migrate();//uygulama her çalıştığında database sıfırdan oluşturulsun.
                }
                if (!context.Tags.Any())//bir ya da birden fazla
                {
                    context.Tags.AddRange(
                        new Tag {Text = "web programlama" ,Url="web-programlama"},
                        new Tag {Text = "backend", Url = "backend" },
                        new Tag {Text = "frontend", Url = "frontend" },
                        new Tag {Text = "fullstack", Url = "fullstack" },
                        new Tag {Text = "php", Url = "php" }
                    );   
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "sadikturan" }, 
                        new User { UserName = "ahmetyilmaz" }
                    );
                    context.SaveChanges();
                }
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post
                        {
                            Title = "Asp.net core",
                            Content = "Asp.net core dersleri",
                            Url ="aspnet-core",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(3).ToList(),
                            Image="1.jpg",
                            UserId = 1  //User = context.Users.FirstOrDefault()
                        },
                        new Post
                        {
                            Title = "PHP",
                            Content = "PHP dersleri",
                            Url = "php",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(2).ToList(),
                            Image = "2.jpg",
                            UserId = 1  //User = context.Users.FirstOrDefault()
                        },
                        new Post
                        {
                            Title = "DJANGO",
                            Content = "DJANGO dersleri",
                            Url = "django",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(4).ToList(),
                            Image = "3.jpg",
                            UserId = 2  //User = context.Users.FirstOrDefault()
                        },
                        new Post
                        {
                            Title = "Angular",
                            Content = "Angular dersleri",
                            Url = "angular",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(4).ToList(),
                            Image = "2.jpg",
                            UserId = 1  //User = context.Users.FirstOrDefault()
                        },
                        new Post
                        {
                            Title = "React",
                            Content = "React dersleri",
                            Url = "react",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(2).ToList(),
                            Image = "1.jpg",
                            UserId = 1  //User = context.Users.FirstOrDefault()
                        },
                        new Post
                        {
                            Title = "Sql",
                            Content = "Sql dersleri",
                            Url = "sql",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-15),
                            Tags = context.Tags.Take(3).ToList(),
                            Image = "3.jpg",
                            UserId = 2  //User = context.Users.FirstOrDefault()
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
