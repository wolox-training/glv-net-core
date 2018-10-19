using System.ComponentModel.DataAnnotations;

namespace TrainingNet.Models.Views
{

    public class UserViewModel
    {
        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required, Display(Name = "Email Address"), EmailAddress, MaxLength(20)]
        public string Email { get; set; }

        [Required, Display(Name = "Password"), DataType(DataType.Password), MinLength(6), MaxLength(40)]
        public string Password { get; set; }

        [Display(Name = "Confirm password"), DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
