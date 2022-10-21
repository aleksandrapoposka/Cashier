using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class StorageAccountConnection : IStorageAccountConnection
    {
        public string ConnectionString { get; set; }
        public string BlobContainer { get; set; }
        public string Table { get; set; }
        public string Queue { get; set; }
    }
}
