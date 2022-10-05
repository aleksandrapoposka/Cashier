using Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureMongoDB
{
    public interface IArticleImageRepository
    {
        List<ArticleImage> Get();
        ArticleImage Get(long articleId);
        ArticleImage Create(ArticleImage image);
        void Update(long articleId, ArticleImage image);
        void Remove(long articleId);
    }
}
