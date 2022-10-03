using Cashier.Models.Articles;
using Entities.Articles;

namespace Cashier.Helpers
{
    public class ArticleMapper
    {
        public static ArticleViewModel ToArticleViewModel(Article entity)
        {
            return new ArticleViewModel
            {
                Id = entity.Id,
                Country = entity.Country,
                Description = entity.Description,
                IsOnSale = entity.IsOnSale,
                Manufacturer = entity.Manufacturer,
                Name = entity.Name,
                Price = entity.Price,
                Stock = entity.Stock
            };
        }

        public static Article ToArticleEntity(ArticleViewModel articleViewModel, string modifiedBy = "admin")
        {
            return new Article
            {
                Id = articleViewModel.Id,
                Country = articleViewModel.Country,
                Description = articleViewModel.Description,
                IsOnSale = articleViewModel.IsOnSale,
                Manufacturer = articleViewModel.Manufacturer,
                Name = articleViewModel.Name,
                Price = articleViewModel.Price,
                Stock = articleViewModel.Stock,
                ModifiedOn = DateTime.UtcNow,
                ModifiedBy = modifiedBy
            };
        }
    }
}
