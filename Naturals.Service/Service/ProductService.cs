
using Natural.Core.IServices;
using Natural.Core.Models;
using Natural_Core.Models;

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
        public async Task<PaginationResult<GetProduct>> GetAllProduct1(int Page, int pageSize = 10)
        {
            var getproduct = await _httpClient.GetAsync<PaginationResult<GetProduct>>($"/Product?page={Page}&pageSize={pageSize}");
            return getproduct;
        }
        //public async Task<PaginationResult<GetProduct>> GetAllProduct1(int page, int pageSize = 10)
        //{
        //    var query = $"/Product?page={page}";

        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        query += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";
        //    }

        //    var getproduct = await _httpClient.GetAsync<PaginationResult<GetProduct>>(query);
        //    return getproduct;
        //}


        public async Task<PaginationResult<GetProduct>> GetAllProduct()
        {
            var getproduct = await _httpClient.GetAsync<PaginationResult<GetProduct>>("/Product/");
            return getproduct;
        }
        public async Task<List<ProductType>> GetAllProductType()
        {
            var getproduct = await _httpClient.GetAsync<List<ProductType>>("/Product/GetAllPrtoductType");
            return getproduct;
        }



        public async Task<GetProduct> GetproductDetailsById(string ID)
        {
            
            var excdtlid = await _httpClient.GetByIdAsync<GetProduct>("/Product/details/", ID);
            return excdtlid;
        }


        public async Task<GetProduct> GetproductById(string ID)
        {
            
            var excdtlid = await _httpClient.GetByIdAsync<GetProduct>("/Product/Details/", ID);
            return excdtlid;
        }


   

       


        public async Task<ProductResponse> CreateProduct(ProductModel mdl)
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
                    formData.Add(new StringContent(mdl.Category), "Category");
                    formData.Add(new StringContent(mdl.ProductName), "ProductName");
                    formData.Add(new StringContent(mdl.Quantity.ToString()), "Quantity");
                    formData.Add(new StringContent(mdl.Weight.ToString()), "Weight");
                    formData.Add(new StringContent(mdl.ProductType.ToString()), "ProductType");
                    formData.Add(new StringContent(mdl.Price.ToString()), "Price");
                    formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.UploadImage.FileName);

                    var result = await _httpClient.PostMultipartFormData<ProductResponse>("/Product/", formData);

                    return result;
                }

            }
            else

                using (var formData = new MultipartFormDataContent())
                {

                   
                    formData.Add(new StringContent(mdl.Category), "Category");
                    formData.Add(new StringContent(mdl.ProductName), "ProductName");
                    formData.Add(new StringContent(mdl.Quantity.ToString()), "Quantity");
                    formData.Add(new StringContent(mdl.Weight.ToString()), "Weight");
                    formData.Add(new StringContent(mdl.ProductType.ToString()), "ProductType");
                    formData.Add(new StringContent(mdl.Price.ToString()), "Price");

                    
                    var response = await _httpClient.PostMultipartFormData<ProductResponse>("/Product/", formData);
                    return response;

                }




        }


        //public async Task<ProductResponse> UpdateProduct(string id, ProductModel mdl)
        //{
        //    if (mdl.UploadImage != null)
        //    {
        //        using (var formData = new MultipartFormDataContent())
        //        {
        //            byte[] filebytes;
        //            using (var ms = new MemoryStream())

        //            {
        //                await mdl.UploadImage.CopyToAsync(ms);
        //                filebytes = ms.ToArray();
        //            }
        //            formData.Add(new StringContent(mdl.Id), "Id");
        //            formData.Add(new StringContent(mdl.Category), "Category");
        //            formData.Add(new StringContent(mdl.ProductName), "ProductName");
        //            formData.Add(new StringContent(mdl.Quantity.ToString()), "Quantity");
        //            formData.Add(new StringContent(mdl.Weight.ToString()), "Weight");
        //            formData.Add(new StringContent(mdl.Price.ToString()), "Price");
        //            formData.Add(new StringContent(mdl.ProductType.ToString()), "ProductType");
        //            formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.UploadImage.FileName);
        //            var result = await _httpClient.PutMultipartFormData<ProductResponse>("/Product/", formData);
        //            return result;
        //        }
        //    }
        //    else
        //    {
        //        using (var formData = new MultipartFormDataContent())
        //        {

        //            formData.Add(new StringContent(mdl.Id), "Id");
        //            formData.Add(new StringContent(mdl.Category), "Category");
        //            formData.Add(new StringContent(mdl.ProductName), "ProductName");
        //            formData.Add(new StringContent(mdl.Quantity.ToString()), "Quantity");
        //            formData.Add(new StringContent(mdl.Weight.ToString()), "Weight");
        //            formData.Add(new StringContent(mdl.ProductType.ToString()), "ProductType");
        //            formData.Add(new StringContent(mdl.Price.ToString()), "Price");

        //            var endpoint = "/Product/";
        //            var response = await _httpClient.PutMultipartFormData<ProductResponse>("/Product/", formData);
        //            return response;

        //        }
        //    }

        //}
        public async Task<ProductResponse> UpdateProduct(string id, ProductModel mdl)
        {
            if (mdl == null)
            {
                throw new ArgumentNullException(nameof(mdl));
            }
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(mdl.Id ?? string.Empty), "Id");
                formData.Add(new StringContent(mdl.Category ?? string.Empty), "Category");
                formData.Add(new StringContent(mdl.ProductName ?? string.Empty), "ProductName");
                formData.Add(new StringContent(mdl.Quantity.ToString()), "Quantity");
                formData.Add(new StringContent(mdl.Weight.ToString()), "Weight");
                formData.Add(new StringContent(mdl.Price.ToString()), "Price");
                formData.Add(new StringContent(mdl.ProductType?.ToString() ?? string.Empty), "ProductType");
                if (mdl.UploadImage != null)
                {
                    byte[] filebytes;
                    using (var ms = new MemoryStream())
                    {
                        await mdl.UploadImage.CopyToAsync(ms);
                        filebytes = ms.ToArray();
                    }
                    if (filebytes != null && !string.IsNullOrEmpty(mdl.UploadImage.FileName))
                    {
                        formData.Add(new ByteArrayContent(filebytes), "UploadImage", mdl.UploadImage.FileName);
                    }
                }
                var response = await _httpClient.PutMultipartFormData<ProductResponse>("/Product/", formData);
                return response;
            }
        }







        public async Task<bool> DeleteImage(string Id)
        {
            try
            {
                var isDeleted = await _httpClient.DeleteAsync("/Product/Delete", Id);

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
                bool deleteEntireProduct = true;
                var isDeleted = await _httpClient.EntireDeleteAsync("/Product/Delete", ProductId, deleteEntireProduct);

                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete distributor. Error: {ex.Message}");
            }
        }


       public async Task<PaginationResult<GetProduct>> SearchProduct(ProductSearch SearchProduct)
        {
            bool NonAssign = false;
            var SearchedResult = await _httpClient.SearchAsync<PaginationResult<GetProduct>>("/Product?", SearchProduct, NonAssign);
            return SearchedResult;
        }
    }
}
