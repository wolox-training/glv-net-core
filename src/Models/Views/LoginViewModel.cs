using System.ComponentModel.DataAnnotations;

namespace TrainingNet.Models.Views
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required, Display(Name = "Password"), DataType(DataType.Password), MinLength(6), MaxLength(40)]
        public string Password { get; set; }

        [Required, Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
