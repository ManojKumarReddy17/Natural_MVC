using Natural.Core.IServices;
using Natural.Core.Models;
using Newtonsoft.Json;
using System.Net.Http;


namespace Naturals.Service.Service
{
    public class RetailorService : IRetailorService
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
            var getretailorid = await _HttpCleintWrapper.GetByIdAsync<RetailorModel>("/Retailor/", Retailorid);
            return getretailorid;
        }

        /// <summary>
        /// GET RETAILOR DETAILS BY ID
        /// </summary>

        public async Task<RetailorModel> GetRetailorDetailsById(string retailorId)
        {
            var getretailorid = await _HttpCleintWrapper.GetByIdAsync<RetailorModel>("/Retailor/details/", retailorId);
            return getretailorid;

        }

        /// <summary>
        /// CREATE RETAILOR 
        /// </summary>

        public async Task<RetailorModel> CreateRetailor(RetailorModel mdl)
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
                    formData.Add(new StringContent(mdl.Email), "Email");
                    formData.Add(new StringContent(mdl.Address), "Address");
                    formData.Add(new StringContent(mdl.MobileNumber), "MobileNumber");
                    formData.Add(new StringContent(mdl.State), "State");
                    formData.Add(new StringContent(mdl.City), "City");
                    formData.Add(new StringContent(mdl.Area), "Area");
                    formData.Add(new StringContent(mdl.Latitude), "Latitude");
                    formData.Add(new StringContent(mdl.Longitude), "Longitude");
                    formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.ProfileImage.FileName);
                    var result = await _HttpCleintWrapper.PostMultipartFormData<RetailorModel>("/Retailor/", formData);
                    return result;
                }
            }
            else
            {
                using (var formData = new MultipartFormDataContent())
                {
                    
                    formData.Add(new StringContent(mdl.FirstName), "FirstName");
                    formData.Add(new StringContent(mdl.LastName), "LastName");
                    formData.Add(new StringContent(mdl.Email), "Email");
                    formData.Add(new StringContent(mdl.Address), "Address");
                    formData.Add(new StringContent(mdl.MobileNumber), "MobileNumber");
                    formData.Add(new StringContent(mdl.State), "State");
                    formData.Add(new StringContent(mdl.City), "City");
                    formData.Add(new StringContent(mdl.Area), "Area");
                    formData.Add(new StringContent(mdl.Latitude), "Latitude");
                    formData.Add(new StringContent(mdl.Longitude), "Longitude");
                    var result = await _HttpCleintWrapper.PostMultipartFormData<RetailorModel>("/Retailor/", formData);
                    return result;
                }
            }
                

        }

        /// <summary>
        /// UPDATE RETAILOR BY ID
        /// </summary>
        
        public async Task<RetailorModel> UpdateRetailor(string Id, RetailorModel mdl)
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
                    formData.Add(new StringContent(mdl.Email), "Email");
                    formData.Add(new StringContent(mdl.Address), "Address");
                    formData.Add(new StringContent(mdl.MobileNumber), "MobileNumber");
                    
                    formData.Add(new StringContent(mdl.State), "State");
                    formData.Add(new StringContent(mdl.City), "City");
                    formData.Add(new StringContent(mdl.Area), "Area");
                    formData.Add(new StringContent(mdl.Latitude), "Latitude");
                    formData.Add(new StringContent(mdl.Longitude), "Longitude");
                    formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.ProfileImage.FileName);
                    var output = await _HttpCleintWrapper.PutMultipartFormData<RetailorModel>($"/Retailor?RetailorId={Id}", formData);
                    return output;
                }
            }
            else
            {
                using (var formData = new MultipartFormDataContent())
                {

                    formData.Add(new StringContent(mdl.FirstName), "FirstName");
                    formData.Add(new StringContent(mdl.LastName), "LastName");
                    formData.Add(new StringContent(mdl.Email), "Email");
                    formData.Add(new StringContent(mdl.Address), "Address");
                    formData.Add(new StringContent(mdl.MobileNumber), "MobileNumber");
                    formData.Add(new StringContent(mdl.State), "State");
                    formData.Add(new StringContent(mdl.City), "City");
                    formData.Add(new StringContent(mdl.Area), "Area");
                    formData.Add(new StringContent(mdl.Latitude), "Latitude");
                    formData.Add(new StringContent(mdl.Longitude), "Longitude");
                    var output = await _HttpCleintWrapper.PutMultipartFormData<RetailorModel>($"/Retailor?RetailorId={Id}", formData);
                    return output;

                }
            }
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

        public async Task<List<RetailorModel>> SearchRetailor(SearchModel searchretailor, bool? NonAssign)
        {
            var SearchedResult = await _HttpCleintWrapper.SearchAsync<List<RetailorModel>>("/Retailor?", searchretailor, NonAssign);
            return SearchedResult;
        }
    }


}


   

