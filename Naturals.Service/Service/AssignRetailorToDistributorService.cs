using Natural.Core.IServices;
using Natural.Core.Models;

namespace Naturals.Service.Service
{
    public class AssignRetailorToDistributorService : IAssignRetailorToDistributorService
    {
        private readonly IHttpClientWrapper _httpClientWrapper; 

        public AssignRetailorToDistributorService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;  
        }

        public Task<RetailorToDistributor> AssignRetailorsToDistributor(RetailorToDistributor retailorToDistributorlist)
        {
            throw new NotImplementedException();
        }
    }
}
