using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Utilities.AzureUtilities
{
    public static class QueueUtilities
    {
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);


        public static void InsertIdiomInQueue(string idiom)
        {
            CloudQueueClient client = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = client.GetQueueReference("search-queue");

            CloudQueueMessage message = new CloudQueueMessage(idiom);
            
            queue.AddMessage(message,TimeSpan.FromSeconds(10));

        }

    }
}