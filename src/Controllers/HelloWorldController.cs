using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace TrainingNet.Controllers
{
    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {

            ViewData["Title"]="Index";
            ViewData["Message"]="Hello from our View Template for TrainingNet!";
            return View();
        }

        [HttpGet("Welcome/{id}")]
        public string Welcome(string name, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {id}");
        }
    }
}
