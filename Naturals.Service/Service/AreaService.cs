
using Natural.Core.IServices;
using Natural.Core.Models;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class AreaService : IAreaService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public AreaService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<List<AreaModel>> GetArea(string CityId)
        {
            string controller = $"/Area/{CityId}";
            var output = await _httpClientWrapper.GetAsync<List<AreaModel>>(controller);
            

            return output;
            
        }

        
    }
}
