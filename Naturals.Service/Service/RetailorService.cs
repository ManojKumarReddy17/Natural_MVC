using Natural.Core.IServices;
using Natural.Core.Models;
using Newtonsoft.Json;
using System.Net.Http;


namespace Naturals.Service.Service
{
    public class RetailorService:IRetailorService
    {
        private readonly IHttpClientWrapper _HttpCleintWrapper;

        public RetailorService(IHttpClientWrapper httpCleintWrapper)
        {
            _HttpCleintWrapper = httpCleintWrapper;
        }
        public async Task<List<RetailorModel>> GetRetailors()
        {
            var getretailor = await _HttpCleintWrapper.GetAsync<List<RetailorModel>>("/Retailor/");
            return getretailor;

        }
        public async Task<RetailorModel> GetRetailorById(string Retailorid)
        {
            var getretailorid = await _HttpCleintWrapper.GetByIdAsync<RetailorModel>("/Retailor", Retailorid);
            return getretailorid;
        }

        public async Task<RetailorModel> CreateRetailors(RetailorModel retailors)
        {
            var result = await _HttpCleintWrapper.PostAsync<RetailorModel>("/Retailor/", retailors);
            return result;
        }

        public async Task<bool> DeleteRetailor(string retailorId)
        {
            try
            {
                var isDeleted = await _HttpCleintWrapper.DeleteAsync("/Retailor", retailorId);

                if (isDeleted)
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
                throw new Exception("An error occurred while deleting the retailor.");
            }
        }

        public async Task<RetailorModel> GetRetailorsById(string retailorId)
        {
            var getretailorid = await _HttpCleintWrapper.GetByIdAsync<RetailorModel>("/Retailor/details", retailorId);
            return getretailorid;


        }
    }


}


   

