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

        internal Product GetProduct()
        {
            Product prd = new Product();

            prd.Url = url;
            prd.Description = HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductName, m => m.InnerText.Replace(",",string.Empty));
            prd.Deleted = false;
            prd.Id = Guid.NewGuid();
            prd.ImageUrl = HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductImage, m => m.GetAttributeValue("src", string.Empty));
            prd.ProductDescriptionCode = GetDescriptionCode(prd.Description);
            prd.Price = (HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductPrice, m => Regex.Match(m.InnerText.Replace("&#46;", string.Empty), @"\d+.\d+(.\d+)?").Value.Trim()));
            prd.Stock = (HtmlDocumentUtilities.ExtractNodeValue(doc, retailer.CrawlingTags.ProductStock, m => string.IsNullOrEmpty(m.InnerText)?"OutOfStock":"InStock"));
            prd.Id = GetProductID(url);



            return prd;
        }

        private Guid GetProductID(string url)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(url));
            return new Guid(data);
        }

        private string GetDescriptionCode(string description)
        {
            var pieces = description.Trim().ToUpper().Split(' ');
            int completeSearchCode = 1;
            foreach (var piece in pieces)
            {
                try
                {
                    completeSearchCode = completeSearchCode ^ piece.GetHashCode();
                }
                catch (Exception ex)
                { }
            }

            return completeSearchCode.ToString();

        }
    }
}
