using System;
using DatabaseModels;
using HtmlAgilityPack;
using Logger;
using MiscUtilities;
using UtilitiesModels;

namespace CrawlingUtilities
{
    internal class CrawlProductParser
    {
        private string Url;
        private RetailerConfiguration RetailerConfig;
        private Product Product;
        private HtmlDocument _document;

        public CrawlProductParser(string url, RetailerConfiguration retailerConfig)
        {
            Url = url;
            RetailerConfig = retailerConfig;
            Product = new Product(url);
        }

        internal Product DownloadProduct()
        {
            try
            {
                CreateProductDocument();

                ExtractNeededInformation();

                return Product;
            }
            catch (Exception ex)
            {
                GenericLogger.Error($"Exception popped when trying to extract product information for {Url}", ex);
                return null;
            }
            
        }


        private void ExtractNeededInformation()
        {
            
        }


        private void CreateProductDocument()
        {
            string htmlProductPage = HttpUtils.GetHttpRequestResponse(Url);

            _document = HtmlDocumentUtilities.GetHtmlDocument(htmlProductPage);

            if(_document == null)
            {
                throw new Exception("Product Document Null Exception");
            }

        }
    }
}