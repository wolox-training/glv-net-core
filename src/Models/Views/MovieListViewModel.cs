using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingNet.Models.Views
{
    public class MovieListViewModel
    {
        public List<MovieViewModel> Movies { get; set;}
    }
}
