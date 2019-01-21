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
using PcGarageParser;

namespace MediaGalaxyDownloader
{
    public class PcGarageDownloader : IDownloader
    {
        private RetailerConfiguration retailerConfiguration;
        private string givenMessage;
        private HtmlDocument document;
        private RetailerCrawlProductCollection products;
        private List<string> prodUrls;

        public PcGarageDownloader(RetailerConfiguration retailer)
        {
            retailerConfiguration = retailer;
        }

        public void GetProducts(CloudQueueMessage givenMessage)
        {
            var url = GetSearchUrl(givenMessage,retailerConfiguration);
            document = HtmlDocumentUtilities.GetHtmlDocument(GetResponseForUrl(url));

            ExtractProducts(document);

            ParseProducts();

            products.SaveProducts(this.givenMessage);
        }

        private void ParseProducts()
        {

            foreach (var url in prodUrls)
            {
                PcGarageProductParser parser = new PcGarageProductParser(url, givenMessage, retailerConfiguration);

                products.AddProduct(parser.GetProduct());
            }
        }

        private void ExtractProducts(HtmlDocument document)
        {
            products = new RetailerCrawlProductCollection(retailerConfiguration.RetailerName);

            prodUrls = document.DocumentNode.SelectNodes(retailerConfiguration.CrawlingTags.UrlTag).Select(M => M.GetAttributeValue("href", string.Empty)).ToList();

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

            givenMessage = searchIdiom;

            switch (searchType)
            {
                case "price-asc":
                    {
                        return string.Format(retailer.CrawlingTags.SearchUrlFormatPriceAsc, searchIdiom.Replace(" ", "%20").ToLowerInvariant());
                    }
                case "price-desc":
                    {
                        return string.Format(retailer.CrawlingTags.SearchUrlFormatPriceDesc, searchIdiom.Replace(" ", "%20").ToLowerInvariant());
                    }
                default:
                    return string.Format(retailer.CrawlingTags.SearchUrlFormatDefault, searchIdiom.Replace(" ", "%20").ToLowerInvariant());
            }
        }

    }
}
