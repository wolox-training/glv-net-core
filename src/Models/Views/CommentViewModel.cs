using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingNet.Models.Views
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Required, Display(Name = "Comentario"), StringLength(500, MinimumLength = 3), RegularExpression(@"^[0-9a-zA-ZñÑáéíóúÁÉÍÓÚ""'\s-]*$")]
        public string Text { get; set; }
        public int IdMovie { get; set; }
        [Required, Display(Name = "Titulo"), StringLength(60, MinimumLength = 3)]
        public string MovieAssociated { get; set; }
    }
}
