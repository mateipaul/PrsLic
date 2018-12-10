using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;
using MiscUtilities;
using UtilitiesModels;

namespace SearchUtilities
{
    public static class SearchMessageCrawler
    {
        private static string url;
        internal static void StartMessageCrawling(CloudQueueMessage message, RetailerConfiguration retailer)
        {
            url = string.Format(retailer.CrawlingTags.SearchUrlFormat, message.AsString.ToLowerInvariant().Replace(" ","+"));

            NavigateLink();
        }

        private static void NavigateLink()
        {
            string htmlSting = HttpUtils.GetWebRequestResponse(url);

            var htmlDocument = HtmlDocumentUtilities.GetHtmlDocument(htmlSting);


        }
    }
}
