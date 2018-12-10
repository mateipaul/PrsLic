using Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiscUtilities
{
    public static class HttpUtils
    {
        private static HttpClient client = new HttpClient();

        public static string GetHttpRequestResponse(string url)
        {
            Thread.Sleep(2000);
            string html = string.Empty;
            try
            {
                html = client.GetStringAsync(url).Result;
            }
            catch (HttpRequestException requestException)
            {

                GenericLogger.Error($"Http Exception Popped for {url} when trying to GetHttpRequestResponse(string url)", requestException);
            }
            catch(Exception ex)
            {
                GenericLogger.Error("Generic exception Popped in GetHttpRequestResponse(string url)", ex);
            }

            return html;

        }

        public static string GetWebRequestResponse(string url)
        {
            string html = string.Empty;
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Accept = "text/html, */*";
                request.MaximumAutomaticRedirections = 10;
                request.AllowAutoRedirect = true;
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.110 Safari/537.36";
                request.Host = uri.Host;
                request.Accept = "Accept: text / html,application / xhtml + xml,application / xml; q = 0.9,image / webp,image / apng,*/*;q=0.8";
                request.Headers["Accept-Encoding"] = "gzip, deflate";
                request.Headers["Accept-Language"] = "en-US,en;q=0.9,ro;q=0.8";

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

                GenericLogger.Error($"WebException popped when tring to extract {url}, \n Ex : {webEx.Message} \n {webEx.StackTrace}");
            }
            catch(Exception ex)
            {
                GenericLogger.Error($"Exception {ex.Message} popped when trying to get source for {url}, \n \n {ex.StackTrace}");
            }
            return html;
        }
    }
}
