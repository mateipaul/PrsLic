using DatabaseModels;
using Interfaces;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;

namespace MediaGalaxyDownloader
{
    public class MediaGalaxyDownloader : IDownloader
    {
        private RetailerConfiguration retailerConfiguration;

        public MediaGalaxyDownloader(RetailerConfiguration retailer)
        {
            retailerConfiguration = retailer;
        }

        public void GetProducts(CloudQueueMessage givenMessage)
        {
            return ;
        }

        public void Navigate()
        {
                
        }
    }
}
