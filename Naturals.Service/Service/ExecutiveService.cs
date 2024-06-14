using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
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



        public async Task<List<ED_CreateModel>> GetExecutives()
        {
            var getexe = await _httpClient.GetAsync<List<ED_CreateModel>>("/Executive");
            return getexe;
        }



        /// <summary>
        /// GET EXECUTIVE DETAILS BY ID //
        /// </summary>
        public async Task<ED_CreateModel> GetExecutiveDetailsById(string ID)
        {

            var excdtlid = await _httpClient.GetByIdAsync<ED_CreateModel>("/Executive/details/", ID);
            return excdtlid;
        }

        /// <summary>
        /// Get Assigned Distributor to ExecutiveById
        /// </summary>
        /// <param name="ExecutiveId"></param>
        /// <returns></returns>

        public async Task<List<DistributorModel>> GetAssignedDistributorsByExecutiveId(string ExecutiveId)
        {
            var assineddistr = await _httpClient.GetByIdAsync<List<DistributorModel>>("/AssignDistributorToExecutive/Details/", ExecutiveId);
            return assineddistr;
        }

        /// <summary>
        /// GET EXECUTIVE BY ID //
        /// </summary>

        public async Task<ED_CreateModel> GetExecutiveById(string Id)
        {
            var output = await _httpClient.GetByIdAsync<ED_CreateModel>("/Executive/", Id);

            return output;
        }

        /// <summary>
        /// CREATE EXECUTIVE  //
        /// </summary>



        public async Task<ProductResponse> CreateExecutive(ExecutiveModel mdl)
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
                    formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.ProfileImage.FileName);


                    var createexe = await _httpClient.PostMultipartFormData<ProductResponse>("/Executive/", formData);
                    return createexe;
                }
            }

            else
            {
                using (var formData = new MultipartFormDataContent())
                {

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
                    var createexe = await _httpClient.PostMultipartFormData<ProductResponse>("/Executive/", formData);
                    return createexe;
                }


            }
        }


        public async Task<ProductResponse> UpdateExecutive(string Id, ExecutiveModel mdl)
        {
            if (mdl.Image != null)
            {
                using (var formData = new MultipartFormDataContent())
                {
                    byte[] filebytes;
                    using (var ms = new MemoryStream())

   

                    {
                        await mdl.ProfileImage.CopyToAsync(ms);
                        filebytes = ms.ToArray();
                    }
                    formData.Add(new StringContent(Id), "Id");
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
                        formData.Add(new StringContent(mdl.Area[i].Executive), $"Area[{i}].Executive");
                    }
                    formData.Add(new StringContent(mdl.Latitude ?? ""), "Latitude");
                    formData.Add(new StringContent(mdl.Longitude ?? ""), "Longitude");
                    formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.ProfileImage.FileName);
                 
                    var output = await _httpClient.PutMultipartFormData<ProductResponse>("/Executive/", formData);
                    return output;
                }
            }
            else
            {
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(Id), "Id");
                    formData.Add(new StringContent(mdl.FirstName), "FirstName");
                    formData.Add(new StringContent(mdl.LastName), "LastName");
                    formData.Add(new StringContent(mdl.Email ?? ""), "Email");
                    formData.Add(new StringContent(mdl.Address ?? ""), "Address");
                    formData.Add(new StringContent(mdl.MobileNumber), "MobileNumber");
                    formData.Add(new StringContent(mdl.UserName), "UserName");
                    formData.Add(new StringContent(mdl.Password), "Password");
                    formData.Add(new StringContent(mdl.State), "State");
                    formData.Add(new StringContent(mdl.City), "City");
                    if(mdl.Area != null)
                    {
                        for (int i = 0; i < mdl.Area.Count; i++)
                        {
                            formData.Add(new StringContent(mdl.Area[i].Area.ToString()), $"Area[{i}].Area");
                            formData.Add(new StringContent(mdl.Area[i].Executive), $"Area[{i}].Executive");
                        }
                    }

                    formData.Add(new StringContent(mdl.Latitude ?? ""), "Latitude");
                    formData.Add(new StringContent(mdl.Longitude ?? ""), "Longitude");
                   
                    var output = await _httpClient.PutMultipartFormData<ProductResponse>("/Executive/", formData);
                    return output;

                }
            }
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

       

        public async Task<List<ED_CreateModel>> SearchExecutive(SearchModel searchexecutive, bool? NonAssign)
        {
            var SearchedResult = await _httpClient.SearchAsync<List<ED_CreateModel>>("/Executive?", searchexecutive, NonAssign);
            return SearchedResult;
        }

        public async Task<List<DistributorModel>> GetNonAssignedDistributors()
        {
            var nonAssign = true;
            SearchModel search = new SearchModel();
            var getdistributor = await _httpClient.SearchAsync<List<DistributorModel>>("/Distributor?nonAssign=", search, nonAssign);
            return getdistributor;

        }
        public async Task<List<DistributorModel>>   SearchNonAssignedDistributors(SearchModel searchdistributor)
        {
            var nonAssign = true; 
            var SearchedResult = await _httpClient.SearchAsync<List<DistributorModel>>("/Distributor?", searchdistributor, nonAssign);
            return SearchedResult;
        }
        public async Task<string> DeleteAssignedDistributor(string distributorId, string executiveId)
        {
            var delete = await _httpClient.DeleteAssignedDistirbutorAsync("/AssignDistributorToExecutive", $"{distributorId}/{executiveId}");
            return delete;
        }

      
    }
}
