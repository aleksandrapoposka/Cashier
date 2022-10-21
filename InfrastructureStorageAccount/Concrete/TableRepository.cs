using Azure.Data.Tables;
using DataAccess.Interfaces;
using Entities.Articles;
using InfrastructureStorageAccount.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureStorageAccount.Concrete
{
    public class TableRepository : ITableRepository
    {
        private readonly IStorageAccountConnection _storageAccountConnection;
        private readonly TableClient _tableClient;

        public TableRepository(IStorageAccountConnection storageAccountConnection)
        {
            _storageAccountConnection = storageAccountConnection;
            _tableClient = new TableClient(_storageAccountConnection.ConnectionString, _storageAccountConnection.Table);
            _tableClient.CreateIfNotExistsAsync();
        }

        public string GetImageUri(long articleId)
        {
            ArticleImage articleImage = _tableClient.Query<ArticleImage>(image => image.ArticleId == articleId)?.SingleOrDefault();
            return (articleImage == null) ? "" : articleImage.Image;
        }

        public async Task InsertArticleImage(ArticleImage articleImage)
        {
            await _tableClient.AddEntityAsync(articleImage);
        }
    }
}
