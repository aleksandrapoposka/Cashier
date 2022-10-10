using DataAccess;
using Entities.Articles;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace InfrastructureMongoDB
{
    public class ArticleImageRepository : IArticleImageRepository
    {
        private readonly IMongoCollection<ArticleImage> _images;
        public ArticleImageRepository(IMongoDatabase mongoDatabase)
        {
            _images = mongoDatabase.GetCollection<ArticleImage>("ArticleImage");
        }

        public ArticleImage Create(ArticleImage image)
        {
            var existingImage = _images.Find(i => i.ArticleId == image.ArticleId).FirstOrDefault();
            if(existingImage == null)
            {
                _images.InsertOne(image);
            } else
            {
                _images.DeleteMany(i => i.ArticleId == image.ArticleId);
                _images.InsertOne(image);
            }
            return image;
        }

        public List<ArticleImage> Get()
        {
            return _images.Find(image => true).ToList();
        }

        public ArticleImage Get(long articleId)
        {
            return _images.Find(image => image.ArticleId == articleId).FirstOrDefault();
        }

        public void Remove(long articleId)
        {
            _images.DeleteOne(image => image.ArticleId == articleId);

        }

        public void Update(long articleId, ArticleImage image)
        {
            _images.ReplaceOne(image => image.ArticleId == articleId, image);
        }
    }
}
