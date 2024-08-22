using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace HackatonGroup3_Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "L'adresse email n'est pas valide")]
        [EmailAddress(ErrorMessage = "L'adresse email n'est pas valide")]
        public string Email { get; set; }
     
        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
