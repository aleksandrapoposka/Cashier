using System.ComponentModel.DataAnnotations;

namespace Cashier.Models.Articles
{
    public class ArticleViewModel : IValidatableObject
    {

        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool IsOnSale { get; set; }
        public int Stock { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Country { get; set; }
        public string? ImgSrc { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price <= 0)
            {
                yield return new ValidationResult("Price must be greater than 0", 
                    new[] { nameof(Price) });
            }
        }
    }
}
