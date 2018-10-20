using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TrainingNet.Models;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.api.v1.Controllers
{
  [Route("api/v1/[controller]")]
    public class CommentsAPIController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CommentsAPIController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        [HttpGet("Comments")]
        public IActionResult Comments(int? id)
        {
            try 
            {
                if (id == null)
                    throw new NullReferenceException();
                var movie = UnitOfWork.MovieRepository.GetMovieWithComments(id.Value);
                if (movie == null)
                    throw new NullReferenceException();
                return Json(movie.Comments.ToList());
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("AddComment")]
        public IActionResult AddComment(int? idMovie, string commentText)
        {
            try 
            {
                if (idMovie == null)
                    throw new NullReferenceException("Movie Error");
                if (string.IsNullOrEmpty(commentText))
                    throw new NullReferenceException("Comment not found");
                var movie = UnitOfWork.MovieRepository.GetMovieWithComments(idMovie.Value);
                if (movie == null)
                    throw new NullReferenceException();
                var comment = new Comment
                {
                    Text = commentText,
                    Movie = movie
                };
                movie.Comments.Add(comment);
                UnitOfWork.MovieRepository.Update(movie);
                UnitOfWork.Complete();
                return Json(new {comment=comment.Text.ToString()});
            }
            catch(NullReferenceException e)
            {
                Response.StatusCode=400;
                return Json(e.Message);
            }
        }
    }
}
