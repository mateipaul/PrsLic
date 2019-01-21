using DatabaseModels;
using Interfaces;
using Microsoft.WindowsAzure.Storage.Queue;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;
using System.Web;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace MediaGalaxyDownloader
{
    public class PcGarageDownloader : IDownloader
    {
        private RetailerConfiguration retailerConfiguration;
        private string givenMessage;
        private HtmlDocument document;
        private RetailerCrawlProductCollection products;

        public PcGarageDownloader(RetailerConfiguration retailer)
        {
            retailerConfiguration = retailer;
        }

        public void GetProducts(CloudQueueMessage givenMessage)
        {
            var url = GetSearchUrl(givenMessage,retailerConfiguration);
            document = HtmlDocumentUtilities.GetHtmlDocument(GetResponseForUrl(url));

            ExtractProducts(document);
        }

        private void ExtractProducts(HtmlDocument document)
        {
            products = new RetailerCrawlProductCollection(retailerConfiguration.RetailerName);



        }

        private string GetResponseForUrl(string url)
        {
            string html = string.Empty;

            html = HttpUtils.GetWebRequestResponse(url);

            return html;

        }

        private string GetSearchUrl(CloudQueueMessage message, RetailerConfiguration retailer)
        {
            string searchIdiom = message.AsString.Split('|').FirstOrDefault();
            string searchType = message.AsString.Split('|').LastOrDefault();

            givenMessage = searchIdiom.Replace(" ","%20");

            switch (searchType)
            {
                case "price-asc":
                    {
                        return string.Format(retailer.CrawlingTags.SearchUrlFormatPriceAsc, givenMessage.ToLowerInvariant());
                    }
                case "price-desc":
                    {
                        return string.Format(retailer.CrawlingTags.SearchUrlFormatPriceDesc, givenMessage.ToLowerInvariant());
                    }
                default:
                    return string.Format(retailer.CrawlingTags.SearchUrlFormatDefault, givenMessage.ToLowerInvariant());
            }
        }

    }
}
