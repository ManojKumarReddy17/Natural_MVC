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
        Task<DistributorModel> GetDistributorDetailsById(string detailsid);
        Task<DistributorModel> GetDistributorById(string id);
        Task<DistributorModel> CreateDistributor(DistributorModel distributor);
        Task<DistributorModel> UpdateDistributor(string DistributorId, DistributorModel distributor);
        Task<bool> DeleteDistributor(string distributorId);
        Task<List<DistributorModel>> SearchDistributor(SearchModel searchdistributor);
        Task<List<RetailorModel>> GetNonAssignedRetailors();
        Task<List<RetailorModel>> SearchRetailor(SearchModel searchretailor);
        Task<List<AssignedRetailorToDistributorIdModel>> GetAssignedRetailorByDistributor(string Disid);



    }
}


       