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

        /// <summary>
        /// GET ALL RETAILORS
        /// </summary>
        public async Task<List<RetailorModel>> GetAllRetailors()
        {
            var getretailor = await _HttpCleintWrapper.GetAsync<List<RetailorModel>>("/Retailor/");
            return getretailor;

        }

        /// <summary>
        /// GET RETAILOR BY ID
        /// </summary>
        public async Task<RetailorModel> GetRetailorById(string Retailorid)
        {
            var getretailorid = await _HttpCleintWrapper.GetByIdAsync<RetailorModel>("/Retailor", Retailorid);
            return getretailorid;
        }

        /// <summary>
        /// GET RETAILOR DETAILS BY ID
        /// </summary>

        public async Task<RetailorModel> GetRetailorDetailsById(string retailorId)
        {
            var getretailorid = await _HttpCleintWrapper.GetByIdAsync<RetailorModel>("/Retailor/details", retailorId);
            return getretailorid;

        }

        /// <summary>
        /// CREATE RETAILOR 
        /// </summary>

        public async Task<RetailorModel> CreateRetailor(RetailorModel retailors)
        {
            var result = await _HttpCleintWrapper.PostAsync<RetailorModel>("/Retailor/", retailors);
            return result;
        }

        /// <summary>
        /// UPDATE RETAILOR BY ID
        /// </summary>
        public async Task<RetailorModel> UpdateRetailor(string RetailorId, RetailorModel updatedRetailor)
        {
            var result = await _HttpCleintWrapper.PutAsync<RetailorModel>("/Retailor", RetailorId, updatedRetailor);
            return result;
        }


        /// <summary>
        /// DELETE RETAILOR BY ID
        /// </summary>

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

    }


}


   

