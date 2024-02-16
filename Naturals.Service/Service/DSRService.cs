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




    }

}
