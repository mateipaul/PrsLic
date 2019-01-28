using DatabaseModels;
using HtmlAgilityPack;
using Interfaces;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UtilitiesModels;

namespace PcGarageParser
{
    public class PcGarageProductParser:IParseProduct
    {
        private string url;
        private string givenMessage;
        private RetailerConfiguration retailer;
        private HtmlDocument doc;

        private RetailerConfiguration updateConfiguration;
        private HtmlDocument _document;

        public PcGarageProductParser(string url, string givenMessage, RetailerConfiguration retailer)
        {
            this.url = url;
            this.givenMessage = givenMessage;
            this.retailer = retailer;
            doc = GetProductDocument(url);
        }

        private HtmlDocument GetProductDocument(string url)
        {
            string html = HttpUtils.GetWebRequestResponse(url);
            return HtmlDocumentUtilities.GetHtmlDocument(html);
        }

        public Produs GetProduct()
        {
           
                Produs prd = new Produs();
            try
            {
                prd.Url = url;
                prd.Denumire = HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductName, m => m.InnerText.Replace(",", string.Empty));
                prd.Url_Imagine = HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductImage, m => m.GetAttributeValue("src", string.Empty));
                prd.Pret = decimal.Parse((HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductPrice, m => Regex.Match(m.GetAttributeValue("content", string.Empty), @"\d+.\d{2}").Value.Trim())));
                prd.Stock = (HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductStock, m => !m.GetAttributeValue("href", string.Empty).Contains("InStock") ? "OutOfStock" : "InStock"));
            }
            catch(Exception ex)
            {

            }
            return prd;
        }

        public PcGarageProductParser(RetailerConfiguration retailerConfiguration)
        {
            updateConfiguration = retailerConfiguration;
        }
        public void GetProduct(ref Produs product)
        {
            _document = GetProductDocument(product.Url);
            ExtractNeededInformation(ref product);
        }

        private void ExtractNeededInformation(ref Produs product)
        {
            product.Denumire = HtmlDocumentUtilities.ExtractNodeValue(_document, updateConfiguration.CrawlingTags.ProductName, m => m.InnerText.Replace(",", string.Empty));
            product.Url_Imagine = HtmlDocumentUtilities.ExtractNodeValue(_document, updateConfiguration.CrawlingTags.ProductImage, m => m.GetAttributeValue("src", string.Empty));
            product.Pret = decimal.Parse((HtmlDocumentUtilities.ExtractNodeValue(_document, updateConfiguration.CrawlingTags.ProductPrice, m => Regex.Match(m.GetAttributeValue("content", string.Empty), @"\d+.\d{2}").Value.Trim())));
            product.Stock = (HtmlDocumentUtilities.ExtractNodeValue(_document, updateConfiguration.CrawlingTags.ProductStock, m => !m.GetAttributeValue("href", string.Empty).Contains("InStock") ? "OutOfStock" : "InStock"));

            EvolutiaPretului priceEvolution = new EvolutiaPretului();
            priceEvolution.Id = Guid.NewGuid();
            priceEvolution.Pret = product.Pret;
            priceEvolution.Id_Produs = product.Id;
            priceEvolution.Data_Actualizare = DateTime.UtcNow;
            priceEvolution.Produs = product;

            product.EvolutiaPretului.Add(priceEvolution);
        }
    }
}
