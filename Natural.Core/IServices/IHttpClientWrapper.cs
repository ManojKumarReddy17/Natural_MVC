

using Natural.Core.Models;

namespace Natural.Core.IServices

{
    public interface IHttpClientWrapper
    {
        public Task<T> GetAsync<T>(string endpoint);

        public Task<T> PostAsync<T>(string endpoint, object model);

        Task<T> PutAsync<T>(string endpoint, object model);
    }
}
