using Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureStorageAccount.Interfaces
{
    public interface ITableRepository
    {
        Task InsertArticleImage(ArticleImage articleImage);
        string GetImageUri(long articleId);
    }
}
