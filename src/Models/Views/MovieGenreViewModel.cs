using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TrainingNet.Models.Views;
using TrainingNet.Paging;

namespace TrainingNet.Models.Views
{
  public class MovieGenreViewModel
    {
        public PaginatedList<MovieViewModel> MoviesList;
        public SelectList GenresList;
        public string CurrentGenre { get; set; }
    }
}
