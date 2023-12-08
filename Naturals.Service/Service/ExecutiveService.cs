using Natural.Core.IServices;
using Natural.Core.Models;
using Natural_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class ExecutiveService : IExecutiveService
    {
        private readonly IHttpClientWrapper _httpCleintWrapper;

        public ExecutiveService(IHttpClientWrapper httpCleintWrapper)
        {
            _httpCleintWrapper = httpCleintWrapper;
        }
        public async Task<ExecutiveModel> GetExecutiveById(string Id)
        {
            string controller = $"/Executive/{Id}";

            var output = await _httpCleintWrapper.GetAsync<ExecutiveModel>(controller);

            return output;
        }

        public async Task<ExecutiveModel> UpdateDistributor(string Id, ExecutiveModel distributor)
        {
            string controller = $"/Executive/{Id}";

            var output = await _httpCleintWrapper.PutAsync<ExecutiveModel>(controller, distributor);

            return output;

        }
    }
}
