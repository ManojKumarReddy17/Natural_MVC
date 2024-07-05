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
        Task<PaginationResult<DistributorModel>> GetAllDistributors();
        Task<PaginationResult<GetExecutive>> GetAllDistributorsAsync(int page);
         Task<PaginationResult<GetExecutive>> GetAllDistributorsAsync1(int page, int pageSize = 10);

        Task<DistributorModel> GetDistributorDetailsById(string detailsid);
        Task<DistributorModel> GetDistributorById(string id);
        Task<ProductResponse> CreateDistributor(ExecutiveModel distributor);
        Task<ProductResponse> UpdateDistributor(string DistributorId, ExecutiveModel distributor);
        Task<bool> DeleteDistributor(string distributorId);
        Task<PaginationResult<DistributorModel>> SearchDistributor(SearchModel searchdistributor, bool? NonAssign);
        Task<PaginationResult<RetailorModel>> GetNonAssignedRetailors();
        Task<List<RetailorModel>> SearchRetailor(SearchModel searchretailor);
        Task<List<RetailorModel>> GetAssignedRetailorByDistributorId(string Disid);
        Task<PaginationResult<RetailorModel>> SearchNonAssignedRetailors(SearchModel searchdistributor);
        Task<string> DeleteAssignedRetailor(string retailorId, string distributorId);

    }
}


       