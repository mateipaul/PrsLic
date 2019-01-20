using CelParser;
using DatabaseModels;
using HtmlAgilityPack;
using Interfaces;
using Logger;
using Microsoft.WindowsAzure.Storage.Queue;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;

namespace CelDonwloader
{
    public class CelDownloader : IDownloader
    {
        private RetailerCrawlProductCollection products;
        private RetailerConfiguration retailerConfiguration;
        private string givenMessage;
        private string url;

        public CelDownloader(RetailerConfiguration retailer)
        {
            retailerConfiguration = retailer;
            products = new RetailerCrawlProductCollection(retailer.RetailerName);
        }

        public void GetProducts(CloudQueueMessage message)
        {
            

            url = GetSearchUrl(message, retailerConfiguration);

            NavigateLink(url);
        }

        private void NavigateLink(string url)
        {
            string htmlSting = HttpUtils.GetWebRequestResponse(url);

            var htmlDocument = HtmlDocumentUtilities.GetHtmlDocument(htmlSting);

            ExtractProducts(htmlDocument);

            products.SaveProducts(givenMessage);
        }

        private  void ExtractProducts(HtmlDocument htmlDocument)
        {
            try
            {
                var productUrls = htmlDocument.DocumentNode.SelectNodes(retailerConfiguration.CrawlingTags.UrlTag)
                    .Select(m => m.GetAttributeValue("href", string.Empty)).ToList();

                if (productUrls.Count < 1)
                {
                    GenericLogger.Info($"No products were found for {givenMessage}");
                    return;
                }
                ExtractProductInformation(productUrls);
            }
            catch (Exception ex)
            {
            }
        }

        private void ExtractProductInformation(List<string> productUrls)
        {
            foreach (var url in productUrls)
            {
                var parser = new CelProductParser(url, givenMessage, retailerConfiguration);
                products.AddProduct(parser.GetProduct());
            }
        }

        private string GetSearchUrl(CloudQueueMessage message, RetailerConfiguration retailer)
        {
            string searchIdiom = message.AsString.Split('|').FirstOrDefault();
            string searchType = message.AsString.Split('|').LastOrDefault();

            givenMessage = searchIdiom;

            switch (searchType)
            {
                case "price-asc":
                    {
                        return string.Format(retailer.CrawlingTags.SearchUrlFormatPriceAsc, searchIdiom.ToLowerInvariant().Replace(" ", "+"));
                    }
                case "price-desc":
                    {
                        return string.Format(retailer.CrawlingTags.SearchUrlFormatPriceDesc, searchIdiom.ToLowerInvariant().Replace(" ", "+"));
                    }
                default:
                    return string.Format(retailer.CrawlingTags.SearchUrlFormatDefault, searchIdiom.ToLowerInvariant().Replace(" ", "+"));
            }
        }
    }
}
