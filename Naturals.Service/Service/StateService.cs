
using Natural.Core.IServices;
using Natural.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class StateService : IStateService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public StateService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<List<StateModel>> GetState()
        {
            var output = await _httpClientWrapper.GetAsync<List<StateModel>>("/State/");
            

            return output;
        }
    }
}
