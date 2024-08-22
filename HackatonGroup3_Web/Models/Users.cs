using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HackatonGroup3_Web.Models
{
    public class Users:IdentityUser
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Remember me?")]
        
        
        public bool RememberMe { get; set; }
    }
}
