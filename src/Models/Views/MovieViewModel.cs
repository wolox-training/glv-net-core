using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingNet.Models.Views
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        public string Title { get; set; }
        
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        
        [Display(Name = "Genero")]
        public string Genre { get; set; }
        
        [Display(Name = "Precio")]
        [Column(TypeName = "decimal(18, 2)")]
        
        public decimal Price { get; set; }
    }
}
