using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
    public interface IExecutiveService
    {
        Task<List<ExecutiveModel>> GetAllExecutives();
        Task<List<ED_CreateModel>> GetExecutives();
        Task<ED_CreateModel> GetExecutiveById(string Id);
        Task<ED_CreateModel> GetExecutiveDetailsById(string ID);
        Task<ProductResponse> CreateExecutive(ExecutiveModel mdl);
        Task<ProductResponse> UpdateExecutive(string ExecutiveId, ExecutiveModel executive);

        Task<bool> DeleteExecutive(string executiveId);        
      Task<List<ED_CreateModel>> SearchExecutive(SearchModel searchexecutive, bool? NonAssign);

        Task<List<DistributorModel>> GetNonAssignedDistributors();
        Task<List<DistributorModel>> SearchNonAssignedDistributors(SearchModel searchdistributor);
        Task<string> DeleteAssignedDistributor(string DistributorId, string executiveId);

        Task<List<DistributorModel>> GetAssignedDistributorsByExecutiveId(string ExecutiveId);

    }
}