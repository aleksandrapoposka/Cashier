using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.BaseMongoEntity;

namespace Entities.Articles
{
   
        public class ArticleImage : MongoBaseEntity
        {
            [BsonElement("articleId")]
            public long ArticleId { get; set; } = 0;
            
            [BsonElement("image")]
            [BsonRepresentation(BsonType.Binary)]
            public byte[] Image { get; set; }

        }
    
}