using RazorPagesProject.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
/*
 * veritabaný olursa AddTransient kullanman daha mantýklý olur.
    AddSingleton Örneði: SettingsService tüm uygulama boyunca ayný nesne olarak kalýr ve her yerden ayný veriyi döndürür.
    AddTransient Örneði: RandomNumberService her çaðrýldýðýnda yeni bir rastgele sayý döndürür çünkü her istekte yeni bir nesne oluþturulur. 
 */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
