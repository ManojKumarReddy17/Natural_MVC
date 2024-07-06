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
        Task<DistributorSalesReportInput> GetAllDSR(DistributorSalesReport DSReport);
        Task<List<DistributorSalesReportInput>> SearchDSR(DistributorSalesReport Search);
        Task<DistributorSalesReport> GetDsreport();
        // Task<List<RetailorByArea>> GetAssignedRetailorByArea(string areaId);
        Task<List<ExecutiveByArea>> GetExecutiveByArea(string areaId);
        Task<List<DsrDistributor>> GetDistributorDetailsByExecutiveId(string ExecutiveId);
        Task<List<DsrRetailor>> GetRetailorDetailsByDistributorId(string DistributorId);
    }
}
