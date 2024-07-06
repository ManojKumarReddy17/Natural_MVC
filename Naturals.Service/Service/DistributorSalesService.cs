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
        private readonly IExecutiveService _executiveService;
        public DistributorSalesService(IHttpClientWrapper httpClientWrapper, IExecutiveService executiveService, IRetailorService retailorService, IUnifiedService unifiedService)
        {
            _httpClientWrapper = httpClientWrapper;
            _retailorService = retailorService;
            _unifiedService = unifiedService;
            _executiveService = executiveService;
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


        //public async Task<List<RetailorByArea>> GetAssignedRetailorByArea(string areaId)
        //{


        //    var result = await _httpClientWrapper.GetByIdAsync<List<RetailorByArea>>("/Retailor/areaId/", areaId);

        //    return result;

        //}



        public async Task<List<ExecutiveByArea>> GetExecutiveByArea(string areaId)
        {


            var result = await _httpClientWrapper.GetByIdAsync<List<ExecutiveByArea>>("/Executive/areaId/", areaId);

            return result;

        }
        public async Task<List<DsrDistributor>> GetDistributorDetailsByExecutiveId(string ExecutiveId)
        {
            var result = await _httpClientWrapper.GetByIdAsync<List<DsrDistributor>>("/Dsr/Details/", ExecutiveId);
            return result;

        }

        public async Task<List<DsrRetailor>> GetRetailorDetailsByDistributorId(string id)
        {

            var result = await _httpClientWrapper.GetByIdAsync<List<DsrRetailor>>("/Dsr/RetailorDetailsbyExeOrDisId?Id=", id);

            return result;
        }

      
        public async Task<DistributorSalesReport> GetDsreport()
        {
            List<ED_CreateModel> retailor = await _executiveService.GetExecutives();


            List<DsrExecutiveDrop> ExecutiveList = retailor.Select(c => new DsrExecutiveDrop
            {
                Id = c.Id,
                Executive = string.Concat(c.FirstName, " ", c.LastName)

            }).ToList();
            List<DsrDistributorDrop> distributor = null;
            List<DsrRetailorDrop> retailors = null;

            DistributorSalesReport viewmodel = new DistributorSalesReport()
            {
                Executivelist = ExecutiveList,
                Distributorlist = distributor,
                Retailorlist = retailors
            };

            return viewmodel;
        }



    }
}
