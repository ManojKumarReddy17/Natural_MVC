using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class DistributorService : IDistributorService
    {

        private readonly IHttpClientWrapper _HttpCleintWrapper;

        public DistributorService(IHttpClientWrapper httpCleintWrapper)
        {
            _HttpCleintWrapper = httpCleintWrapper;
        }
        public async Task<List<DistributorModel>> GetDistributors()
        {
            var getdistributor = await _HttpCleintWrapper.GetAsync<List<DistributorModel>>("/Distributor/");
            return getdistributor;

        }
        public async Task<DistributorModel> CreateDistributor(DistributorModel distributor)
        {
            var result= await _HttpCleintWrapper.PostAsync<DistributorModel>("/Distributor/", distributor);
            return result;
        }

        public async Task<DistributorModel> GetDistributorById(string Id)
        {
            string controller = $"/Distributor/{Id}";
      
            var output = await _HttpCleintWrapper.GetAsync<DistributorModel> (controller);

            return output;

        }

        public Task UpdateDistributor(string id, DistributorModel update)
        {
            throw new NotImplementedException();
        }
    }
}
