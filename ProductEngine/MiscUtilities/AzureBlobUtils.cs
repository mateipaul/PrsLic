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

        public void DownloadBlob(string fileName, string container,string usage)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "BlobFiles",usage);
            try
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer containerRef = blobClient.GetContainerReference(container);
                CloudBlockBlob blockBlob = containerRef.GetBlockBlobReference(fileName);

                Directory.CreateDirectory(path);

                blockBlob.DownloadToFile(Path.Combine(path,fileName), FileMode.Create);



            }
            catch (Exception ex)
            {
                GenericLogger.Error($"Exception {ex.Message} popped when trying to get Blob : {fileName} from container : {container}", ex);
            }




        }


    }
}
