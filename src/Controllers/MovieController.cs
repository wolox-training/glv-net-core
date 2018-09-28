using Microsoft.AspNetCore.Mvc;
using TrainingNet.Repositories.Interfaces;
using TrainingNet.Models.Views;
using TrainingNet.Models;
using System;

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
                var movie = new Movie 
                {
                    Title = movieVM.Title,
                    ReleaseDate = movieVM.ReleaseDate,
                    Genre = movieVM.Genre,
                    Price = movieVM.Price,
                };
                UnitOfWork.MovieRepository.Add(movie);
                UnitOfWork.Complete();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
               
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            try 
            {
                
                if (id == null)
                    throw new NullReferenceException();
                var movie = UnitOfWork.MovieRepository.Get(id.Value);
                if (movie == null)
                    throw new NullReferenceException();
                MovieViewModel movieVM = new MovieViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Genre = movie.Genre,
                    Price = movie.Price,
                };
                return View(movieVM);
            }
            catch (NullReferenceException) 
            {
                return NotFound();
            }                
        }

        [HttpPost("Edit/{id}")]
        public IActionResult Edit(MovieViewModel movieVM)
        {
            try 
            {
                if (!ModelState.IsValid)
                    throw new NullReferenceException();
                var movie = new Movie 
                {
                    Id = movieVM.Id,
                    Title = movieVM.Title,
                    ReleaseDate = movieVM.ReleaseDate,
                    Genre = movieVM.Genre,
                    Price = movieVM.Price,
                };
                UnitOfWork.MovieRepository.Update(movie);
                UnitOfWork.Complete();
                return RedirectToAction("Index", "Home");
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
