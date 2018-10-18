using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingNet.Mail;
using TrainingNet.Models;
using TrainingNet.Models.Views;
using TrainingNet.Paging;
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
                var movie = UnitOfWork.MovieRepository.GetMovieWitYourComments(id.Value);
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
        public IActionResult AddComment(int? id, string commentText)
        {
            try 
            {
                if (id == null)
                    throw new NullReferenceException();
                var movie = UnitOfWork.MovieRepository.GetMovieWitYourComments(id.Value);
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
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
