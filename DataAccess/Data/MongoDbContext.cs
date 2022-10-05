using DataAccess.Helpers;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using MongoDB.Driver.Core.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class MongoDBContext 
    {
        private readonly IConfiguration Configuration;
        private IMongoDatabase _db = null;

        public MongoDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
            Initialize();
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

        private MongoClient Initialize()
        {
            try
            {
                string connectionString = Configuration[ConfigurationConst.MongoConnString];
                if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString), "Cannot be null or empty !");

                MongoClient client = new MongoClient(connectionString);

                string _databaseName = MongoUrl.Create(connectionString).DatabaseName;
                if (string.IsNullOrWhiteSpace(_databaseName)) throw new ArgumentNullException(nameof(_databaseName), "Cannot be null or empty !");
                _db = client.GetDatabase(_databaseName);

                _db.RunCommand<BsonDocument>(new BsonDocument("ping", 1));

                if (client.Cluster.Description.State == ClusterState.Disconnected) throw new MongoConnectionException(new ConnectionId(client.Cluster.Description.Servers.FirstOrDefault()?.ServerId), "Connection state - Disconnected");

                return client;
            }
            catch (Exception ex)
            {
                throw new Exception("MongoDB connection failed.", ex);
            }
        }
    }
}
