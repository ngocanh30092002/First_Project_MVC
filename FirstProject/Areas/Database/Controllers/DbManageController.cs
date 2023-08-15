using App.Data;
using App.Models;
using FirstProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbManageController(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }

        [TempData]
        public string StatusMessage { set; get; }

        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _dbContext.Database.EnsureDeletedAsync();

            StatusMessage = success ? "Xóa thành công" : "Xóa thất bại";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SeedUserAsync() 
        {
            var rolenames = typeof(RoleName).GetFields().ToList();

            foreach(var role in rolenames)
            {
                var roleName = role.GetRawConstantValue().ToString();
                var isExits = await _roleManager.FindByNameAsync(roleName);

                if(isExits == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var userAdmin = await _userManager.FindByNameAsync("admin");

            if(userAdmin == null)
            {
                userAdmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(userAdmin, "admin123");
                await _userManager.AddToRoleAsync(userAdmin, RoleName.Administrator);
            }


            StatusMessage = "Tạo mới thành công";

            return RedirectToAction(nameof(Index));
        }
    }
}
