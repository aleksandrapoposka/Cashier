using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cashier.Models.Home
{
    public class RegisterUserViewModel : IValidatableObject
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
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
        
        [Required]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth > DateTime.UtcNow.AddYears(-18))
                yield return new ValidationResult($"User must be older than 18 years.", new[] { nameof(DateOfBirth) });
        }
    }
}
