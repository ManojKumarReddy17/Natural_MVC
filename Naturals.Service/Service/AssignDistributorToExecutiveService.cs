using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class AssignDistributorToExecutiveService : IAssignDistributorToExecutiveService
    {
        private readonly IHttpClientWrapper _httpClient;

        public AssignDistributorToExecutiveService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DistributorToExecutive> AssignDsitributorToExecutive(DistributorToExecutive distributorToExecutive)
        {
            var result = await _httpClient.PostAsync<DistributorToExecutive>("/AssignDistributorToExecutive/", distributorToExecutive);
            return result;
        }
    }
}
