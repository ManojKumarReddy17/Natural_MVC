using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public  class DistributorLoginService : IDistributorLoginService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public DistributorLoginService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<DistributorLoginModel> DistributorLogin(DistributorLoginModel model)
        {
            var output = await _httpClientWrapper.PostAsync<DistributorLoginModel>("/Distributor/Login", model);
            return output;
        }
    }
}
