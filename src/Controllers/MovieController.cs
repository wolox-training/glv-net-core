using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Localization;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Controllers
{
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IHtmlLocalizer<MovieController> _localizer;
        private readonly IUnitOfWork _unitOfWork;
        
        public IHtmlLocalizer<MovieController> Localizer
        {
            get {return this._localizer;}
        }
        
        public MovieController(IHtmlLocalizer<MovieController> localizer)
        {
            this._localizer = localizer;
        }

        public MovieController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        [HttpGet("Create")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
    }
}
