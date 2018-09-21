using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Localization;
using TrainingNet.Repositories.Interfaces;
using TrainingNet.Models.Views;
using TrainingNet.Models;

namespace TrainingNet.Controllers
{
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public MovieController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(MovieViewModel movieVM)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie();
                movie.Title = movieVM.Title;
                movie.ReleaseDate = movieVM.ReleaseDate;
                movie.Genre = movieVM.Genre;
                movie.Price = movieVM.Price;
                _unitOfWork.MovieRepository.Add(movie);
                _unitOfWork.Complete();
                return View("Index");
            }
            return View();
        }
    }
}
