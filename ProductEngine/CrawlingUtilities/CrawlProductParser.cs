using System;
using System.Text.RegularExpressions;
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
            Product.SetDescription(HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductName, m => m.InnerText.Trim()));
            Product.SetPrice(HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductPrice, m => Regex.Match(m.InnerText.Replace("&#46;", string.Empty), @"\d+.\d+(.\d+)?").Value.Trim()));
            Product.SetStock(HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductStock, m => string.IsNullOrEmpty(m.InnerText)?"OutOfStock":"InStock"));
            Product.SetImageUrl(HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductImage, m => m.GetAttributeValue("scr",string.Empty)));
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