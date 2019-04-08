using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GastoTransparenteMunicipal.Helpers
{
    public class BlobService
    {
        public CloudStorageAccount storageAccount;

        public BlobService(string AccountName, string AccountKey)
        {
            string UserConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", AccountName, AccountKey);
            storageAccount = CloudStorageAccount.Parse(UserConnectionString);
        }

        public CloudBlockBlob UploadBlob(string BlobName, string ContainerName, StringBuilder data)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerName.ToLower());
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(BlobName);

            blockBlob.Properties.ContentType = "text/csv; charset=utf-8";
            blockBlob.UploadText(data.ToString(), new UTF8Encoding(true));
            
            //blockBlob.UploadText(data.ToString(),Encoding.UTF8);                
            return blockBlob;         
        }
     
        public CloudBlockBlob DownloadBlob(string BlobName, string ContainerName)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(BlobName);
            // blockBlob.DownloadToStream(Response.OutputStream);
            return blockBlob;
        }

        public bool CheckConnection()
        {
            string containerCheck = "containercheckconnection";
            bool isConnected = true;

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerCheck);

            try
            {
                container.CreateIfNotExists();
                container.DeleteIfExists();
                return isConnected;
            }
            catch(Exception ex)
            {
                return !isConnected;
            }            
        }
    }
}