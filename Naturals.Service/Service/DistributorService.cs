using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Naturals.Service.Service
{
    public class DistributorService : IDistributorService
    {

        private readonly IHttpClientWrapper _HttpCleintWrapper;

        public DistributorService(IHttpClientWrapper httpCleintWrapper)
        {
            _HttpCleintWrapper = httpCleintWrapper;
        }

        /// <summary>
        /// GET ALL DISTRIBUTORS //
        /// </summary>
        public async Task<List<DistributorModel>> GetAllDistributors()
        {
            var getdistributor = await _HttpCleintWrapper.GetAsync<List<DistributorModel>>("/Distributor/");
            return getdistributor;

        }

        /// <summary>
        /// GET DISTRIBUTOR DETAILS BY ID //
        /// </summary>

        public async Task<DistributorModel> GetDistributorDetailsById(string detailsid)
        {

            var DistributorDetails = await _HttpCleintWrapper.GetByIdAsync<DistributorModel>("/Distributor/Details", detailsid);
            return DistributorDetails;
        }
        /// <summary>
        /// GET DISTRIBUTOR BY ID //
        /// </summary>

        public async Task<DistributorModel> GetDistributorById(string id)
        {
            var DistributorDetails = await _HttpCleintWrapper.GetByIdAsync<DistributorModel>("/Distributor", id);
            return DistributorDetails;

        }

        /// <summary>
        /// CREATE DISTRIBUTOR //
        /// </summary>
       

        public async Task<DistributorModel> CreateDistributor(DistributorModel distributor)
        {
            var result = await _HttpCleintWrapper.PostAsync<DistributorModel>("/Distributor/", distributor);
            return result;
        }

        /// <summary>
        /// UPDATE DISTRIBUTOR BY ID //
        /// </summary>

        public async Task<DistributorModel> UpdateDistributor(string Id, DistributorModel distributor)
        {
            var output = await _HttpCleintWrapper.PutAsync<DistributorModel>("/Distributor",Id, distributor);

            return output;

        }


        /// <summary>
        /// DELETE DISTRIBUTOR BY ID //
        /// </summary>
        public async Task<bool> DeleteDistributor(string distributorId)
        {
            try
            {
                var isDeleted = await _HttpCleintWrapper.DeleteAsync("/Distributor", distributorId);

                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete distributor. Error: {ex.Message}");
            }
        }
    }
}

