var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//mvc þablonu projeye tanýtýlmýþ oldu.
//controller'larýn view'lerle iliþki olmasý gerekiyor.
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
//app.MapGet("/abc", () => "Merhaba Dünya");

// {controller=Home}/{action=Index}/id?
//app.MapDefaultControllerRoute();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
