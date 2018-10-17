using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingNet.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int IdMovie { get; set; }
        public string MovieAssociated { get; set; }
    }
}
