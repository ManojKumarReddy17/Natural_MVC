

using Natural.Core.Models;

namespace Natural.Core.IServices

{
    public interface IHttpClientWrapper
    {
        Task<T> GetAsync<T>(string endpoint);
        //Task<T> PostAsync<T>(string endpoint, object model);

         Task<T> PutAsync<T>(string endpoint, object model);
        public Task<T> PostAsync<T>(string endpoint, object model);
        public Task<T>GetByIdAsync<T>(string endpoint,string Id);
        public Task<T> PutAsync<T>(string endpoint, string Id, object model);
        Task <bool>DeleteAsync(string endpoint, string categoryId);
    }
        public Task<T> GetByIdAsync<T>(string endpoint, string id);
        Task<bool> DeleteAsync(string endpoint, String Id);
       // Task<T> GetExecutiveDetailsByID<T>(string ID);
    }  
}
