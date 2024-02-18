using Natural.Core.IServices;
using Natural.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Naturals.Service.Service
{
    public class DSRService : IDSRService
    {

        private readonly IHttpClientWrapper _HttpCleintWrapper;
        private readonly IUnifiedService _unifiedService;
        private readonly IExecutiveService _ExecutiveService;
        private readonly ICategoryService _categoryservice;
        private readonly IProductService _ProductService;

        public DSRService(IHttpClientWrapper httpCleintWrapper, IUnifiedService unifiedService, IExecutiveService executiveService, ICategoryService  categoryservice, IProductService ProductService)
        {
            _HttpCleintWrapper = httpCleintWrapper;
            _unifiedService = unifiedService;
            _ExecutiveService = executiveService;
            _categoryservice = categoryservice;
              _ProductService = ProductService;
    }

        public Task<DSRModel> CreateDsr(DSRModel dsr)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DSRModel>> GetDsrAll()
        {
            var response = await _HttpCleintWrapper.GetAsync<List<DSRModel>>("/Dsr/");
            return response;
            
        }

        public async Task<List<DsrProduct>> GetProductAsync()

        {
            var result = await _HttpCleintWrapper.GetAsync<List<DsrProduct>>("/Dsr/Product");

            return result;
        }
        public async Task<List<DsrDistributor>> AssignedDistributorDetailsByExecutiveId(string Id)
        {
            var result = await _HttpCleintWrapper.GetByIdAsync<List<DsrDistributor>>("/Dsr/Details", Id);
            return result;

        }

        public async Task<List<DsrRetailor>> GetAssignedRetailorDetailsByDistributorId(string DistributorId)
        {

            var result = await _HttpCleintWrapper.GetByIdAsync<List<DsrRetailor>>("/Dsr", DistributorId);

            return result;


        }

        public async Task<Dsrcreate> CreateDsr()
        {
            List<ExecutiveModel> executives = await _ExecutiveService.GetAllExecutives();
            List<DsrExecutiveDrop> executiveList = executives.Select(c => new DsrExecutiveDrop
            {
                Id = c.Id,
                Executive = string.Concat(c.FirstName, " ", c.LastName),
            }).ToList();

            List<CategoryModel> categories = await _categoryservice.GetCategories();

            List<DsrProduct> products = await GetProductAsync();

            Dsrcreate viewmodel = new Dsrcreate()
            {
                ExecutiveList = executiveList,
                CategoryList = categories,
                ProductList = products
            };

      
            return viewmodel;

        }

       // public async Task<List<DsrProduct>> SearchProductsAsync(ProductSearch search, List<DsrProduct> productlist)
       // {
           
          
       //     var categoryName = await _categoryservice.GetCategoryById(search.Category);
       //     string category = categoryName.CategoryName;

       //     //List<DsrProduct> SearchedProduct = new List<DsrProduct>();
       //var      SearchedProduct = productlist
       //     .Where(c =>
       //            (string.IsNullOrEmpty(category) || c.Category.StartsWith(category)) &&
       //            (string.IsNullOrEmpty(search.ProductName) || c.ProductName.StartsWith(search.ProductName, StringComparison.OrdinalIgnoreCase))
       //        )
       //        .Select(c => new DsrProduct
       //        {
       //            Id = c.Id,
       //            Category = c.Category,
       //            ProductName = c.ProductName,
       //            Price = c.Price,
       //            Quantity = c.Quantity,
       //            Weight = c.Weight

       //        })
       //        .ToList();

       //     return SearchedProduct;

       // }


        //public async Task<List<DsrProduct>> SearchProductsAsync(Dsrcreate ExistingSession, Dsrcreate UpadeSession)
        //{

        //    ProductSearch search = new ProductSearch();
        //    search.Category = UpadeSession.Category;
        //    search.ProductName = UpadeSession.Product;
        //    List<DsrProduct> productlist = ExistingSession.ProductList;

        //    var categoryName = await _categoryservice.GetCategoryById(search.Category);
        //    string category = categoryName.CategoryName;

        //    //List<DsrProduct> SearchedProduct = new List<DsrProduct>();
        //    var SearchedProduct = productlist
        //         .Where(c =>
        //                (string.IsNullOrEmpty(category) || c.Category.StartsWith(category)) &&
        //                (string.IsNullOrEmpty(search.ProductName) || c.ProductName.StartsWith(search.ProductName, StringComparison.OrdinalIgnoreCase))
        //            )
        //            .Select(c => new DsrProduct
        //            {
        //                Id = c.Id,
        //                Category = c.Category,
        //                ProductName = c.ProductName,
        //                Price = c.Price,
        //                Quantity = c.Quantity,
        //                Weight = c.Weight

        //            })
        //            .ToList();

        //    return SearchedProduct;

        //}

        public async Task<List<DsrProduct>> SearchProductsAsync(Dsrcreate ExistingSession, Dsrcreate UpadeSession)
        {
            ProductSearch search = new ProductSearch();
            string? category;
            //if (UpadeSession.Category != null)
            // in case category is empty
            if (!string.IsNullOrEmpty(UpadeSession.Category))
            {
                 search.Category = UpadeSession.Category;
                 var categoryName = await _categoryservice.GetCategoryById(search.Category);
                 category = categoryName.CategoryName;
            }
            else
            {
                 category = null;
            }

            search.ProductName = UpadeSession.Product;
            List<DsrProduct> productlist = ExistingSession.ProductList;

            var SearchedProduct = productlist
                                  .Where(c =>
                                    (string.IsNullOrEmpty(category) || c.Category.StartsWith(category)) &&
                                    (string.IsNullOrEmpty(search.ProductName) || c.ProductName.StartsWith(search.ProductName, StringComparison.OrdinalIgnoreCase))
                                    )
                                   .Select(c => new DsrProduct
                                   {
                                     Id            = c.Id,
                                     Category      = c.Category,
                                     ProductName   = c.ProductName,
                                     Price         = c.Price,
                                     Quantity      = c.Quantity,
                                     Weight        = c.Weight,
                                     Total         = c.Total
                                   })
                                   .ToList();

            return SearchedProduct;

        }

        //public async Task<List<DsrProduct>> UpdateSession(List<DsrProduct> ExistingSession, List<DsrProduct> UpadeSession)
        //{
        //    var updatedprodctList = (from s1 in ExistingSession
        //                             join s2 in UpadeSession on s1.Id equals s2.Id into gj
        //                             from s2 in gj.DefaultIfEmpty()
        //                             select s2 != null ? s2 : s1).ToList();
        //    return updatedprodctList;

        //}


        //before updating the session while creating we bind the differnce from updated data to existing data
        public async Task<Dsrcreate> UpdateSession(Dsrcreate ExistingSession, Dsrcreate UpadeSession)
        {
           
            {
                var updatedprodctList = (from s1 in ExistingSession.ProductList
                                         join s2 in UpadeSession.ProductList on s1.Id equals s2.Id into gj
                                         from s2 in gj.DefaultIfEmpty()
                                         select s2 != null ? s2 : s1).ToList();

                //just to add other parameters to the upading session model executive list, category list may not be use full but untill we insert data its better to have
                //those lists in session so that no need to call api
                Dsrcreate updatemodel = new Dsrcreate()
                {
                    ExecutiveList = ExistingSession.ExecutiveList,
                    CategoryList = ExistingSession.CategoryList,
                    Executive = UpadeSession.Executive,
                    Distributor = UpadeSession.Distributor,
                    Retailor = UpadeSession.Retailor,
                    ProductList = updatedprodctList
                };

                return updatemodel;
            }
            
        }

      public async  Task<Dsrcreate> Insert(Dsrcreate ExistingSession)
        {
            var id = ExistingSession.ProductList.Sum(item => item.Total);
            var result = await _HttpCleintWrapper.PostAsync<Dsrcreate>("/Dsr/", ExistingSession);
            return result;

        }
    }

}
