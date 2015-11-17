using ImageLoader.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ImageLoader
{
    public static class UserAgentLogger
    {
        public static void LogUserAgentInfo(UserAgentEntity entity)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            //TODO: Remove direct reading from config - inject connection string.
            ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("useragents");
            table.CreateIfNotExists();

            TableOperation insertOperation = TableOperation.Insert(entity);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }
    }
}