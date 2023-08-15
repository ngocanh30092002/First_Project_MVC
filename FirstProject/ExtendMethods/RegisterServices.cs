using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;



namespace FirstProject.ExtendMethods
{
    public static class RegisterServices
    {
        public static void Register(this WebApplicationBuilder builder)
        {

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

            builder.Services.ConfigureIdentityService<AppUser, IdentityRole, AppDbContext>();

            builder.Services.AuthenticationService(builder);
        }

        public static void AuthenticationService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication().AddGoogle(
                googleOption =>
                {
                    var googleConfig = builder.Configuration.GetSection("Authentication:Google");
                    googleOption.ClientId = googleConfig["ClientId"] ?? "";
                    googleOption.ClientSecret = googleConfig["ClientSecret"] ?? "";
                    googleOption.CallbackPath = "/dang-nhap-tu-google"; // Nếu kh thiết lập thì sẽ mặc định là https://localhost:7227/signin-google
                })
                .AddFacebook(
                facebookOptions =>
                {
                    var fbConfig = builder.Configuration.GetSection("Authentication:Facebook");
                    facebookOptions.AppId = fbConfig["AppId"] ?? "";
                    facebookOptions.AppSecret = fbConfig["AppSecret"] ?? "";
                    facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
                });
        }
        public static void ConfigureIdentityService<IUser, IRole, IContext>(this IServiceCollection service) where IUser: IdentityUser where IRole : IdentityRole where IContext : DbContext
        {
            //Đăng ký dịch vụ Identity
            service.AddIdentity<IUser, IRole>() // Thiết lập user, role
            .AddEntityFrameworkStores<IContext>() // làm việc trên dbcontext nào
                .AddDefaultTokenProviders();

            //builder.Services.AddDefaultIdentity<AppUser>()
            //                .AddEntityFrameworkStores<MyBlogContext>()
            //                .AddDefaultTokenProviders();


            service.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/login/";
                option.LogoutPath = "/logout/";
                option.AccessDeniedPath = "/khongduoctruycap/"; //khi mà truy cập 1 trang k đc phép sẽ chuyển hướng đến trang này
            });
            service.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = false;
            });


            service.AddAuthorization(options =>
            {
                options.AddPolicy("AllowEditRole", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    //policyBuilder.RequireRole("Admin");
                    //policyBuilder.RequireRole("Editor"); // có cả admin và editor
                    //policyBuilder.RequireClaim("Manage.Role", "Add", "Update");
                    policyBuilder.RequireClaim("AllowToEdit", "User");
                    //policyBuilder.RequireClaim("Ten Claim", new string[]
                    //{
                    //    "Gia tri 1",
                    //    "Gia Tri 2"
                    //});
                });
            });
        }
    }
}
