using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Localization;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Controllers
{
    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        private readonly IHtmlLocalizer<HelloWorldController> _localizer;
        private readonly IUnitOfWork _unitOfWork;
        
        private IHtmlLocalizer<HelloWorldController> Localizer
        {
            get => this._localizer;
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
        public HelloWorldController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }
    }
}
