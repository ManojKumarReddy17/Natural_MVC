using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class RetailorService:IRetailorService
    {
        private readonly IHttpClientWrapper _HttpCleintWrapper;

        public RetailorService(IHttpClientWrapper httpCleintWrapper)
        {
            _HttpCleintWrapper = httpCleintWrapper;
        }
        public async Task<List<RetailorModel>> GetRetailors()
        {
            var getretailor = await _HttpCleintWrapper.GetAsync<List<RetailorModel>>("/Retailor/");
            return getretailor;

        }
        public async Task<RetailorModel> CreateRetailors(RetailorModel retailors)
        {
            var result = await _HttpCleintWrapper.PostAsync<RetailorModel>("/Retailor/", retailors);
            return result;
        }
    }
   
}
