using System.ComponentModel.DataAnnotations;

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
            throw new NotImplementedException();
        }
    }
}
