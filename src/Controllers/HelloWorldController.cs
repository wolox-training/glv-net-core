using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace TrainingNet.Controllers
{

    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        [HttpGet("")]
        public string Index()
        {
            return "This is my default action...";
        }

        [HttpGet("Welcome/{id}")]
        public string Welcome(string name, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {id}");
        }
    }
}
