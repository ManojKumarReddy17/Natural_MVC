

using Natural.Core.Models;

namespace Natural.Core.IServices

{
    public interface IHttpClientWrapper
    {
        public Task<T> GetAsync<T>(string endpoint);

        public Task<T> PostAsync<T>(string endpoint, object model);
        public Task<T> GetByIdAsync<T>(string endpoint, string id);
        Task<bool> DeleteAsync(string endpoint, String Id);
       // Task<T> GetExecutiveDetailsByID<T>(string ID);
    }  
}
