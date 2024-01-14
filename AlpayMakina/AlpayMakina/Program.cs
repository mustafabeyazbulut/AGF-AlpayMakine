using AlpayMakina.Models.DapperContect;
using AlpayMakina.Repositories.CategoryRepositories;
using AlpayMakina.Repositories.CompanyInformationRepositories;
using AlpayMakina.Repositories.ProductRepositories;
using AlpayMakina.Repositories.SliderRepositories;
using AlpayMakina.Repositories.SocialMediaRepositories;
using AlpayMakina.Repositories.SubCategoryRepositories;
using AlpayMakina.Utils;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddTransient<Context>();
builder.Services.AddTransient<ISocialMediaRepository, SocialMediaRepository>();
builder.Services.AddTransient<ICompanyInformationRepository, CompanyInformationRepository>();
builder.Services.AddTransient<ISliderRepository, SliderRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ImageOperations>();
var app = builder.Build();

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
    name: "default",
    pattern: "{area=UI}/{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"

    );
});
app.Run();
