using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
namespace Entities
{
    public class BaseMongoEntity
    {
        [Serializable]
        public abstract class MongoBaseEntity
        {
            [BsonId]
            public ObjectId Id { get; set; }
            public string ModifiedBy { get; set; }
            public DateTime ModifiedOn { get; set; }
        }
    }
}
