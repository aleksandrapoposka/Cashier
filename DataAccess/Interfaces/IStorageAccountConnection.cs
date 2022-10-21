using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IStorageAccountConnection
    {
        string ConnectionString { get; set; }
        string BlobContainer { get; set; }
        string Table { get; set; }
        string Queue { get; set; }
    }
}
