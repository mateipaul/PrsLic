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
        private HtmlDocument _document;

        public CrawlProductParser(RetailerConfiguration retailerConfig)
        {
            RetailerConfig = retailerConfig;
        }

        internal void DownloadProduct(ref Product product)
        {
            try
            {
                CreateProductDocument(product);

                ExtractNeededInformation(ref product);

            }
            catch (Exception ex)
            {
                GenericLogger.Error($"Exception popped when trying to extract product information for {Url}", ex);
            }
            
        }


        private void ExtractNeededInformation(ref Product product)
        {
            product.Description = (HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductName, m => m.InnerText.Trim().Replace(",",string.Empty)));
            product.Price = (HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductPrice, m => Regex.Match(m.InnerText.Replace("&#46;", string.Empty), @"\d+.\d+(.\d+)?").Value.Trim()));
            product.Stock = (HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductStock, m => string.IsNullOrEmpty(m.InnerText)?"OutOfStock":"InStock"));
            product.ImageUrl = (HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductImage, m => m.GetAttributeValue("src",string.Empty)));

            PriceEvolution priceEvolution = new PriceEvolution();
            priceEvolution.Id = Guid.NewGuid();
            priceEvolution.Price = product.Price;
            priceEvolution.ProductID = product.Id;
            priceEvolution.UpdatedDate = DateTime.UtcNow;

            product.PriceEvolutions.Add(priceEvolution);
        }


        private void CreateProductDocument(Product prd)
        {
            string htmlProductPage = HttpUtils.GetWebRequestResponse(prd.Url);

            _document = HtmlDocumentUtilities.GetHtmlDocument(htmlProductPage);

            if(_document == null)
            {
                throw new Exception("Product Document Null Exception");
            }

        }
    }
}