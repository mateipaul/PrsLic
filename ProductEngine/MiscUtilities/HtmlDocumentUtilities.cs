using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscUtilities
{
    public static class HtmlDocumentUtilities
    {
       

        public static HtmlDocument GetHtmlDocument(string htmlSourceString)
        {
             HtmlDocument doc = new HtmlDocument();

            if (string.IsNullOrEmpty(htmlSourceString))
            {
                return null;
            }

            doc.LoadHtml(htmlSourceString);

            return doc;
        }

        public static string ExtractNodeValue(HtmlDocument doc,string xPath, Func<HtmlNode, string> func)
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
