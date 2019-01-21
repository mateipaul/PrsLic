using DatabaseModels;
using Interfaces;
using Microsoft.WindowsAzure.Storage.Queue;
using MiscUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesModels;
using System.Web;
using System.Net;
using System.IO;

namespace MediaGalaxyDownloader
{
    public class MediaGalaxyDownloader : IDownloader
    {
        private RetailerConfiguration retailerConfiguration;
        private string givenMessage;

        public MediaGalaxyDownloader(RetailerConfiguration retailer)
        {
            retailerConfiguration = retailer;
        }

        public void GetProducts(CloudQueueMessage givenMessage)
        {
            var url = GetSearchUrl(givenMessage,retailerConfiguration);
            var productResponse = JsonUtilities.Instance.DeserializeJson<MediGalaxyResponseStructure>(GetResponseForUrl(url));
        }

        private string GetResponseForUrl(string url)
        {
            string html = string.Empty;
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Accept = "application/json";
                request.KeepAlive = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";
                request.Host = "cerberus.mediagalxy.ro";
                request.Headers.Add("Origin", "https://mediagalaxy.ro");
                request.Referer = "https://mediagalxy.ro";
                request.Headers["Accept-Encoding"] = "gzip, deflate";
                request.Headers["Accept-Language"] = "en-US,en;q=0.9,ro;q=0.8";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            html = reader.ReadToEnd();
                        }
                    }
                }

            }
            catch (WebException webEx)
            {

            }
            catch (Exception ex)
            {
            
            }
            return html;
        }

        private string GetSearchUrl(CloudQueueMessage message, RetailerConfiguration retailer)
        {
            string searchIdiom = message.AsString.Split('|').FirstOrDefault();
            string searchType = message.AsString.Split('|').LastOrDefault();

            givenMessage = searchIdiom.Replace(" ","%20");

            switch (searchType)
            {
                case "price-asc":
                    {
                        return string.Format(retailer.CrawlingTags.SearchUrlFormatPriceAsc, givenMessage.ToLowerInvariant());
                    }
                case "price-desc":
                    {
                        return string.Format(retailer.CrawlingTags.SearchUrlFormatPriceDesc, givenMessage.ToLowerInvariant());
                    }
                default:
                    return string.Format(retailer.CrawlingTags.SearchUrlFormatDefault, givenMessage.ToLowerInvariant());
            }
        }

    }
}
