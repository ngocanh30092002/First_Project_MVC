using FirstProject.Controllers;
using FirstProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Areas.ProductManage.Controllers
{
    [Area("ProductManage")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly ILogger<PlanetController> _logger;

        public ProductController(ProductService productService, ILogger<PlanetController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [Route("cac-san-pham")]
        public IActionResult Index()
        {
            // /Areas/AreaName/Views/ControllerName/Action.cshtml
            var products = _productService.OrderBy(p => p.Name).ToList();
            return View(products); // /Areas/ProductManage/Views/Product/Index.cshtml
        }
    }
}
