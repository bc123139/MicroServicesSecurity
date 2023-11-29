using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MvcWebClient.Http
{
    public class GeneralHttpClient : IHttpClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public async Task<string> GetStringAsync(string uri)
        {
            var requestMessage=new HttpRequestMessage(HttpMethod.Get, uri);
            var response=await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item),Encoding.UTF8,"application/json")
            };
            var response = await _httpClient.SendAsync(requestMessage);
            if(response.StatusCode==System.Net.HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }
            return response;
        }
    }
}
