using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Cashier.Models.Countries
{
    public class CountryViewModel : IValidatableObject

    {
        public long Id { get; set; }
        [Required]
        public int IsoNumericCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Alpha2Code { get; set; }
        [Required]
        public string Alpha3Code { get; set; }
        [Required]
        public string ContinentCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Alpha2Code.Length != 2)
            {
                yield return new ValidationResult("Please enter valid alpha2 code",
                    new[] { nameof(Alpha2Code) });
            }

            if (Alpha3Code.Length != 3)
            {
                yield return new ValidationResult("Please enter valid alpha3 code",
                    new[] { nameof(Alpha3Code) });
            }

            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(IsoNumericCode.ToString()))
            {
                yield return new ValidationResult("Please enter valid ISO numeric code",
                    new[] { nameof(IsoNumericCode) });
            }
        }
    }
}
