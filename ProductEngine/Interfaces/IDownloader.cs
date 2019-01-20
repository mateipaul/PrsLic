using DatabaseModels;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;

namespace Interfaces
{
    public interface IDownloader
    {
            void GetProducts(CloudQueueMessage givenMessage);
    }
}
