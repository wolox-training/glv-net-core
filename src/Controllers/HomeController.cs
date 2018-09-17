using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using TrainingNet.Models;

namespace TrainingNet.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HomeController(IHtmlLocalizer<HomeController> localizer)
        {
            this._localizer = localizer;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            
            ViewData["Message"] = _localizer["HomePage"];
            return View();
        }

        [HttpGet("About")]
        public IActionResult About()
        {
            ViewData["Message"] = _localizer["AboutPage"];
            return View();
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = _localizer["ContactPage"];
            return View();
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            ViewData["Message"] = _localizer["PrivacityPage"];
            return View();
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
