using NatDMS.Models;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
    public interface IDistributorLoginReportsService
    {
        Task<DistributorSalesReportInput> GetById(DistributorLoginModel DSReport);
        Task<DistributorLoginModel> GetDsreport();
        Task<List<DsrRetailor>> GetRetailorDetailsByDistributorId(string id);
       Task<List<DistributorSalesReportInput>> SearchDSR(DistributorLoginSample Search);
    }
}
