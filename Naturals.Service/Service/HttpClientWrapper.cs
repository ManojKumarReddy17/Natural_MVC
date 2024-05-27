
using Microsoft.Extensions.Options;

using Natural.Core.IServices;
using Natural.Core.Models;

using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
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
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + endpoint);   // reponse is api output in  json
            var responseContent = await response.Content.ReadAsStringAsync();   // we are convertimg  response to single string 
            return JsonConvert.DeserializeObject<T>(responseContent); // we are converting the string to required model
        }


        /// <summary>
        /// GET BY ID ASYNC
        /// </summary>
        public async Task<T> GetByIdAsync<T>(string endpoint, object id)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{endpoint}{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseContent);



        }


        public async Task<string> GetIdAsync(string endpoint, string id)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{endpoint}{id}");

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
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
        //This Is For Product Post Method
        /// </summary>
        public async Task<T> PostMultipartFormData<T>(string endpoint, MultipartFormDataContent model)
        {
            var response = await _httpClient.PostAsync(_httpClient.BaseAddress + endpoint, model);
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

        /// </summary>
        //This Is For Product Put Method
        /// </summary>
        public async Task<T> PutMultipartFormData<T>(string endpoint, MultipartFormDataContent model)
        {
            var response = await _httpClient.PutAsync(_httpClient.BaseAddress + endpoint, model);
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


        public async Task<string> DeleteAssignedDistirbutorAsync(string endpoint, string id)
        {
            var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}{endpoint}/{id}");
            var responseData = await response.Content.ReadAsStringAsync();

            return responseData;
        }

        public async Task<T> SearchAsync<T>(string endpoint, object model, bool? NonAssign)
        {
            var properties = model.GetType().GetProperties();
            var keyValuePairs = properties
                .Where(p => p.GetValue(model) != null)
                .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(model).ToString())}");

            var queryString = string.Join("&", keyValuePairs);
            if (!string.IsNullOrEmpty(queryString) && NonAssign != true)
            {

                //var queryString = model != null ? $"?model={Uri.EscapeDataString(JsonConvert.SerializeObject(model))}" : "";
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{endpoint}{queryString}");// reponse is api output in  json
                var responseContent = await response.Content.ReadAsStringAsync();   // we are convertimg  response to single string 
                return JsonConvert.DeserializeObject<T>(responseContent);
            };
            if(!string.IsNullOrEmpty(queryString) && NonAssign == true)
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{endpoint}{queryString}&nonAssign={NonAssign}");// reponse is api output in  json
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);

            }
            else
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{endpoint}{NonAssign}");
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            
        }


        public void Dispose()
        {
            _httpClient.Dispose();
        }

    }
}