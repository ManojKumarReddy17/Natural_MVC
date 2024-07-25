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
    public class DistributorLoginReportsService : IDistributorLoginReportsService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IRetailorService _RetailorService;


        public DistributorLoginReportsService(IHttpClientWrapper httpClientWrapper, IRetailorService RetailorService)
        {
            _httpClientWrapper = httpClientWrapper;
            _RetailorService = RetailorService;
        }

        public async Task<DistributorSalesReportInput> GetById(DistributorLoginModel DSReport)
        {
            var result = await _httpClientWrapper.GetByIdAsync<DistributorSalesReportInput>("/DistributorLoginReports/", DSReport);

            return result;
        }
        public async Task<List<DistributorSalesReportInput>> SearchDSR(DistributorLoginSample Search)
        {

            //var SearchedResult = await _httpClientWrapper.PostAsync<List<DistributorSalesReportInput>>("/api/DistributorLoginReports?Distributor&Retailor&StartDate&EndDate", Search);
            var SearchedResult = await _httpClientWrapper.PostAsync<List<DistributorSalesReportInput>>("/DistributorLoginReports/search", Search);


            return SearchedResult;
        }
       

        public async Task<List<DsrRetailor>> GetRetailorDetailsByDistributorId(string id)
        {
            var result = await _httpClientWrapper.GetByIdAsync<List<DsrRetailor>>("/Dsr/RetailorDetailsbyExeOrDisId?Id=", id);
            return result;
        }
      

        public async Task<DistributorLoginModel> GetDsreport()
        {
            PaginationResult<RetailorModel> retailor = await _RetailorService.GetAllRetailors();

            List<DsrRetailorDrop> retailorList = retailor.Items
             //   .Where(c => c.IsRetailor) // Assuming IsRetailor property identifies a retailer
                .Select(c => new DsrRetailorDrop
                {
                    Id = c.Id,
                    Retailor = string.Concat(c.FirstName, " ", c.LastName)
                }).ToList();

            List<DsrDistributorDrop> distributorList = retailor.Items
             //   .Where(c => c.IsDistributor) // Assuming IsDistributor property identifies a distributor
                .Select(c => new DsrDistributorDrop
                {
                    Id = c.Id,
                    DistributorName = string.Concat(c.FirstName, " ", c.LastName)
                }).ToList();

            DistributorLoginModel viewModel = new DistributorLoginModel()
            {
                Retailorlist = retailorList,
                Distributorlist = distributorList
            };

            return viewModel;
        }





    }
}
