using ImageLoader.Data;
using ImageLoader.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using System.IO;

namespace ImageLoader
{
    public class ImageController : ApiController
    {
        //Add dependency injection (constructor)
        private readonly IUnitOfWork uow = null;
        private readonly IRepository<Domain.Image> repository = null;

        public ImageController()
        {
            uow = new UnitOfWork();
            repository = new Repository<Domain.Image>(uow);
        }
        // GET api/<controller>
        public IEnumerable<Image> Get()
        {          
            return repository.All.ToList();
        }
       
        // POST api/<controller>
        public void Post([FromBody]string urlValue)
        {            
        }

        // PUT api/<controller>/5
        [HttpPut]
        public Image Put([FromBody] Image image)
        {
            //TODO: Add exception handling here
            var imageBytes = DownloadImage(image.Url);
            string imageBlobUrl = SaveImageToBlob(imageBytes);

            var newImage = new Image { Domain = (new Uri(imageBlobUrl)).Host, Url = imageBlobUrl };
            repository.Insert(newImage);
            uow.Save();

            return newImage;
        }

        // DELETE api/<controller>/5
        public void Delete(int id, string blobId)
        {
            //TODO: Add exception handling here
            repository.Delete(id);
            uow.Save();

            DeleteBlob(blobId);
        }

        private byte[] DownloadImage(string imageUrl)
        {
            byte[] bytes = null;

            using (var wc = new WebClient())
            {
                bytes = wc.DownloadData(imageUrl);
            }

            return bytes;
        }

        private string SaveImageToBlob(byte[] image)
        {
            string blobContainerName = "pictures";
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(blobContainerName);

            var blobIdString = Guid.NewGuid().ToString();

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobIdString);

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var memoryStream = new MemoryStream(image))
            {
                blockBlob.UploadFromStream(memoryStream);
            }

            return string.Format("https://imagestorage1.blob.core.windows.net/{0}/{1}", blobContainerName, blobIdString);
        }

        private void DeleteBlob(string blobId)
        {
            string blobContainerName = "pictures";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(blobContainerName);

            // Retrieve reference to a blob 
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobId);

            // Delete the blob.
            blockBlob.Delete();
        }
    }
}