using Natural.Core.IServices;
using Natural.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public  class AreaService: IAreaService
    {
        private readonly IHttpClientWrapper _httpClient;


        public AreaService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task <AreaModel>GetAreaDetailsbyId(string ID) 
        {
            var result = await _httpClient.GetByIdAsync<AreaModel>("/Area/areaById/",ID);
            return result;
      
        }
        

       
        public async Task<AreaModel>CreateAreas(AreaModel area)
        {
            var result = await _httpClient.PostAsync<AreaModel>("/Area", area);
            return result;
        }

        public async Task<PaginationResult<AreaModel>> GetAreas(int page)
        {

            var result = await _httpClient.GetAsync<PaginationResult<AreaModel>>($"/Area?page={page}");


            return result;
        }
        


        public async Task<List<AreaModel>> SearchArea(SearchModel searcharea)
        {
            var SearchedResult = await _httpClient.PostAsync<List<AreaModel>>("/Area/Search", searcharea);
            return SearchedResult;  
        }
           public async Task<AreaModel>EditArea (string Id,AreaModel areaModel) 
           { 
             var result =await _httpClient.PutAsync<AreaModel>("/Area/AreaId", Id,areaModel);
             return result;
           }
        public async Task<bool> DeleteArea (string Id)
        {
            try
            {
                var delete = await _httpClient.DeleteAsync("/Area", Id);
                if (delete)
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
    }
    
}
