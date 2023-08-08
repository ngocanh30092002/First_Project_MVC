using FirstProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FirstProject.Controllers
{
    /// <summary>
    /// Route ở controller + route ở action
    /// Route ở controller mà thiết lập thì phải thiết lập router ở action nếu k thiết lập thì sử dụng /[action]
    /// Route ở action mà có ký tự / ở đầu thì kh cần + route ở controller
    /// </summary>
    /// 
    [Route("he-mat-troi/[action]")]
    public class PlanetController : Controller
    {
        private readonly PlanetService _planetService;
        private readonly ILogger <PlanetController> _logger;
        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger)
        {
            _planetService = planetService;
            _logger = logger;
        }

        [Route("danh-sach-hanh-tinh")]
        //[Route("/danh-sach-hanh-tinh")]
        public IActionResult Index()
        {
            return View();
        }

        //[BindProperty(SupportsGet = true, Name = "action")]
        //public string Name { set; get; }

        public IActionResult Neptune(string action)
        {
            var planet = _planetService.Where(p => p.Name == action).FirstOrDefault();
            return View("Detail", planet);
        }

        public IActionResult Saturn(string action)
        {
            var planet = _planetService.Where(p => p.Name == action).FirstOrDefault();
            return View("Detail", planet);
        }

        public IActionResult Jupiter(string action)
        {
            var planet = _planetService.Where(p => p.Name == action).FirstOrDefault();
            return View("Detail", planet);
        }

        public IActionResult Comet(string action)
        {
            var planet = _planetService.Where(p => p.Name == action).FirstOrDefault();
            return View("Detail", planet);
        }

        /// <summary>
        /// Route()  muốn truy cập đến Controller , action, area sẽ sd "[]"
        /// Các taghelper sẽ ưu tiên cái /[controller]/[action]
        /// Có thể đặt tên route để sau phát sinh route Url.RouteUrl(Name)
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [Route("sao/[action]", Order = 1, Name = "Uranus1")] // sao/Uranus        
        [Route("sao/[controller]/[action]", Order = 2)] // sao/Planet/Uranus        
        [Route("[controller]-[action].html" , Order = 3)] // Planet-Uranus.html
        public IActionResult Uranus(string action)
        {
            var planet = _planetService.Where(p => p.Name == action).FirstOrDefault();
            return View("Detail", planet);
        }

        /// <summary>
        /// Route(hanhtinh/{id:int}) để thực hiện action thì gọi đến url: hanhtinh/1 
        /// Nếu Action có attribute Route sẽ k chịu ảnh hưởng từ MapControllerRoute
        /// Còn các cái khác vẫn bị ảnh hưởng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("hanhtinh/{id:int}")] 
        public IActionResult PlanetInfor(int id)
        {
            var planet = _planetService.Where(p => p.Id == id).FirstOrDefault();
            return View("Detail", planet);
        }
    }
}
