using FirstProject.ExtendMethods;
using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("AppMVC");
    options.UseSqlServer(connectionString);
});

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // Mặc định sẽ tìm trong thư mục View/Controller/Action.cshtml
    // Thêm việc đọc trong thư mục MyView/Controller/Action.cshtml
    //{0} - Action {1} - Controller {2} - Ten Area
    options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);
});

builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<PlanetService>();

//builder.Services.AddSingleton(typeof(ProductService));
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

app.UseStatusCodePageWithCustomer();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    //endpoints.MapControllers();
    //endpoints.MapControllerRoute();
    //endpoints.MapDefaultControllerRoute();
    //endpoints.MapAreaControllerRoute();



    //[AcceptVerbs("POST")] -- Attribute cho Action ( chỉ cho phép truy cập nếu là pthuc POST)

    //[Route]
    //[HttpGet]
    //[HttpPost]
    app.MapControllerRoute(
        name: "first",
        pattern: "{url:regex(^((xemsanpham)|(viewproduct))$)}/{id?}",
        defaults: new
        {
            controller = "First",
            action = "ViewProduct"
        },
        constraints: new
        {
            //url = new StringRouteConstraint("xemsanpham"),
            id = new RangeRouteConstraint(2, 4)
        }
    ) ;
    endpoints.MapAreaControllerRoute(
        name: "product",
        pattern: "/{controller}/{action=Index}/{id?}",
        areaName: "ProductManage");

    // Controller không có Area
    app.MapControllerRoute(
        name: "default",
        pattern: "/{controller=Home}/{action=Index}/{id?}"
    );

    
});

app.UseAuthorization();


app.Run();
