using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreWakeMeUp.Endpoint
{
    public class ConnectPoint
    {
        public static async Task<KeyValuePair<HttpStatusCode, string>> HttpGetData(string url)
        {
            string result = null;
            HttpStatusCode statusCode;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                statusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                }
            }

            return new KeyValuePair<HttpStatusCode, string>(statusCode, result);
        }

        public static async Task<KeyValuePair<HttpStatusCode, byte[]>> HttpGetImage(string url)
        {
            byte [] result = null;
            HttpStatusCode statusCode;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                statusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        result = await content.ReadAsByteArrayAsync();
                    }
                }
            }

            return new KeyValuePair<HttpStatusCode, byte[]>(statusCode, result);
        }
    }
}