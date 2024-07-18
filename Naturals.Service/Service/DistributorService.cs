using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

#nullable disable

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
        public async Task<PaginationResult<DistributorModel>> GetAllDistributors()
        {
            var getdistributor = await _HttpCleintWrapper.GetAsync<PaginationResult<DistributorModel>>("/Distributor/");
            return getdistributor;

        }
        public async Task<PaginationResult<GetExecutive>> GetAllDistributorsAsync(int page)
        {
            var getexe = await _HttpCleintWrapper.GetAsync<PaginationResult<GetExecutive>>($"/Distributor?page={page}" );
            return getexe;
        }
    

        public async Task<PaginationResult<GetExecutive>> GetAllDistributorsAsync1(int page, int pageSize = 10)
        {
            var getexe = await _HttpCleintWrapper.GetAsync<PaginationResult<GetExecutive>>($"/Distributor? page={page}& pageSize={pageSize}");
            return getexe;
        }

        /// <summary>
        /// GET DISTRIBUTOR DETAILS BY ID //
        /// </summary>

        public async Task<DistributorModel> GetDistributorDetailsById(string detailsid)
        {

            var DistributorDetails = await _HttpCleintWrapper.GetByIdAsync<DistributorModel>("/Distributor/Details/", detailsid); 

            return DistributorDetails;
        }

        /// <summary>
        /// GET DISTRIBUTOR BY ID //
        /// </summary>

        public async Task<DistributorModel> GetDistributorById(string id)
        {
            var DistributorDetails = await _HttpCleintWrapper.GetByIdAsync<DistributorModel>("/Distributor/Details/", id);
            return DistributorDetails;

        }

        /// <summary>
        /// CREATE DISTRIBUTOR //
        /// </summary>

        public async Task<ProductResponse> CreateDistributor(ExecutiveModel mdl)
        {
            using (var formData = new MultipartFormDataContent())
            {
                byte[] filebytes = null;
                if (mdl.ProfileImage != null)
                {
                    using (var ms = new MemoryStream())

                    {
                        await mdl.ProfileImage.CopyToAsync(ms);
                        filebytes = ms.ToArray();
                    }
                }

                formData.Add(new StringContent(mdl.FirstName), "FirstName");
                formData.Add(new StringContent(mdl.LastName), "LastName");
                formData.Add(new StringContent(mdl.Email ?? ""), "Email");
                formData.Add(new StringContent(mdl.Address ?? ""), "Address");
                formData.Add(new StringContent(mdl.MobileNumber), "MobileNumber");
                formData.Add(new StringContent(mdl.UserName), "UserName");
                formData.Add(new StringContent(mdl.Password), "Password");
                formData.Add(new StringContent(mdl.State), "State");
                formData.Add(new StringContent(mdl.City), "City");
                for (int i = 0; i < mdl.Area.Count; i++)
                {
                    formData.Add(new StringContent(mdl.Area[i].Area.ToString()), $"Area[{i}].Area");

                }
                formData.Add(new StringContent(mdl.Latitude ?? ""), "Latitude");
                formData.Add(new StringContent(mdl.Longitude ?? ""), "Longitude");
                if(filebytes != null)
                {
                    formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.ProfileImage.FileName );
                }
                


                var result = await _HttpCleintWrapper.PostMultipartFormData<ProductResponse>("/Distributor/", formData);
                return result;
            }
           
        }
        

        /// <summary>
        /// UPDATE DISTRIBUTOR BY ID //
        /// </summary>

        public async Task<ProductResponse> UpdateDistributor(string Id, ExecutiveModel mdl)
        {
            if (mdl.ProfileImage != null)
            {
                using (var formData = new MultipartFormDataContent())
                {
                    byte[] filebytes;
                    using (var ms = new MemoryStream())

                    {
                        await mdl.ProfileImage.CopyToAsync(ms);
                        filebytes = ms.ToArray();
                    }
                    formData.Add(new StringContent(mdl.FirstName), "FirstName");
                    formData.Add(new StringContent(mdl.LastName), "LastName");
                    formData.Add(new StringContent(mdl.Email ?? ""), "Email");
                    formData.Add(new StringContent(mdl.Address?? ""), "Address");
                    formData.Add(new StringContent(mdl.MobileNumber), "MobileNumber");
                    formData.Add(new StringContent(mdl.UserName), "UserName");
                    formData.Add(new StringContent(mdl.Password), "Password");
                    formData.Add(new StringContent(mdl.State), "State");
                    formData.Add(new StringContent(mdl.City), "City");
                    for (int i = 0; i < mdl.Area.Count; i++)
                    {
                        formData.Add(new StringContent(mdl.Area[i].Area.ToString()), $"Area[{i}].Area");

                    }
                    formData.Add(new StringContent(mdl.Latitude?? ""), "Latitude");
                    formData.Add(new StringContent(mdl.Longitude?? ""), "Longitude");
                    formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.ProfileImage.FileName);
                    var output = await _HttpCleintWrapper.PutMultipartFormData<ProductResponse>($"/Distributor?DistributorId={Id}", formData);
                    
                    return output;
                }
            }
            else
            {
                using (var formData = new MultipartFormDataContent())
                {

                    formData.Add(new StringContent(mdl.FirstName), "FirstName");
                    formData.Add(new StringContent(mdl.LastName), "LastName");
                    formData.Add(new StringContent(mdl.Email ?? ""), "Email");
                    formData.Add(new StringContent(mdl.Address?? ""), "Address");
                    formData.Add(new StringContent(mdl.MobileNumber), "MobileNumber");
                    formData.Add(new StringContent(mdl.UserName), "UserName");
                    formData.Add(new StringContent(mdl.Password), "Password");
                    formData.Add(new StringContent(mdl.State), "State");
                    formData.Add(new StringContent(mdl.City), "City");
                    for (int i = 0; i < mdl.Area.Count; i++)
                    {
                        formData.Add(new StringContent(mdl.Area[i].Area.ToString()), $"Area[{i}].Area");

                    }
                    formData.Add(new StringContent(mdl.Latitude ?? ""), "Latitude");
                    formData.Add(new StringContent(mdl.Longitude?? ""), "Longitude");
                    var output = await _HttpCleintWrapper.PutMultipartFormData<ProductResponse>($"/Distributor?DistributorId={Id}", formData);
                    return output;

                }
            }
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

        public async Task<PaginationResult<DistributorModel>> SearchDistributor(SearchModel searchdistributor, bool? NonAssign)
        {
            // bool NonAssign = false;
            var SearchedResult = await _HttpCleintWrapper.SearchAsync<PaginationResult<DistributorModel>>("/Distributor?", searchdistributor, NonAssign);
            return SearchedResult;
        }



        /// <summary>
        /// GET ALL NON_ASSIGNED RETAILORS
        /// </summary>
        //public async Task<List<RetailorModel>> GetNonAssignedRetailors()
        //{
        //    var nonAssign = true;
        //    SearchModel search = new SearchModel();
        //    var getretailor = await _HttpCleintWrapper.SearchAsync<List<RetailorModel>>("/Retailor?nonAssign=", search,nonAssign);
        //    return getretailor;

        //}
        public async Task<List<RetailorModel>> SearchRetailor(SearchModel searchretailor)
        {
            var nonAssign = false;
            var SearchedResult = await _HttpCleintWrapper.SearchAsync<List<RetailorModel>>("/Retailor?", searchretailor, nonAssign);
            return SearchedResult;
        }
         
        public async Task<List<RetailorModel>> GetAssignedRetailorByDistributorId(string Disid)
        {
            var result = await _HttpCleintWrapper.GetByIdAsync<List<RetailorModel>>("/AssignRetailorToDistributor/details/",Disid);
             return result;
        }
        public async Task<List<RetailorModel>> SearchNonAssignedRetailors(SearchModel searchdistributor)
        {
            var nonAssign = true;
            var SearchedResult = await _HttpCleintWrapper.SearchAsync<List<RetailorModel>>("/Retailor?", searchdistributor, nonAssign);
            return SearchedResult;
        }

        public async Task<string> DeleteAssignedRetailor(string retailorId, string distributorId)
        {
            var delete = await _HttpCleintWrapper.DeleteAssignedDistirbutorAsync("/AssignRetailorToDistributor",$"{retailorId}/{distributorId}");
            return delete;
        }

        public async Task<PaginationResult<RetailorModel>> GetNonAssignedRetailors()
        {
            var nonAssign = true;
            SearchModel search = new SearchModel();
            var getexes = await _HttpCleintWrapper.SearchAsync<PaginationResult<RetailorModel>>($"/Retailor?nonAssig=", search, nonAssign);
            return getexes;


        }
        public async Task<PaginationResult<RetailorModel>> SearchNonAssignedRetailor(SearchModel search)
        {
            var nonAssign = true;
            
            var getexes = await _HttpCleintWrapper.SearchAsync<PaginationResult<RetailorModel>>($"/Retailor?", search, nonAssign);
            return getexes;


        }
       
    }


}