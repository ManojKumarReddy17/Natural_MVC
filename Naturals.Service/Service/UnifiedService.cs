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

        public async Task<PaginationResult<AreaModel>> GetAreasByCityId(string cityId)
        {
            return await _httpClientWrapper.GetByIdAsync<PaginationResult<AreaModel>>("/Area?CityId=", cityId);
        }
        public async Task<PaginationResult<AreaModel>> GetAreasByCityId1(string cityId, int page =1)
        {
            var areas = await _httpClientWrapper.GetAsync<PaginationResult<AreaModel>>($"/Area?CityId={cityId}&page={page}");

            var result = new PaginationResult<AreaModel>
            {
                Items = areas.Items,
                TotalItems = areas.TotalItems,
                TotalPageCount = areas.TotalPageCount
                //TotalPageCount = 1 // Assuming all items fit in one page since no pagination info is available
            };

            return result;
        }

    }

} 