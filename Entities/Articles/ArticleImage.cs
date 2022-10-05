using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Articles
{
    public class ArticleImage: MongoBaseEntity
    {
        [BsonElement("articleId")]
        public string ArticleId { get; set; } = String.Empty;
        [BsonElement("image")]
        [BsonRepresentation(BsonType.Binary)]
        public byte[] Image { get; set; } 

    }
}
