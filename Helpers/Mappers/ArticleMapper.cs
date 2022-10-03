using Cashier.Models.Articles;
using Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.ViewModelHelpers
{
    public static class ArticleMapper
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
    }
}
