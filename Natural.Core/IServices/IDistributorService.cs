using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
    public interface IDistributorService
    {
        Task<List<DistributorModel>> GetDistributors();
        Task<DistributorModel> CreateDistributor(DistributorModel distributor);

        Task<DistributorModel> GetDistributorById(string Id);


            Task<DistributorModel> UpdateDistributor(string Id, DistributorModel distributor);
    }
}
