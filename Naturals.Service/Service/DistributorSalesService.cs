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
        private readonly IExecutiveService _ExecutiveService;


        public DistributorSalesService(IHttpClientWrapper httpClientWrapper, IExecutiveService ExecutiveService)
        {
            _httpClientWrapper = httpClientWrapper;
            _ExecutiveService = ExecutiveService;
        }

        public async Task<DistributorSalesReportInput> GetById(DistributorSalesReport DSReport)
        {
            var result = await _httpClientWrapper.GetByIdAsync<DistributorSalesReportInput>("/DSReport/", DSReport);

            return result;
        }
        public async Task<List<DistributorSalesReportInput>> SearchDSR(DistributorSalesReport Search)
        {

            var SearchedResult = await _httpClientWrapper.PostAsync<List<DistributorSalesReportInput>>("/DSReport", Search);

            return SearchedResult;
        }
        //public async Task<List<DistributorSalesReportInput>> DailySaleReport(DistributorSalesReport report)
        //{

        //    var SearchedResult = await _httpClientWrapper.PostAsync<List<DistributorSalesReportInput>>("/DSReport", report);

        //    return SearchedResult;
        //}



        public async Task<List<DsrDistributor>> AssignedDistributorDetailsByExecutiveId(string Id)
        {
            var result = await _httpClientWrapper.GetByIdAsync<List<DsrDistributor>>("/Dsr/Details", Id);
            return result;

        }

        public async Task<List<DsrRetailor>> GetAssignedRetailorDetailsByDistributorId(string DistributorId)
        {

            var result = await _httpClientWrapper.GetByIdAsync<List<DsrRetailor>>("/Dsr", DistributorId);

            return result;


        }

        public async Task<DistributorSalesReport> GetDsreport()
        {
            List<ExecutiveModel> executives = await _ExecutiveService.GetAllExecutives();
            List<DsrExecutiveDrop> executiveList = executives.Select(c => new DsrExecutiveDrop
            {
                Id = c.Id,
                Executive = string.Concat(c.FirstName, " ", c.LastName),
            }).ToList();



            List<DsrDistributorDrop> distributor = null;
            List<DsrRetailorDrop> retailor = null;

            DistributorSalesReport viewmodel = new DistributorSalesReport()
            {
                Executivelist = executiveList,
                Distributorlist = distributor,
                Retailorlist = retailor
            };

            return viewmodel;
        }
    }
}
