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
        public async Task<List<ExecutiveModel>> GetDeatils()
        {
            var getexe = await _httpClient.GetAsync<List<ExecutiveModel>>("/Executive");
            return getexe;
        }
        public async Task<ExecutiveModel> CreateExecutive(ExecutiveModel mdl)
        {
            var createexe = await _httpClient.PostAsync<ExecutiveModel>("/Executive/", mdl);
            return createexe;
        }
        public async Task<bool> DeleteExecutiveasync(string executiveId)
        {
            try
            {
                var delete = await _httpClient.DeleteAsync("/Executive", executiveId);
                if(delete)
                {
                    return true;
                }
                else
        {
                    return false;
                }
        }
            catch (Exception ) 
        {
                throw new Exception("Error");
            }
        }

        public async Task<ExecutiveModel> GetExecutiveDetailsById(string ID)
        {
            
            var excdtlid = await _httpClient.GetByIdAsync<ExecutiveModel>("/Executive/details", ID);
            return excdtlid;
        }
        public async Task<ExecutiveModel> UpdateDistributor(string Id, ExecutiveModel execupdate)
        {
            string controller = $"/Executive/{Id}";

            var output = await _httpClient.PutAsync<ExecutiveModel>("/Executive/id", Id);

            return output;

        }

        public async Task<ExecutiveModel> GetExecutiveById(string Id)
        {
            //string controller = $"/Executive/{Id}";


            var output = await _httpClient.GetByIdAsync<ExecutiveModel>("/Executive",Id);



            return output;
        }
    }
    }

