using Natural.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
   public interface IAreaService
    {

        Task<PaginationResult<AreaModel>> GetAreas(int page);
        Task<AreaModel> GetAreaDetailsbyId(string ID);


        Task<AreaModel> CreateAreas(AreaModel area);
        Task<List<AreaModel>> SearchArea(SearchModel searcharea);
        Task<AreaModel> EditArea(string Id, AreaModel areaModel);
        Task<bool> DeleteArea(string Id);
    }
}
