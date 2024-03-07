using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
    public interface IDistributorSalesService
    {
        ////Task<DistributorSalesReport> GetById(DistributorSalesReportInput DSReport);
        // Task<List<DistributorSalesReportInput>> DailySaleReport(DistributorSalesReport report);
        Task<DistributorSalesReportInput> GetById(DistributorSalesReport DSReport);
        Task<List<DistributorSalesReportInput>> SearchDSR(DistributorSalesReport Search);
        Task<List<DsrDistributor>> AssignedDistributorDetailsByExecutiveId(string Id);
        Task<List<DsrRetailor>> GetAssignedRetailorDetailsByDistributorId(string DistributorId);
        Task<DistributorSalesReport> GetDsreport();

    }
}
