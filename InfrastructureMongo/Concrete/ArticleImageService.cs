using DataAccess.Interfaces;
using Entities.Articles;
using InfrastructureMongo.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace InfrastructureMongo.Concrete
{
    public class ArticleImageService : IArticleImageService
    {
        private readonly IMongoCollection<ArticleImage> _images;

        public ArticleImageService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _images = database.GetCollection<ArticleImage>(settings.CollectionName);
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

        public ArticleImage Get(string id)
        {
            return _images.Find(image => image.Id.ToString() == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _images.DeleteOne(image => image.Id.ToString() == id);
        }

        public void Update(string id, ArticleImage image)
        {
            _images.ReplaceOne(student => image.Id.ToString() == id, image);
        }
    }
}
