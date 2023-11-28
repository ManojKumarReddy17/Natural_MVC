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
        Task<List<RetailorModel>> GetRetailors();
        Task<RetailorModel> CreateRetailors(RetailorModel distributor);
    }
}
