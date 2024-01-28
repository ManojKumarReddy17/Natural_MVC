using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class ExecutiveService : IExecutiveService
    {
        private readonly IHttpClientWrapper _httpClient;
        public ExecutiveService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// GET ALL EXECUTIVES //
        /// </summary>
        public async Task<List<ExecutiveModel>> GetAllExecutives()
        {
            var getexe = await _httpClient.GetAsync<List<ExecutiveModel>>("/Executive");
            return getexe;
        }

        /// <summary>
        /// GET EXECUTIVE DETAILS BY ID //
        /// </summary>
        public async Task<ExecutiveModel> GetExecutiveDetailsById(string ID)
        {

            var excdtlid = await _httpClient.GetByIdAsync<ExecutiveModel>("/Executive/details", ID);
            return excdtlid;
        }


        /// <summary>
        /// Get Assigned Distributor to ExecutiveById
        /// </summary>
        /// <param name="ExecutiveId"></param>
        /// <returns></returns>

        public async Task<List<DistributorModel>> GetAssignedDistributorsByExecutiveId(string ExecutiveId)
        {
            var assineddistr = await _httpClient.GetByIdAsync<List<DistributorModel>>("/AssignDistributorToExecutive/Details", ExecutiveId);
            return assineddistr;
        }

        /// <summary>
        /// GET EXECUTIVE BY ID //
        /// </summary>

        public async Task<ExecutiveModel> GetExecutiveById(string Id)
        {
            var output = await _httpClient.GetByIdAsync<ExecutiveModel>("/Executive", Id);

            return output;
        }

        /// <summary>
        /// CREATE EXECUTIVE  //
        /// </summary>
        public async Task<ExecutiveModel> CreateExecutive(ExecutiveModel mdl)
        {
            var createexe = await _httpClient.PostAsync<ExecutiveModel>("/Executive/", mdl);
            return createexe;
        }

        /// <summary>
        /// UPDATE EXECUTIVE  //
        /// </summary>
        public async Task<ExecutiveModel> UpdateExecutive(string Id, ExecutiveModel executive)
        {
            var output = await _httpClient.PutAsync<ExecutiveModel>("/Executive", Id, executive);

            return output;

        }

        /// <summary>
        /// DELETE EXECUTIVE  //
        /// </summary>
        public async Task<bool> DeleteExecutive(string executiveId)
        {
            try
            {
                var delete = await _httpClient.DeleteAsync("/Executive", executiveId);
                if (delete)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw new Exception("Error");
            }
        }

        public async Task<List<ExecutiveModel>> SearchExecutive(SearchModel searchexecutive)
        {
            var SearchedResult = await _httpClient.PostAsync<List<ExecutiveModel>>("/Executive/Search", searchexecutive);
            return SearchedResult;
        }

        public async Task<List<DistributorModel>> GetNonAssignedDistributors()
        {
            var getdistributor = await _httpClient.GetAsync<List<DistributorModel>>("/Distributor/Assign");
            return getdistributor;

        }
        public async Task<List<DistributorModel>> SearchNonAssignedDistributors(SearchModel searchdistributor)
        {
            var SearchedResult = await _httpClient.PostAsync<List<DistributorModel>>("/Distributor/Search", searchdistributor);
            return SearchedResult;
        }
        public async Task<string> DeleteAssignedDistributor(string distributorId, string executiveId)
        {
            var delete = await _httpClient.DeleteAssignedDistirbutorAsync("/AssignDistributorToExecutive", $"{distributorId}/{executiveId}");
            return delete;
        }

    }
}
