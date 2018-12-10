using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscUtilities
{
    public class AzureQueueUtils
    {
        private static AzureQueueUtils _instance;
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);


        private AzureQueueUtils()
        {

        }

        public static AzureQueueUtils Instance
        {
            get
            {
                return _instance ?? (_instance = new AzureQueueUtils());
            }
        }

        public bool QueueHasItems()
        {
            bool hasItems = false;

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("search-queue");

            var message = queue.PeekMessage();

            if (message != null)
            {
                hasItems = true;
            }

            return hasItems;

        }
        public CloudQueueMessage GetQueueItem()
        {

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("search-queue");

            var message = queue.GetMessage();

            if (message != null)
            {
                return message;
            }
            return null;
        }
    }
}
