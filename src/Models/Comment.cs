using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingNet.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
