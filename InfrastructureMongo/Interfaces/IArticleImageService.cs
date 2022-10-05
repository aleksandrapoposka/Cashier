using Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureMongo.Interfaces
{
    public interface IArticleImageService
    {
        List<ArticleImage> Get();
        ArticleImage Get(string id);
        ArticleImage Create(ArticleImage student);
        void Update(string id, ArticleImage student);
        void Remove(string id);
    }
}
