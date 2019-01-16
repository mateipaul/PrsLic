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
        private HtmlDocument doc;
        private HashSet<string> _productUrls;
        internal List<Produs> crawlProducts;
        private string retailerHost;

        public CrawlDownloader(RetailerConfiguration retailerConfiguration)
        {
            RetailerConfig = retailerConfiguration;
            Initialize();
        }

        private void Initialize()
        {
            doc = new HtmlDocument();
            _productUrls = new HashSet<string>();
            crawlProducts = new List<Produs>();
            retailerHost = Regex.Match(RetailerConfig.CrawlingTags.SearchUrlFormatDefault,@"https://www.[a-z]+.[a-z]+([a-z]+)?").Value;
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

            string html = HttpUtils.GetHttpRequestResponse(string.Format(RetailerConfig.CrawlingTags.SearchUrlFormatDefault));

            if (string.IsNullOrEmpty(html))
            {

            }
            doc = HtmlDocumentUtilities.GetHtmlDocument(html);

        }


        private void StartParsing()
        {

            Parallel.ForEach(_productUrls
                            , new ParallelOptions() { MaxDegreeOfParallelism = 1 }
                            , url => crawlProducts.Add(ExtractProductFrom(url)));

        }

        private Produs ExtractProductFrom(string url)
        {
            //CrawlProductParser parser = new CrawlProductParser(url, RetailerConfig);
            //Product tempProd = parser.DownloadProduct();

            //return tempProd;
            return null;
        }
    }
}
