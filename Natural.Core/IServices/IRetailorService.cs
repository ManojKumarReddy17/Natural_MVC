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
        Task<List<RetailorModel>> GetAssignedRetailors();
        Task<RetailorModel> GetRetailorById(string Retailorid);
        Task<RetailorModel> GetRetailorDetailsById(string retailorId);
        Task<RetailorModel> CreateRetailor(RetailorModel retailor);
        Task<RetailorModel> UpdateRetailor(string RetailorId, RetailorModel Retailor);
        Task <bool>DeleteRetailor(string retailorId);
        Task<List<RetailorModel>> SearchRetailor(SearchModel searchretailor);




    }
}
