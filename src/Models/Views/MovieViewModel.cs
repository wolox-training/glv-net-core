using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingNet.Models.Views
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Titulo"), StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }
        
        [Display(Name = "Release Date"), DataType(DataType.Date), Range(typeof(DateTime), "1/1/186", "1/1/2020")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        
        [Required, Display(Name = "Genero"),RegularExpression(@"^[A-Z]+[a-zA-ZñÑáéíóúÁÉÍÓÚ""'\s-]*$"), StringLength(30)]
        public string Genre { get; set; }
        
        [Required, Display(Name = "Precio"), DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required, Display(Name = "Calificación"), RegularExpression(@"^[0-9]+[0-9""'\s-]*$"), StringLength(2)]
        public string Rating { get; set; }

        [Required]
        public ICollection<Comment> Comments { get; set; }
  }
}
