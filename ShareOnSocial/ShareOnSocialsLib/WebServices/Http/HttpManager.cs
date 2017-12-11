using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShareOnSocialsLib.WebServices.Http
{
    public class HttpManager : IHttpManager 
    {
        public async Task<string> SendWithAuthHeader(string methodtype, string url, string header)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage message = new HttpRequestMessage())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", header);
                    message.RequestUri = new Uri(url);
                    message.Method = new HttpMethod(methodtype);
                    HttpResponseMessage response = await client.SendAsync(message);
                    string json = await response.Content.ReadAsStringAsync();
                    return json;
                }
            }
        }
    }
}
