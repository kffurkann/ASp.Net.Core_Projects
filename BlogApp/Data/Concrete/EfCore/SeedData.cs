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
                        new Tag { Text = "web programlama", Url = "web-programlama", Color = TagColors.warning },
                        new Tag { Text = "backend", Url = "backend", Color = TagColors.info },
                        new Tag { Text = "frontend", Url = "frontend", Color = TagColors.success },
                        new Tag { Text = "fullstack", Url = "fullstack", Color = TagColors.secondary },
                        new Tag { Text = "php", Url = "php", Color = TagColors.primary }
                    );   
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "sadikturan", Name = "Sadık Turan", Email = "info@sadikturan.com", Password = "123456", Image = "p1.jpg" },
                        new User { UserName = "cinarturan", Name = "Çınar Turan", Email = "info@cinarturan.com", Password = "123456", Image = "p2.jpg" }
                    );
                    context.SaveChanges();
                }
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post
                        {
                            Title = "Asp.net core",
                            Description = "Asp.net core dersleri",
                            Content = "Asp.net core dersleri",
                            Url ="aspnet-core",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(3).ToList(),
                            Image="1.jpg",
                            UserId = 1,  //User = context.Users.FirstOrDefault()
                            Comments = new List<Comment> {
                                new Comment { Text = "iyi bir kurs", PublishedOn = DateTime.Now.AddDays(-20), UserId = 1},//new DateTime()
                                new Comment { Text = "çok faydalandığım bir kurs", PublishedOn = DateTime.Now.AddDays(-10), UserId = 2},
                            }
                        },
                        new Post
                        {
                            Title = "PHP",
                            Description = "Php core dersleri",
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
                            Description = "Django dersleri",
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
