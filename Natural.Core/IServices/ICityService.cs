using Natural.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
    public interface ICityService
    {
        Task<List<CityModel>> GetCity();
        Task<CityModel> UpdateCity(string Id, CityModel city);
        Task<bool> DeleteCity(string cityId);
        Task<CityModel> CreateCity(CityModel city);

        Task<CityModel> GetCityById(string Id);


    }
}
