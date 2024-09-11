using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options => {
    var config=builder.Configuration;
    var connectionString = config.GetConnectionString("sql_connection");
    options.UseSqlServer(connectionString);//mssql mysqle çevirebilirsin
});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();

var app = builder.Build();
                              //app aracılığı ile Services containerına ulaşır ve içerisindeki context bilgisini alır
SeedData.TestVerileriniDoldur(app); //migration oluşturduktan sonra datbase update yapmana gerek yok,
                                    //SeedData.TestVerileriniDoldur(app); çalıştırıyor

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/{url}",
    defaults:new { controller = "Posts", action = "Details" } 
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}");

app.Run();
