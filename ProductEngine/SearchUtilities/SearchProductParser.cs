using DatabaseModels;
using HtmlAgilityPack;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UtilitiesModels;

namespace SearchUtilities
{
    class SearchProductParser
    {
        private string url;
        private string givenMessage;
        private RetailerConfiguration retailer;
        private HtmlDocument doc;

        public SearchProductParser(string url, string givenMessage,RetailerConfiguration retailer)
        {
            this.url = url;
            this.givenMessage = givenMessage;
            this.retailer = retailer;
            GetProductDocument();
        }

        private void GetProductDocument()
        {
            string html = HttpUtils.GetWebRequestResponse(url);
            doc = HtmlDocumentUtilities.GetHtmlDocument(html);
        }

        internal Produs GetProduct()
        {
            Produs prd = new Produs();

            prd.Url = url;
            prd.Denumire = HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductName, m => m.InnerText.Replace(",",string.Empty));
            prd.Url_Imagine = HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductImage, m => m.GetAttributeValue("src", string.Empty));
            prd.Pret = decimal.Parse((HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductPrice, m => Regex.Match(m.InnerText.Replace("&#46;", string.Empty), @"\d+.\d+(.\d+)?").Value.Trim())));
            prd.Stock = (HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductStock, m => string.IsNullOrEmpty(m.InnerText)?"OutOfStock":"InStock")); 

            return prd;
        }

       
    }
}
