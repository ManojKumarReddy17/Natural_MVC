using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
    public interface ILocationService
    {
        Task<List<CityModel>> GetCities();
        Task<List<StateModel>> GetStates();
        Task<List<AreaModel>> GetAreas();

    }
}
