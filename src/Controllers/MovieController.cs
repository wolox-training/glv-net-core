using Microsoft.AspNetCore.Mvc;
using TrainingNet.Repositories.Interfaces;
using TrainingNet.Models.Views;
using TrainingNet.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrainingNet.Controllers
{
    [Route("[controller]"), Authorize]
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

        [HttpGet("")]
        public IActionResult Index(string searchString, string movieGenre)
        {
            try
            {
                var movies = UnitOfWork.MovieRepository.GetAll();
                var genreQuery = movies.OrderBy(m => m.Genre).Select(m => m.Genre).Distinct().ToList();
                if (movies == null)
                    throw new NullReferenceException();
                if (!String.IsNullOrEmpty(searchString))
                {
                    var moviesVM = movies.Select(m => new MovieViewModel
                    {
                        Id = m.Id,
                        Title= m.Title,
                        ReleaseDate = m.ReleaseDate,
                        Genre = m.Genre,
                        Price = m.Price,
                        Rating = m.Rating,
                    })
                    .Where(m => m.Title.Contains(searchString))
                    .ToList();
                        var movieGenreVM = new MovieGenreViewModel();
                        movieGenreVM.GenresList = new SelectList(genreQuery.Distinct());
                        movieGenreVM.MoviesList = moviesVM.ToList();
                    return View(movieGenreVM);
                }
                else
                {
                    if (!String.IsNullOrEmpty(movieGenre))
                    {
                        var moviesVM = movies.Select(m => new MovieViewModel
                        {
                            Id = m.Id,
                            Title= m.Title,
                            ReleaseDate = m.ReleaseDate,
                            Genre = m.Genre,
                            Price = m.Price,
                            Rating = m.Rating,
                        })
                        .Where(m => m.Genre == movieGenre)
                        .ToList();
                        var movieGenreVM = new MovieGenreViewModel();
                        movieGenreVM.GenresList = new SelectList(genreQuery.Distinct());
                        movieGenreVM.MoviesList = moviesVM.ToList();

                        return View(movieGenreVM);
                    }
                    else
                    {
                        var moviesVM = movies.Select(m => new MovieViewModel
                        {
                            Id = m.Id,
                            Title = m.Title,
                            ReleaseDate = m.ReleaseDate,
                            Genre = m.Genre,
                            Price = m.Price,
                            Rating = m.Rating,
                        }).ToList();
                        var movieGenreVM = new MovieGenreViewModel();
                        movieGenreVM.GenresList = new SelectList(genreQuery.Distinct());
                        movieGenreVM.MoviesList = moviesVM.ToList();
                        return View(movieGenreVM);
                    }
                }
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
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
                    Rating = movieVM.Rating,
                };
                UnitOfWork.MovieRepository.Add(movie);
                UnitOfWork.Complete();
                return RedirectToAction("Index", "Movie");
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
                    Rating = movie.Rating,
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
                    Rating = movieVM.Rating,
                };
                UnitOfWork.MovieRepository.Update(movie);
                UnitOfWork.Complete();
                return RedirectToAction("Index", "Movie");
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet("Delete")]
        public IActionResult Delete(int? id)
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
                    Rating = movie.Rating,
                };
                return View(movieVM);
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                    throw new NullReferenceException();
                var movie = UnitOfWork.MovieRepository.Get(id.Value);
                if (movie == null)
                    throw new NullReferenceException();
                UnitOfWork.MovieRepository.Remove(movie);
                UnitOfWork.Complete();
                return RedirectToAction("Index", "Movie");
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }
        
        [HttpGet("Details")]
        public IActionResult Details(int? id)
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
                    Rating = movie.Rating,
                };
                return View(movieVM);
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
