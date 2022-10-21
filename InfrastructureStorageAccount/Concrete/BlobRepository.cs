using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DataAccess.Interfaces;
using InfrastructureStorageAccount.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureStorageAccount.Concrete
{
    public class BlobRepository : IBlobRepository
    {
        private readonly IStorageAccountConnection _storageAccountConnection;
        private readonly BlobContainerClient _blobContainerClient; 

        public BlobRepository(IStorageAccountConnection storageAccountConnection)
        {
            _storageAccountConnection = storageAccountConnection;
            _blobContainerClient = new BlobServiceClient(_storageAccountConnection.ConnectionString)
                .GetBlobContainerClient(_storageAccountConnection.BlobContainer);
        }

        public async Task<string> UploadBlob(IFormFile file)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(file.FileName);
            Stream image = file.OpenReadStream();
            await blobClient.UploadAsync(image,new BlobHttpHeaders {ContentType = GetContentType(file.FileName)});
            return blobClient.Uri.AbsoluteUri;
        }

        private static string GetContentType(string file)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(file, out string contentType))
                contentType = "application/octet-stream";

            return contentType;
        }
    }
}
