using System;
using System.IO;
using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Shared.Models.Settings;

namespace Service.Implementation
{
    public class StorageHandler : IStorageHandler
    {
        private readonly StorageSettings _storageSettings;
        private readonly AmazonS3Client _client;
        public StorageHandler(IOptions<Settings> appSettings, AmazonS3Client client)
        {
            _storageSettings = appSettings.Value.StorageSettings;
            _client = client;
        }

        public string StoreFile(Stream file)
        {
            try
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = Guid.NewGuid().ToString(),
                        BucketName = _storageSettings.BucketName,
                        CannedACL = S3CannedACL.Private
                    };
                    var fileTransferUtility = new TransferUtility(_client);
                    fileTransferUtility.UploadAsync(uploadRequest).Wait();
                    return $"https://s3.amazonaws.com/{_storageSettings.BucketName}/{uploadRequest.Key}";
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Stream RetrieveFile(string path)
        {
            try
            {
                AmazonS3Uri amazonS3Uri = new AmazonS3Uri(path);
                var uploadRequest = new GetObjectRequest()
                {
                    Key = amazonS3Uri.Key,
                    BucketName = amazonS3Uri.Bucket,
                };
                var getFileResponse = _client.GetObjectAsync(uploadRequest);
                var file = getFileResponse.Result.ResponseStream;
                return file;
            }
            catch (Exception e)
            {
                var exception = cheekAggregateException(e);
                if (exception is AmazonS3Exception)
                {
                    if ((exception as AmazonS3Exception).StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }
                throw;
            }
        }
        private Exception cheekAggregateException(Exception exception)
        {
            return exception is AggregateException ? cheekAggregateException(exception.InnerException) : exception;
        }
    }
}
