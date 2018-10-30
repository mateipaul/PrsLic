using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
