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

        internal void DownloadProduct(ref Produs product)
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


        private void ExtractNeededInformation(ref Produs product)
        {
            product.Denumire = (HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductName, m => m.InnerText.Trim().Replace(",",string.Empty)));
            product.Pret = (HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductPrice, m => Regex.Match(m.InnerText.Replace("&#46;", string.Empty), @"\d+.\d+(.\d+)?").Value.Trim()));
            product.Stock = (HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductStock, m => string.IsNullOrEmpty(m.InnerText)?"OutOfStock":"InStock"));
            product.Url_Imagine = (HtmlDocumentUtilities.ExtractNodeValue(_document, RetailerConfig.CrawlingTags.ProductImage, m => m.GetAttributeValue("src",string.Empty)));

            EvolutiaPretului priceEvolution = new EvolutiaPretului();
            priceEvolution.Id = Guid.NewGuid();
            priceEvolution.Pret = product.Pret;
            priceEvolution.Id_Produs = product.Id;
            priceEvolution.Data_Actualizare = DateTime.UtcNow;

            product.EvolutiaPretului.Add(priceEvolution);
        }


        private void CreateProductDocument(Produs prd)
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