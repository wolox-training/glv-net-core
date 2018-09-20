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

        public IHtmlLocalizer<HomeController> Localizer
        {
            get {return this._localizer;}
        }

        public HomeController(IHtmlLocalizer<HomeController> localizer)
        {
            this._localizer = localizer;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewData["Message"] = Localizer["HomePage"].Value; 
            return View();
        }

        [HttpGet("About")]
        public IActionResult About()
        {
            ViewData["Message"] = Localizer["AboutPage"].Value;
            return View();
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = Localizer["ContactPage"].Value;
            return View();
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            ViewData["Message"] = Localizer["PrivacyPage"].Value;
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
