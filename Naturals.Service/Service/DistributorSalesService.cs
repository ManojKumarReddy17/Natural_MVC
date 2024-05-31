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
    public class DistributorSalesService : IDistributorSalesService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IRetailorService _retailorService;
        private readonly IUnifiedService _unifiedService;

        public DistributorSalesService(IHttpClientWrapper httpClientWrapper, IRetailorService retailorService, IUnifiedService unifiedService)
        {
            _httpClientWrapper = httpClientWrapper;
            _retailorService = retailorService;
            _unifiedService = unifiedService;
        }

        public async Task<DistributorSalesReportInput> GetAllDSR(DistributorSalesReport DSReport)
        {
            var result = await _httpClientWrapper.GetByIdAsync<DistributorSalesReportInput>("/DSReport/", DSReport);

            return result;
        }
       
        public async Task<List<DistributorSalesReportInput>> SearchDSR(DistributorSalesReport Search)
        {

            var SearchedResult = await _httpClientWrapper.PostAsync<List<DistributorSalesReportInput>>("/DSReport", Search);

            return SearchedResult;
        }

       
        public async Task<List<RetailorByArea>> GetAssignedRetailorByArea(string areaId)
        {


            var result = await _httpClientWrapper.GetByIdAsync<List<RetailorByArea>>("/Retailor/areaId/", areaId);

            return result;

        }
      


        public async Task<DistributorSalesReport> GetDsreport()
        {
            List<RetailorModel> retailor = await _retailorService.GetAllRetailors();

           
            List<RetailorByArea> retailorList = retailor.Select(c => new RetailorByArea
            {
                Id = c.Id,
                Retailor = string.Concat(c.FirstName, " ", c.LastName)

            }).ToList();

            DistributorSalesReport viewmodel = new DistributorSalesReport()
            {
                Retailorlist = retailorList
            };

            return viewmodel;
        }


    }
}
