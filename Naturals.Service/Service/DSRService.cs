using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class DSRService : IDSRService
    {

        private readonly IHttpClientWrapper _HttpCleintWrapper;
        private readonly IUnifiedService _unifiedService;

        public DSRService(IHttpClientWrapper httpCleintWrapper, IUnifiedService unifiedService)
        {
            _HttpCleintWrapper = httpCleintWrapper;
            _unifiedService = unifiedService;
        }

        public Task<DSRModel> CreateDsr(DSRModel dsr)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DSRModel>> GetDsrAll()
        {
            var response = await _HttpCleintWrapper.GetAsync<List<DSRModel>>("/Dsr/");
            return response;
        }






    }

}
