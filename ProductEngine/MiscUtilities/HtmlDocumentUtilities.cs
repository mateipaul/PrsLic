using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscUtilities
{
    public  class HtmlDocumentUtilities
    {
        private HtmlDocument doc;

        public HtmlDocument GetHtmlDocument(string htmlSourceString)
        {
            if (string.IsNullOrEmpty(htmlSourceString))
            {
                return null;
            }
            doc = new HtmlDocument();

            doc.LoadHtml(htmlSourceString);

            return doc;
        }

        private string ExtractNodeValue(string xPath, Func<HtmlNode, string> func)
        {
            try
            {
                if (string.IsNullOrEmpty(xPath))
                    return string.Empty;

                var node = doc.DocumentNode.SelectSingleNode(xPath);

                return (func != null && node != null) ? func(node) : string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


    }
}
