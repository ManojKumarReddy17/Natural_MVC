using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
    public interface IExecutiveService
    {
        Task<List<ExecutiveModel>> GetAllDeatils();
        Task<ExecutiveModel> GetExecutiveById(string Id);
        Task<ExecutiveModel> GetExecutiveDetailsById(string ID);
        Task<ExecutiveModel> CreateExecutive(ExecutiveModel mdl);
        Task<ExecutiveModel> UpdateExecutive(string ExecutiveId, ExecutiveModel executive);
        Task<bool> DeleteExecutive(string executiveId);
    }
}
