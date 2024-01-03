using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class DsrService : IDsrService
    {
        private readonly IHttpClientWrapper _HttpCleintWrapper;

        public DsrService(IHttpClientWrapper httpCleintWrapper)
        {
            _HttpCleintWrapper = httpCleintWrapper;
        }

        public async Task<List<DsrModel>> GetDsrAll()
        {
            var response = await _HttpCleintWrapper.GetAsync<List<DsrModel>>("/Dsr/");
            return response;
        }
        public async Task<DsrModel> CreateDSR(DsrModel dsr)
        {
            var result = await _HttpCleintWrapper.PostAsync<DsrModel>("/Dsr/", dsr);
            return result;
        }
      
    }
}

