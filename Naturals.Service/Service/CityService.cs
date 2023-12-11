
using Natural.Core.Models;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Natural.Core.IServices;

namespace Naturals.Service.Service
{
    public class CityService : ICityService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public CityService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<List<CityModel>> GetCity(string StateId)
        {
            string controller = $"/City/{StateId}";
            
            var output = await _httpClientWrapper.GetAsync<List<CityModel>>(controller);

           

            return output;
            
        }

        public async Task<List<CityModel>> GetCity()
        {
            

            var output = await _httpClientWrapper.GetAsync<List<CityModel>>("/City/sta01");
           

            return output;
        }
        private object Where(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
