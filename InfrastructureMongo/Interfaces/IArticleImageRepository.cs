using Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureMongo.Interfaces
{
    public  interface IArticleImageRepository : IMongoBaseRepository<ArticleImage>
    {
    }
}
