using FirstProject.Services;
using Microsoft.AspNetCore.Mvc;
namespace FirstProject.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly ProductService _productService;
        public FirstController(ILogger<FirstController> logger, IWebHostEnvironment env, ProductService productService) 
        {
            _logger =   logger;
            _env = env;
            _productService = productService;   
        }

        public IActionResult Index()
        {
            return View();
        }

        public void Nothing()
        {
            _logger.LogInformation("Hiii");

            Response.Headers.Add("Hii", "Nothing is called");
        }

        public object Anything() => DateTime.Now; // tự động convert về string để trả về cho client

        /*
            Thường thì các action sẽ trả về Đối tượng của lớp IActionResult
            ví dụ
                ContentResult               | Content()
                EmptyResult                 | new EmptyResult()
                FileResult                  | File()    
                ForbidResult                | Forbid()
                JsonResult                  | Json()
                LocalRedirectResult         | LocalRedirect()
                RedirectResult              | Redirect()
                RedirectToActionResult      | RedirectToAction()
                RedirectToPageResult        | RedirectToRoute()
                RedirectToRouteResult       | RedirectToPage()
                PartialViewResult           | PartialView()
                ViewComponentResult         | ViewComponent()
                StatusCodeResult            | StatusCode()
                ViewResult                  | View()
         */

        public IActionResult Readme()
        {
            var content = @"
                Xin chao cac ban iuu

                --Ngoc Anh--


                <h1>Let go</h1>";
            return this.Content(content,"text/html");
        }
        public IActionResult MyLove()
        {
            string filePath = Path.Combine(_env.ContentRootPath, "Files", "Mai.jpg");
            //E:\TaiLieu\MVC\GioiThieuMVC\FirstProject\FirstProject\Files\Mai.jpg
            var fileStream = System.IO.File.ReadAllBytes(filePath);

            return this.File(fileStream, "image/jpg");
        }
        public IActionResult IphonePrice()
        {
            return Json(
                new
                {
                    productName = "Iphone 11prm",
                    price = 100000
                });
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Chuyển hướng đến Home");
            var url = Url.Action("Privacy", "Home");
            return LocalRedirect(url);
        }

        public IActionResult Google()
        {
            _logger.LogInformation("Chuyển hướng đến Google");
            var url = "https://google.com";
            return Redirect(url);
        }
        
        public IActionResult HelloView(string username)
        {
            return View("/Views/Home/Index.cshtml");
            /*
                View() -> razor engine đọc file .cshtml(template)
                View(template) -> Đường dẫn tuyệt đối
                View(template, model) 
                View("name") -> tìm trong thư mục view/tên controller/ name
                không truyền tham số view() thì sẽ gọi tên của Action -> View/First(Controller Name) /HelloView (Action Name)

                trường hợp chỉ muốn chuyền model View((object) username)
             */
        }

        public IActionResult ViewProduct(int? id)
        {
            if(id == null)
            {
                TempData["StatusMessage"] = "San pham khong ton tai";
                return Redirect(Url.Action("Index", "Home"));
            }

            var product = _productService.Where(p => p.Id == id.Value).FirstOrDefault();
            if(product == null)
            {
                TempData["StatusMessage"] = "San pham khong ton tai";
                return Redirect(Url.Action("Index", "Home"));
            }

            this.ViewData["product"] = product; // cách 2 
            //return View(product); cách 1 để truyền dl sang view
            ViewBag.product = product; // kiểu dynamic nên sẽ thực hiện khi chạy ct 

            //TempData // Có ở trong controller , view. Sử dụng session để lưu dữ liệu, nếu đọc ở trang khác thì sau khi đọc sẽ xóa lun
            return View();
        }
    }
}
