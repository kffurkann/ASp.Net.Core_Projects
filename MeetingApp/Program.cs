var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//mvc �ablonu projeye tan�t�lm�� oldu.
//controller'lar�n view'lerle ili�ki olmas� gerekiyor.
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
//app.MapGet("/abc", () => "Merhaba D�nya");

// {controller=Home}/{action=Index}/id?
//app.MapDefaultControllerRoute();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
