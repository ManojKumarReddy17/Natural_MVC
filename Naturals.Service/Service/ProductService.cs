using Microsoft.Extensions.Options;
using Natural.Core.IServices;
using Natural.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace Naturals.Service.Service
{
    public class ProductService : IProductService
    {
        
        private readonly IHttpClientWrapper _httpClient;
       
       
  
        public ProductService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
       
        }
      

        public async Task<List<GetProduct>> GetAllProduct()
        {
            var getproduct = await _httpClient.GetAsync<List<GetProduct>>("/Product/");
            return getproduct;
        }

        public async Task<GetProduct> GetproductDetailsById(string ID)
        {
            
            var excdtlid = await _httpClient.GetByIdAsync<GetProduct>("/Product/details", ID);
            return excdtlid;
        }

        public async Task<GetProduct> GetproductById(string ID)
        {
            
            var excdtlid = await _httpClient.GetByIdAsync<GetProduct>("/Product", ID);
            return excdtlid;
        }


   

        public async Task<ProductResponse> CreateProduct(ProductModel mdl)
        {

            using (var formData = new MultipartFormDataContent())
            {
                byte[] filebytes;
                using (var ms = new MemoryStream())

                {
                    await mdl.UploadImage.CopyToAsync(ms);
                    filebytes = ms.ToArray();
                }
                formData.Add(new StringContent(mdl.Category), "Category");
                formData.Add(new StringContent(mdl.ProductName), "ProductName");
                formData.Add(new StringContent(mdl.Quantity), "Quantity");
                formData.Add(new StringContent(mdl.Weight.ToString()), "Weight");
                formData.Add(new StringContent(mdl.Price.ToString()), "Price");

                formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.UploadImage.FileName);

              
                var result = await _httpClient.PostMultipartFormData<ProductResponse>("/Product/", formData);
                

                return result;
            }

        }


        public async Task<ProductResponse> UpdateProduct(string id, ProductModel mdl)
        {
            if (mdl.UploadImage != null)
            {
                using (var formData = new MultipartFormDataContent())
                {
                    byte[] filebytes;
                    using (var ms = new MemoryStream())

                    {
                        await mdl.UploadImage.CopyToAsync(ms);
                        filebytes = ms.ToArray();
                    }
                    formData.Add(new StringContent(mdl.Id), "Id");
                    formData.Add(new StringContent(mdl.Category), "Category");
                    formData.Add(new StringContent(mdl.ProductName), "ProductName");
                    formData.Add(new StringContent(mdl.Quantity), "Quantity");
                    formData.Add(new StringContent(mdl.Weight.ToString()), "Weight");
                    formData.Add(new StringContent(mdl.Price.ToString()), "Price");

                    formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.UploadImage.FileName);
                    var result = await _httpClient.PutMultipartFormData<ProductResponse>("/Product/", formData);
                    return result;
                }
            }
            else
            {
                using (var formData = new MultipartFormDataContent())
                {

                    formData.Add(new StringContent(mdl.Id), "Id");
                    formData.Add(new StringContent(mdl.Category), "Category");
                    formData.Add(new StringContent(mdl.ProductName), "ProductName");
                    formData.Add(new StringContent(mdl.Quantity), "Quantity");
                    formData.Add(new StringContent(mdl.Weight.ToString()), "Weight");
                    formData.Add(new StringContent(mdl.Price.ToString()), "Price");
                    
                    var endpoint = "/Product/";
                    var response = await _httpClient.PutMultipartFormData<ProductResponse>("/Product/", formData);
                    return response;
                   
                }
            }

        }
        public async Task<bool> DeleteImage(string Id)
        {
            try
            {
                var isDeleted = await _httpClient.DeleteAsync("/Product", Id);

                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete distributor. Error: {ex.Message}");
            }
        }


        public async Task<bool> DeleteProduct(string ProductId)
        {
            try
            {
                var isDeleted = await _httpClient.DeleteAsync("/Product/delete", ProductId);

                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete distributor. Error: {ex.Message}");
            }
        }

       public async Task<List<GetProduct>> SearchProduct(ProductSearch SearchProduct)
        {
            var SearchedResult = await _httpClient.PostAsync<List<GetProduct>>("/Product/Search", SearchProduct);
            return SearchedResult;
        }
    }
}
