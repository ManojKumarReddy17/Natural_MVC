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
        Task<List<DistributorModel>> GetAllDistributors();
        Task<List<GetExecutive>> GetAllDistributorsAsync();
        Task<DistributorModel> GetDistributorDetailsById(string detailsid);
        Task<DistributorModel> GetDistributorById(string id);
        Task<ProductResponse> CreateDistributor(ExecutiveModel distributor);
        Task<ProductResponse> UpdateDistributor(string DistributorId, ExecutiveModel distributor);
        Task<bool> DeleteDistributor(string distributorId);
        Task<List<DistributorModel>> SearchDistributor(SearchModel searchdistributor);
        Task<List<RetailorModel>> GetNonAssignedRetailors();
        Task<List<RetailorModel>> SearchRetailor(SearchModel searchretailor);
        Task<List<RetailorModel>> GetAssignedRetailorByDistributorId(string Disid);
        Task<List<RetailorModel>> SearchNonAssignedRetailors(SearchModel searchdistributor);
        Task<string> DeleteAssignedRetailor(string retailorId, string distributorId);

    }
}


       