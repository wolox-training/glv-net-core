using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace TrainingNet.Controllers
{

    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/
        [HttpGet("")]
        public string Index()
        {
            return "This is my default action...";
        }

        // GET: /HelloWorld/Welcome/ 
        // Requires using System.Text.Encodings.Web;
        
        [HttpGet("Welcome/{id}")]           // Indicate that Welcome will receive an id in the method
        public string Welcome(string name, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {id}");
        }
    }
}
