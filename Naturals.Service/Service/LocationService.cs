using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class LocationService : ILocationService
    {

        private readonly IHttpClientWrapper _httpClientWrapper;

        public LocationService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<List<CityModel>> GetCities()
        {
            return await _httpClientWrapper.GetAsync<List<CityModel>>("/City/");
        }

        public async Task<List<StateModel>> GetStates()
        {
            return await _httpClientWrapper.GetAsync<List<StateModel>>("/State/");
        }

        public async Task<List<AreaModel>> GetAreas()
            {
            return await _httpClientWrapper.GetAsync<List<AreaModel>>("/Area/");
        }


    }
}