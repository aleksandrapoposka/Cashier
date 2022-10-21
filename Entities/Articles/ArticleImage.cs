using Azure.Data.Tables;
using Azure;

namespace Entities.Articles
{
   
        public class ArticleImage : ITableEntity
        {       
        
            public ArticleImage()
            {
                PartitionKey = nameof(ArticleId);
                RowKey = Guid.NewGuid().ToString();
                Timestamp = DateTime.Now;
            }
            public long ArticleId { get; set; }        
           
            public string Image { get; set; }

            public string PartitionKey { get; set; }

            public string RowKey { get; set; }

            public DateTimeOffset? Timestamp { get; set; }

            public ETag ETag { get; set; }
    }
    
}