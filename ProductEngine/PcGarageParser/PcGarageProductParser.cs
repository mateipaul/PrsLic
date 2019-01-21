using DatabaseModels;
using HtmlAgilityPack;
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
    public class PcGarageProductParser
    {
        private string url;
        private string givenMessage;
        private RetailerConfiguration retailer;
        private HtmlDocument doc;

        public PcGarageProductParser(string url, string givenMessage, RetailerConfiguration retailer)
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

    }
}
