
using Microsoft.Extensions.Options;

using Natural.Core.IServices;
using Natural.Core.Models;

using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

#nullable disable

namespace Naturals.Service.Service
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        public ApiDetails _ApiDetails { get; set; }
        public HttpClientWrapper(IOptions<ApiDetails> apiDetails, HttpClient httpClient)
        {
            _ApiDetails = apiDetails.Value;
            _httpClient = httpClient;
            _httpClient = new HttpClient { BaseAddress = new Uri(_ApiDetails.Natrual_API) };
            _httpClient.DefaultRequestHeaders.Add("ApiKey", _ApiDetails.ApiKey);

        }

        /// <summary>
        /// GET ASYNC
        /// </summary>
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + endpoint);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        /// <summary>
        /// POST ASYNC
        /// </summary>
        public async Task<T> PostAsync<T>(string endpoint, object model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_httpClient.BaseAddress + endpoint, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        /// <summary>
        /// GET BY ID ASYNC
        /// </summary>
        public async Task<T> GetByIdAsync<T>(string endpoint, string id)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{endpoint}/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        /// <summary>
        /// PUT ASYNC
        /// </summary>
        public async Task<T> PutAsync<T>(string endpoint, string Id, object model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}{endpoint}/{Id}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        /// <summary>
        /// DELETE ASYNC
        /// </summary>
        public async Task<bool> DeleteAsync(string endpoint, string id)
        {
            var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}{endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

    }
}