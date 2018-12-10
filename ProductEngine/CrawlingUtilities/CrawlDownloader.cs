using DatabaseModels;
using HtmlAgilityPack;
using Logger;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UtilitiesModels;

namespace CrawlingUtilities
{
    public class CrawlDownloader
    {
        private RetailerConfiguration RetailerConfig;
        private Manufacturer manufacturer;
        private HtmlDocument doc;
        private HashSet<string> _productUrls;
        internal List<Product> crawlProducts;
        private string retailerHost;

        public CrawlDownloader(RetailerConfiguration retailerConfiguration,Manufacturer currentManufacturer)
        {
            RetailerConfig = retailerConfiguration;
            manufacturer = currentManufacturer;
            Initialize();
        }

        private void Initialize()
        {
            doc = new HtmlDocument();
            _productUrls = new HashSet<string>();
            crawlProducts = new List<Product>();
            retailerHost = Regex.Match(RetailerConfig.CrawlingTags.SearchUrlFormat,@"https://www.[a-z]+.[a-z]+([a-z]+)?").Value;
        }

       

        public void StartNavigation()
        {
            SearchManufacturer();

            GetProductUrls();

            StartParsing();

        }

        

        private void GetProductUrls()
        {
            try
            {
                do
                {

                    doc.DocumentNode.SelectNodes(RetailerConfig.CrawlingTags.UrlTag)
                                    .Select(productNode => productNode.GetAttributeValue("href", string.Empty))
                                    .ToList()
                                    .ForEach(productUrl =>
                                    {
                                        string tempURL = productUrl.Split('?').FirstOrDefault();
                                        if (!string.IsNullOrEmpty(tempURL))
                                            _productUrls.Add(tempURL);
                                    });

                } while (HasNextPage(ref doc));


            }
            catch (Exception ex)
            {
                GenericLogger.Error($"could not get products urls for {RetailerConfig.RetailerName}", ex);
            }
        }

        private string SurfNewPage(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception($"url can not be null. Occured in SurfNewPage");
            }
            return HttpUtils.GetHttpRequestResponse(url);
        }

        private bool HasNextPage( ref HtmlDocument doc)
        {
            string nextPageUrl = retailerHost + HtmlDocumentUtilities.ExtractNodeValue(doc, RetailerConfig.CrawlingTags.NextProductPageTag, node => node.GetAttributeValue("href", string.Empty));

            if (string.IsNullOrEmpty(nextPageUrl))
            {
                return false;
            }

            doc = HtmlDocumentUtilities.GetHtmlDocument(SurfNewPage(nextPageUrl));

            return true;
            
        }



        private void SearchManufacturer()
        {

            string html = HttpUtils.GetHttpRequestResponse(string.Format(RetailerConfig.CrawlingTags.SearchUrlFormat, manufacturer.Name));

            if (string.IsNullOrEmpty(html))
            {
                throw new Exception($"source string can not be null. Occured in SearchManufacturer({manufacturer.Name})");
            }
            doc = HtmlDocumentUtilities.GetHtmlDocument(html);

        }


        private void StartParsing()
        {

            Parallel.ForEach(_productUrls
                            , new ParallelOptions() { MaxDegreeOfParallelism = 1 }
                            , url => crawlProducts.Add(ExtractProductFrom(url)));

        }

        private Product ExtractProductFrom(string url)
        {
            CrawlProductParser parser = new CrawlProductParser(url, RetailerConfig);
            Product tempProd = parser.DownloadProduct();

            return tempProd;
        }
    }
}
