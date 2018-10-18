using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TrainingNet.Models.Views;

namespace TrainingNet.Models.Views
{
  public class MovieGenreViewModel
    {
        public List<MovieViewModel> MoviesList;
        public SelectList GenresList;
        public string MovieGenre { get; set; }
    }
}