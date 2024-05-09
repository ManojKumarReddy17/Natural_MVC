using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class CityService : ICityService
    {
        private readonly IHttpClientWrapper _HttpCleintWrapper;

        public CityService(IHttpClientWrapper httpCleintWrapper)
        {
            _HttpCleintWrapper = httpCleintWrapper;
        }



        public async Task<CityModel> CreateCity(CityModel city)
        {
            var result = await _HttpCleintWrapper.PostAsync<CityModel>("/City", city);

            return result;
        }

        public async Task<bool> DeleteCity(string id)
        {
            try
            {
                var isDeleted = await _HttpCleintWrapper.DeleteAsync("/City", id);

                if (isDeleted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while deleting the City.");
            }
        }



        

        public async Task<List<CityModel>> GetCity()
        {
            var result = await _HttpCleintWrapper.GetAsync<List<CityModel>>("/City/");
            return result;
        }

        public async Task<CityModel> GetCityById(string Id)
        {
            var result = await _HttpCleintWrapper.GetByIdAsync<CityModel>("/City/getbyid", Id);
            return result;
        }





        public async Task<CityModel> UpdateCity(string Id, CityModel city)
        {
            var result = await _HttpCleintWrapper.PutAsync<CityModel>("/City", Id, city);
            return result;
        }


    }
}
