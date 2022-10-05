using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IMongoDBConnection
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string MongoDBCollectionName { get; set; }
    }
}
