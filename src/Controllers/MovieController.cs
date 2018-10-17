using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using TrainingNet.Models;
using TrainingNet.Models.Views;
using TrainingNet.Repositories.Interfaces;
using TrainingNet.Mail;
using System.Collections.Generic;
using TrainingNet.Paging;

namespace TrainingNet.Controllers
{
    [Route("[controller]"), Authorize]
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IHtmlLocalizer<MovieController> _localizer;
        
        public MovieController(IUnitOfWork unitOfWork, IHtmlLocalizer<MovieController> localizer)
        {
            this._unitOfWork = unitOfWork;
            this._localizer = localizer;
        }

        private IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        [HttpGet(""), Authorize]
        public IActionResult Index(string searchString, string currentGenre, string sortOrder, string currentFilter, int? page)
        {  
                                            //null   <---------------------------------------------------ok
                                            //----------------------------------------------------------->VD

                                            //algo   ---------------------------------------------------->VD              1
            try
            {
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewData["CurrentFilter"] = searchString;
                ViewData["CurrentSort"] = sortOrder;
                ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
                ViewData["DateSort"] = sortOrder == "date" ? "date_desc" : "date";  
                ViewData["GenreSort"] = sortOrder == "genre" ? "genre_desc" : "genre";
                ViewData["PriceSort"] = sortOrder == "price" ? "price_desc" : "price";
                ViewData["RatingSort"] = sortOrder == "rating" ? "rating_desc" : "rating";
                var movies = UnitOfWork.MovieRepository.GetAll();
                var movieGenreVM = new MovieGenreViewModel();
                if (movies == null)
                    throw new NullReferenceException();
                var genreQuery = movies.OrderBy(m => m.Genre).Select(m => m.Genre).Distinct().ToList();
                if (!String.IsNullOrEmpty(searchString))
                    movies = movies.Where(m => m.Title.Contains(searchString));
                if (!String.IsNullOrEmpty(currentGenre))
                {
                    movies = movies.Where(m => m.Genre == currentGenre);
                }
                switch (sortOrder)
                {
                    case "title_desc":
                        movies = movies.OrderByDescending(m => m.Title.ToLower());
                        break;
                    case "date":
                        movies = movies.OrderBy(m => m.ReleaseDate);
                        break;
                    case "date_desc":
                        movies = movies.OrderByDescending(m => m.ReleaseDate);
                        break;
                    case "genre":
                        movies = movies.OrderBy(m => m.Genre);
                        break;
                    case "genre_desc":
                        movies = movies.OrderByDescending(m => m.Genre);
                        break;
                    case "price":
                        movies = movies.OrderBy(m => m.Price);
                        break;
                    case "price_desc":
                        movies = movies.OrderByDescending(m => m.Price);
                        break;
                    case "rating":
                        movies = movies.OrderBy(m => m.Rating);
                        break;
                    case "rating_desc":
                        movies = movies.OrderByDescending(m => m.Rating);
                        break;
                    default:
                        movies = movies.OrderBy(m => m.Title.ToLower());
                        break;
                }
                movieGenreVM.GenresList = new SelectList(genreQuery.Distinct());
                var moviesVM = movies.Select(m => new MovieViewModel
                    {
                        Id = m.Id,
                        Title= m.Title,
                        ReleaseDate = m.ReleaseDate,
                        Genre = m.Genre,
                        Price = m.Price,
                        Rating = m.Rating,
                    });
                int pageSize = 3;
                movieGenreVM.CurrentGenre = currentGenre;
                movieGenreVM.MoviesList = PaginatedList<MovieViewModel>.Create(moviesVM.ToList(), page ?? 1, pageSize);
                return View(movieGenreVM);
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

        [HttpPost("Create"),ValidateAntiForgeryToken]
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
        [HttpPost("Delete"),ValidateAntiForgeryToken]
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
        
        [HttpGet("SendMovieMail")]
        public IActionResult SendMovieMail(int? id)
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

        [HttpPost("SendMovieMail")]
        public IActionResult SendMovieMail([FromForm] string emailAddress, int? id)
        {
            try 
            {
                if (id == null)
                    throw new NullReferenceException();
                var movie = UnitOfWork.MovieRepository.Get(id.Value);
                if (movie == null)
                    throw new NullReferenceException();
                string body = $@"
                {_localizer["Movie"].Value}: {movie.Title}{Environment.NewLine}
                {_localizer["ReleaseDate"].Value}: {movie.ReleaseDate}{Environment.NewLine}
                {_localizer["Genre"].Value}: {movie.Genre}{Environment.NewLine}
                {_localizer["Price"].Value}: {movie.Price}{Environment.NewLine}
                {_localizer["Rating"].Value}: {movie.Rating}{Environment.NewLine}";
                Mailer.Send(emailAddress, movie.Title.ToString(), body);
                return RedirectToAction("Index", "Movie");
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
