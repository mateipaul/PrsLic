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

namespace CelParser
{
    public class CelProductParser:IParseProduct
    {

        private string url;
        private string givenMessage;
        private RetailerConfiguration retailer;
        private HtmlDocument doc;

        private RetailerConfiguration updateProductConfiguration;
        private HtmlDocument updateDocument;

        public CelProductParser(string url, string givenMessage, RetailerConfiguration retailer)
        {
            this.url = url;
            this.givenMessage = givenMessage;
            this.retailer = retailer;
            doc = GetProductDocument(url);
        }

        private HtmlDocument GetProductDocument(string url)
        {
            string html = HttpUtils.GetWebRequestResponse(url);
            doc = HtmlDocumentUtilities.GetHtmlDocument(html);
            return doc;
        }

        public Produs GetProduct()
        {
            Produs prd = new Produs();

            prd.Url = url;
            prd.Denumire = HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductName, m => m.InnerText.Replace(",", string.Empty));
            prd.Url_Imagine = HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductImage, m => m.GetAttributeValue("src", string.Empty));
            prd.Pret = decimal.Parse((HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductPrice, m => Regex.Match(m.InnerText.Replace("&#46;", string.Empty), @"\d+.\d+(.\d+)?").Value.Trim())));
            prd.Stock = (HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductStock, m => string.IsNullOrEmpty(m.InnerText) ? "OutOfStock" : "InStock"));

            return prd;
        }

       

        public CelProductParser(RetailerConfiguration retailer)
        {
            updateProductConfiguration = retailer;
        }

        public void GetProduct(ref Produs product)
        {
            updateDocument = GetProductDocument(product.Url);

            ExtractNeededInformation(ref product, updateDocument);
        }

        private void ExtractNeededInformation(ref Produs product,HtmlDocument _document)
        {
            product.Denumire = (HtmlDocumentUtilities.ExtractNodeValue(_document, updateProductConfiguration.CrawlingTags.ProductName, m => m.InnerText.Trim().Replace(",", string.Empty)));
            product.Pret = Decimal.Parse((HtmlDocumentUtilities.ExtractNodeValue(_document, updateProductConfiguration.CrawlingTags.ProductPrice, m => Regex.Match(m.InnerText.Replace("&#46;", string.Empty), @"\d+.\d+(.\d+)?").Value.Trim())));
            product.Stock = (HtmlDocumentUtilities.ExtractNodeValue(_document, updateProductConfiguration.CrawlingTags.ProductStock, m => string.IsNullOrEmpty(m.InnerText) ? "OutOfStock" : "InStock"));
            product.Url_Imagine = (HtmlDocumentUtilities.ExtractNodeValue(_document, updateProductConfiguration.CrawlingTags.ProductImage, m => m.GetAttributeValue("src", string.Empty)));

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
