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


       

        public async Task<RetailorToDistributor> AssignRetailorToDistributor(RetailorToDistributor retailorToDistributor)
        {
            var result = await _httpClientWrapper.PostAsync<RetailorToDistributor>("/AssignRetailorToDistributor/", retailorToDistributor);
            return result;
        }
    }
}
