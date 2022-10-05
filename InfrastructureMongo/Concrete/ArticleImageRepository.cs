using Entities.Articles;
using InfrastructureMongo.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureMongo.Concrete
{
    public class ArticleImageRepository : MongoBaseRepository<ArticleImage>, IArticleImageRepository
    {
      
    }
}
