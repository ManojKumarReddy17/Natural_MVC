using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
   public  interface IDSRService
    {
        Task<List<DSRModel>> GetDsrAll();
        Task<DSRModel> CreateDsr(DSRModel dsr);
        Task<List<DsrDistributor>> AssignedDistributorDetailsByExecutiveId(string Id);
        Task<List<DsrRetailor>> GetAssignedRetailorDetailsByDistributorId(string DistributorId);
        Task<List<DsrProduct>> GetProductAsync();
        Task<Dsrcreate> CreateDsr();
        //Task<List<DsrProduct>> SearchProductsAsync(ProductSearch search, List<DsrProduct> productlist);
        //Task<List<DsrProduct>> UpdateSession(List<DsrProduct> ExistingSession, List<DsrProduct> UpadeSession);
        Task<Dsrcreate> UpdateSession(Dsrcreate ExistingSession, Dsrcreate UpadeSession);
        Task<List<DsrProduct>> SearchProductsAsync(Dsrcreate ExistingSession, Dsrcreate UpadeSession);
        Task<Dsrcreate> Insert(Dsrcreate ExistingSession);

    }
}
