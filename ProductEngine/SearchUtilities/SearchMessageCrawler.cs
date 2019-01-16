using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels;
using HtmlAgilityPack;
using Logger;
using Microsoft.WindowsAzure.Storage.Queue;
using MiscUtilities;
using UtilitiesModels;

namespace SearchUtilities
{
    public static class SearchMessageCrawler
    {
        private static string url;
        private static string givenMessage;
        private static RetailerConfiguration retConfig;
        private static RetailerCrawlProductCollection products;

        internal static void StartMessageCrawling(CloudQueueMessage message, RetailerConfiguration retailer)
        {
            url = GetSearchUrl(message, retailer);
            givenMessage = message.AsString.Split('|').FirstOrDefault();
            retConfig = retailer;
            products = new RetailerCrawlProductCollection(retailer.RetailerName);

            NavigateLink();
        }

        private static string GetSearchUrl(CloudQueueMessage message, RetailerConfiguration retailer)
        {
            string searchIdiom = message.AsString.Split('|').FirstOrDefault();
            string searchType = message.AsString.Split('|').LastOrDefault();

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

        private static void NavigateLink()
        {
            string htmlSting = HttpUtils.GetWebRequestResponse(url);

            var htmlDocument = HtmlDocumentUtilities.GetHtmlDocument(htmlSting);

            ExtractProducts(htmlDocument);

            products.SaveProducts(givenMessage);
        }

        private static void ExtractProducts(HtmlDocument htmlDocument)
        {
            try
            {
                var productUrls = htmlDocument.DocumentNode.SelectNodes(retConfig.CrawlingTags.UrlTag)
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

        private static void ExtractProductInformation(List<string> productUrls)
        {
            foreach (var url in productUrls)
            {
                SearchProductParser parser = new SearchProductParser(url, givenMessage,retConfig);
                products.AddProduct(parser.GetProduct());
            }
        }
    }
}
