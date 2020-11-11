using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace CoordiNet
{
    public static class Oauth2Handler
    {
        public static async Task<string> CreateBearerToken(string clientId, string clientSecret)
        {
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                return default;

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "<URL>");
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "grant_type", "client_credentials" }
                });

                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
                    var token = payload.Value<string>("access_token");
                    return token;
                }
            }

            return default;
        }
    }
}
