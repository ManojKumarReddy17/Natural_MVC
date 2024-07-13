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
        Task<DsrDisplay> GetDsrAll();
        Task<DSRModel> CreateDsr(DSRModel dsr);
        Task<List<DsrDistributor>> AssignedDistributorDetailsByExecutiveId(string Id);
        Task<List<DsrRetailor>> GetAssignedRetailorDetailsByDistributorId(string DistributorId);
        
        Task<Dsrcreate> CreateDsr();
        Task<Dsrcreate> UpdateSession(Dsrcreate ExistingSession, Dsrcreate UpadeSession);
        Task<List<DsrProduct>> SearchProductsAsync(Dsrcreate ExistingSession, Dsrcreate UpadeSession);
        Task<ProductResponse> Insert(DsrInsert Insertdata);
        Task<ProductResponse> Updatedsr(DsrInsert updatedata);
        Task<DsrInsert> onlyUpdateaInsert(Dsrcreate ExistingSession);
        Task<PaginationResult<DsrProduct>> GetProductAsync();
        Task<DsrInsert> Details(string dsrid);
        Task<List<DSRModel>> dsrsearch(Dsrview dsr);
        Task<bool> DeleteDsr(string DsrId);
        Task<Dsrcreate> editDsr(string dsrid);
        Task <List<DsrExecutiveDrop>> GetExecutive();


    }
}
