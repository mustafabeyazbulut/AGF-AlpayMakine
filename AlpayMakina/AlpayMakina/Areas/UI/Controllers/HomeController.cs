using AlpayMakina.Dtos.SubCategoryDtos;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.Controllers
{
    [Area("UI")]
    [Route("UI/Home")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            ViewBag.CategoryId = 0;
            ViewBag.SubCategoryId = 0;
            return View();
        }

        
        [Route("{id}")]
        [Route("Index/{id}")]
        public IActionResult Index(int id)
        {
            ViewBag.CategoryId = id;
            return View();
        }
    }
}
