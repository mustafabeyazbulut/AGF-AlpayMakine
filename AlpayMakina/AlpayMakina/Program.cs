using AlpayMakina.Models.DapperContect;
using AlpayMakina.Repositories.CategoryRepositories;
using AlpayMakina.Repositories.CompanyInformationRepositories;
using AlpayMakina.Repositories.ProductRepositories;
using AlpayMakina.Repositories.SliderRepositories;
using AlpayMakina.Repositories.SocialMediaRepositories;
using AlpayMakina.Repositories.SubCategoryRepositories;
using AlpayMakina.Repositories.UserRepositories;
using AlpayMakina.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "AGFCookie";
    options.LoginPath= "/Login/Index";
    options.AccessDeniedPath = "/Login/Index";
});

builder.Services.AddTransient<Context>();
builder.Services.AddTransient<ISocialMediaRepository, SocialMediaRepository>();
builder.Services.AddTransient<ICompanyInformationRepository, CompanyInformationRepository>();
builder.Services.AddTransient<ISliderRepository, SliderRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddScoped<ImageOperations>();
builder.Services.AddScoped<HashHelper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Index");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseStatusCodePagesWithReExecute("/Error/Index", "?statusCode={0}");
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
////pattern: "{area=UI}/{controller=Home}/{action=Index}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"

//    );
//});
app.UseStatusCodePagesWithReExecute("/Error/Index/{0}");
app.UseExceptionHandler("/Error/Index");
app.UseEndpoints(endpoints =>
{
    // Belirli bir alan varsa bu route'u kullan
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
    );

    // Eğer belirli bir alan belirtilmemişse
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    // Sayfa bulunamadığında Error/Index action'ına git
    endpoints.MapControllerRoute(
        name: "error",
        pattern: "Error/{action=Index}/{id?}",
        defaults: new { controller = "Error" }
    );
});

app.Run();
