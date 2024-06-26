﻿using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices 
{
    public interface IUnifiedService
    {
        Task<List<StateModel>> GetStates();
        Task<List<CityModel>> GetCities();
        Task<List<CityModel>> GetCitiesbyStateId(string stateId);
        Task<PaginationResult<AreaModel>> GetAreasByCityId1(string cityId, int page=1);
        Task<PaginationResult<AreaModel>> GetAreasByCityId(string cityId);
    }
}
