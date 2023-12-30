

using Natural.Core.Models;

namespace Natural.Core.IServices

{
    public interface IHttpClientWrapper
    {
        public Task<T> GetAsync<T>(string endpoint);

        public Task<T> PostAsync<T>(string endpoint, object model);
        public Task<T> GetByIdAsync<T>(string endpoint, string id);
        Task<T> PutAsync<T>(string endpoint, string Id, object model);
        Task<bool> DeleteAsync(string endpoint, string id);





        Task<T> GetRetailorAsync<T>(string retailorId);
        Task<T> GetDistributorAsync<T>(string distributorId);


    }
}




