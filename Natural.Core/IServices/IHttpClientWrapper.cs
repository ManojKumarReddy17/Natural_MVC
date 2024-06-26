﻿

using Natural.Core.Models;

namespace Natural.Core.IServices

{
    public interface IHttpClientWrapper
    {
        public Task<T> GetAsync<T>(string endpoint);
        

        public Task<T> PostAsync<T>(string endpoint, object model);
        public Task<T> GetByIdAsync<T>(string endpoint, object id);
        Task<T> PutAsync<T>(string endpoint, string Id, object model);
        Task<bool> DeleteAsync(string endpoint, string id);
        Task<T> PostMultipartFormData<T>(string endpoint, MultipartFormDataContent model);
        Task<T> PutMultipartFormData<T>(string endpoint, MultipartFormDataContent model);
        Task<string> DeleteAssignedDistirbutorAsync(string endpoint,string id);
        Task<string> GetIdAsync(string endpoint, string id);
        Task<T> SearchAsync<T>(string endpoint, object model, bool? NonAssign);
        Task<bool> EntireDeleteAsync(string v, string productId, bool deleteEntireProduct);
    }
}




