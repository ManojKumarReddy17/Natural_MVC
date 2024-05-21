using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{

    public class UnifiedService : IUnifiedService
    {

        private readonly IHttpClientWrapper _httpClientWrapper;

        public UnifiedService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<List<StateModel>> GetStates()
        {
            return await _httpClientWrapper.GetAsync<List<StateModel>>("/State/");
        }
        public async Task<List<CityModel>> GetCities()
        {
            return await _httpClientWrapper.GetAsync<List<CityModel>>("/City/");
        }
        public async Task<List<CityModel>> GetCitiesbyStateId(string stateId)
        {
            return await _httpClientWrapper.GetByIdAsync<List<CityModel>>("/City?StateId=", stateId);
        }

        public async Task<List<AreaModel>> GetAreasByCityId(string cityId)
        {
            return await _httpClientWrapper.GetByIdAsync<List<AreaModel>>("/Area?CityId=", cityId);
        }


    }

} 