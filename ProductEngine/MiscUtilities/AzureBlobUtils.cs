using Logger;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscUtilities
{
    public class AzureBlobUtils
    {
        private static AzureBlobUtils _instance;
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);


        private AzureBlobUtils()
        {

        }

        public static AzureBlobUtils Instance
        {
            get
            {
                return _instance ?? (_instance = new AzureBlobUtils());
            }
        }

        public static void DownloadBlob(string fileName, string container)
        {

            try
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer containerRef = blobClient.GetContainerReference(container);
                CloudBlockBlob blockBlob = containerRef.GetBlockBlobReference(fileName);


                blockBlob.DownloadToFile(Path.Combine(Environment.CurrentDirectory, "BlobFiles", fileName), FileMode.Create);



            }
            catch (Exception ex)
            {
                GenericLogger.Error($"Exception {ex.Message} popped when trying to get Blob : {fileName} from container : {container}", ex);
            }




        }


    }
}
