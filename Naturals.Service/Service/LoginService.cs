using Natural.Core.IServices;
using Natural.Core.Models;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Text;

namespace Naturals.Service.Service
{
    public class LoginService : ILoginService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public LoginService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<LoginModel> LoginAsync(LoginModel model)
        {
           
           var output =  await _httpClientWrapper.PostAsync<LoginModel>("/Login/",model);

            return output;

        }
    }
}
