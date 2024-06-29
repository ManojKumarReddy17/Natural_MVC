using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
   public interface IRetailorService
    {
        Task<List<RetailorModel>> GetAllRetailors();
        // Task<PaginationResult<RetailorModel>> GetAllRetailors1(int page);
        Task<PaginationResult<RetailorModel>> GetAllRetailors1(int page, int pageSize = 10);

        Task<RetailorModel> GetRetailorById(string Retailorid);
        Task<RetailorModel> GetRetailorDetailsById(string retailorId);
        Task<RetailorModel> CreateRetailor(RetailorModel retailor);
        Task<RetailorModel> UpdateRetailor(string Id, RetailorModel mdl);
        Task <bool>DeleteRetailor(string retailorId);
        Task<PaginationResult<RetailorModel>> SearchRetailor(SearchModel searchretailor, bool? NonAssign);




    }
}
