using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelDonwloader;
using DatabaseModels;
using HtmlAgilityPack;
using Interfaces;
using Logger;
using MediaGalaxyDownloader;
using Microsoft.WindowsAzure.Storage.Queue;
using MiscUtilities;
using UtilitiesModels;

namespace SearchUtilities
{
    public static class SearchMessageCrawler
    {
        private static string url;
        private static CloudQueueMessage queueMessage;
        private static string givenMessage;
        private static RetailerConfiguration retConfig;
        private static RetailerCrawlProductCollection products;
        private static IDownloader downloader;

        internal static void StartMessageCrawling(CloudQueueMessage message, RetailerConfiguration retailer)
        {
            queueMessage = message;
            givenMessage = message.AsString.Split('|').FirstOrDefault();
            retConfig = retailer;
            products = new RetailerCrawlProductCollection(retailer.RetailerName);

            downloader = GenerateDownloader(retailer);

            DownloadProducts(downloader, retailer);
        }

        private static void DownloadProducts(IDownloader downloader,RetailerConfiguration retailer)
        {
            try
            {
                downloader.GetProducts(queueMessage);
            }
            catch (Exception ex)
            {

                
            }
        }

        private static IDownloader GenerateDownloader(RetailerConfiguration retailer)
        {
            try
            {
                switch (retailer.RetailerName)
                {
                    case "Cel":
                        {
                            return new CelDownloader(retailer);
                        }
                    case "MediaGalaxy":
                        {
                            return new MediaGalaxyDownloader.PcGarageDownloader(retailer);
                        }

                    default:
                        {
                            return null;
                        }
                }
            }
            catch (Exception ex)
            {
                GenericLogger.Error($"could not create downloader for {retailer.RetailerName}");
                return null;
            }
        }

       
    }
}
