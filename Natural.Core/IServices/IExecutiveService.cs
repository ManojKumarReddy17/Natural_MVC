﻿using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
    public interface IExecutiveService
    {
        Task<ExecutiveModel> GetExecutiveById(string Id);

        Task<ExecutiveModel> UpdateDistributor(string Id, ExecutiveModel distributor);
        Task<List<ExecutiveModel>> GetDeatils();
        Task<ExecutiveModel> CreateExecutive(ExecutiveModel mdl);
        Task<bool> DeleteExecutiveasync(string executiveId);
        Task<ExecutiveModel> GetExecutiveDetailsById(string ID);
    }
}