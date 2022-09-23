using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cashier.Models.Home
{
    public class ApplicationUserViewModel
    {
        [Required]
        [MaxLength(200)]
        [MinLength(2)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Second name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

    }
}
