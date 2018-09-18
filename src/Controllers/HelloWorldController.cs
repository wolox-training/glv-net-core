using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Localization;


namespace TrainingNet.Controllers
{
    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        private readonly IHtmlLocalizer<HelloWorldController> _localizer;
        
        public IHtmlLocalizer<HelloWorldController> Localizer
            {
                get {return this._localizer;}
            }
        
        public HelloWorldController(IHtmlLocalizer<HelloWorldController> localizer)
        {
            this._localizer = localizer;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Index";
            ViewData["Message"] = Localizer["HomePage"].Value;
            return View();
        }

        [HttpGet("Welcome/{id}")]
        public string Welcome(string name, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {id}");
        }
    }
}
