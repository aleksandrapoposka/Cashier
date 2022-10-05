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

        public ArticleImageRepository(IMongoDBConnection settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _images = database.GetCollection<ArticleImage>(settings.MongoDBCollectionName);
        }

        public ArticleImage Create(ArticleImage image)
        {
            _images.InsertOne(image);
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
