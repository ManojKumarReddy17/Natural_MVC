using System.Net.Http;
using AutoMapper;
using Natural.Core.IServices;
using Natural.Core.Models;



namespace Naturals.Service.Service
{
    public class DSRService : IDSRService
    {

        private readonly IHttpClientWrapper _HttpCleintWrapper;
        private readonly IUnifiedService _unifiedService;
        private readonly IExecutiveService _ExecutiveService;
        private readonly ICategoryService _categoryservice;
        private readonly IProductService _ProductService;
        private readonly IMapper _mapper;

        public DSRService(IHttpClientWrapper httpCleintWrapper, IUnifiedService unifiedService, IExecutiveService executiveService, ICategoryService  categoryservice, IProductService ProductService, IMapper mapper)
        {
            _HttpCleintWrapper = httpCleintWrapper;
            _unifiedService = unifiedService;
            _ExecutiveService = executiveService;
            _categoryservice = categoryservice;
              _ProductService = ProductService;
            _mapper = mapper;
        }

        public Task<DSRModel> CreateDsr(DSRModel dsr)
        {
            throw new NotImplementedException();
        }

        public async Task<DsrDisplay> GetDsrAll()
        {

            //List<ExecutiveModel> executives = await _ExecutiveService.GetAllExecutives();
            List<ED_CreateModel> executives = await _ExecutiveService.GetExecutives();
            List<DsrExecutiveDrop> executiveList = executives.Select(c => new DsrExecutiveDrop
            {
                Id = c.Id,
                Executive = string.Concat(c.FirstName, " ", c.LastName),
            }).ToList();

            var response = await _HttpCleintWrapper.GetAsync<List<DSRModel>>("/Dsr/");

            DsrDisplay dsrDisplay = new DsrDisplay()
            {
                ExecutiveList = executiveList,
                dsr = response
            };
            return dsrDisplay;
            
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
            //List<ExecutiveModel> executives = await _ExecutiveService.GetAllExecutives();
            List<ED_CreateModel> executives = await _ExecutiveService.GetExecutives();
            List<DsrExecutiveDrop> executiveList = executives.Select(c => new DsrExecutiveDrop
            {
                Id = c.Id,
                Executive = string.Concat(c.FirstName, " ", c.LastName),
            }).ToList();

            List<CategoryModel> categories = await _categoryservice.GetCategories();
           
            var products = await GetProductAsync();

            var create = products.Select(c => new DsrProduct
            {
                Id = c.Id,
                ProductName = c.ProductName,
                Category = c.Category,
                Price = c.Price,
                Weight = c.Weight

            }).ToList();

            Dsrcreate viewmodel = new Dsrcreate()
            {
                ExecutiveList = executiveList,
                CategoryList = categories,
                ProductList = create
            };

            return viewmodel;
        }

 

        public async Task<List<DsrProduct>> SearchProductsAsync(Dsrcreate ExistingSession, Dsrcreate UpadeSession)
        {
            ProductSearch search = new ProductSearch();
            string? category;
          
            if (!string.IsNullOrEmpty(UpadeSession.Category))
            {
                 search.Category  =   UpadeSession.Category;
                 var categoryName =   await _categoryservice.GetCategoryById(search.Category);
                 category         =   categoryName.CategoryName;
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



        //before updating the session while creating we bind the differnce from updated data to existing data
        public async Task<Dsrcreate> UpdateSession(Dsrcreate ExistingSession, Dsrcreate UpadeSession)
        {

            {
                var updatedprodctList = (from s1 in ExistingSession.ProductList
                                         join s2 in UpadeSession.ProductList on s1.Id equals s2.Id into gj
                                         from s2 in gj.DefaultIfEmpty()
                                         select s2 != null ? s2 : s1).ToList();

              





                Dsrcreate updatemodel = new Dsrcreate()
                {
                    ExecutiveList = ExistingSession.ExecutiveList,
                    CategoryList = ExistingSession.CategoryList,
                    Executive = UpadeSession.Executive,
                    Distributor = UpadeSession.Distributor,
                    Retailor = UpadeSession.Retailor,
                    ProductList = updatedprodctList,
                    dsrid = ExistingSession.dsrid != null ? ExistingSession.dsrid : null, // we neeed this while calling api
                    CreatedDate = UpadeSession.CreatedDate
                };

            return updatemodel;
        }

        }


       



        //public async Task<DsrInsert> Insert(Dsrcreate ExistingSession)
      public async Task<DsrInsert> onlyUpdateaInsert(Dsrcreate ExistingSession)
        {
            var Total = ExistingSession.ProductList.Sum(item => item.Total);
            List<DsrProduct> oldproduct = await GetProductAsync();
            List<DsrProduct> newproduct = ExistingSession.ProductList;

            var differentProducts = ExistingSession.ProductList.Where(s => s.Quantity != null)
                                                                .Select(s => new DsrInsertProduct
                                                                {
                                                                    Product = s.Id,
                                                                    Quantity = s.Quantity,
                                                                    Price = s.Price
                                                                })
                                                                .ToList();

            var inserter = _mapper.Map<Dsrcreate, DsrInsert>(ExistingSession);

            inserter.product = differentProducts;
            inserter.dsrid = ExistingSession.dsrid;
            inserter.TotalAmount = Total;
            inserter.OrderBy = "Admin123";




            return inserter;

        }

        public async Task<ProductResponse> Insert(DsrInsert Insertdata)
        {

            var result = await _HttpCleintWrapper.PostAsync<ProductResponse>("/Dsr/", Insertdata);
            return result;

        }

        public async Task<ProductResponse> Updatedsr(DsrInsert updatedata)
        {
            string id = updatedata.dsrid;
            

            var result = await _HttpCleintWrapper.PutAsync<ProductResponse>("/Dsr",id, updatedata);
            return result;

        }

        public async Task<DsrInsert> Details(string dsrid)

        {
            var dsr = await _HttpCleintWrapper.GetByIdAsync<DsrInsert>("/Dsr/ById", dsrid);
            return dsr;

        }

        public async Task<List<DSRModel>>  dsrsearch(Dsrview dsr)

        {
            var dsrr = await _HttpCleintWrapper.PostAsync<List<DSRModel>>("/Dsr/Search", dsr);
            return dsrr;

        }

        public async Task<bool> DeleteDsr(string DsrId)
        {
            try
            {
                var isDeleted = await _HttpCleintWrapper.DeleteAsync("/Dsr", DsrId);

                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete distributor. Error: {ex.Message}");
            }
        }

     

        public async Task<Dsrcreate> editDsr(string dsrid)

        {
            var dsrempty = await CreateDsr();
            
            var dsrids = await _HttpCleintWrapper.GetByIdAsync<Dsredit>("/Dsr/ids", dsrid);
           

         var    editda = _mapper.Map<Dsredit, Dsrcreate>(dsrids);
            editda.ProductList = dsrids.dsrdetail;




            dsrempty.ProductList = (from P in dsrempty.ProductList
                                    join Pr in editda.ProductList
                                    on P.Id equals Pr.Id into newurl
                                    from sub in newurl.DefaultIfEmpty()
                                    select new DsrProduct()
                                    {
                                        Id = P.Id,
                                        Category = P.Category,
                                        ProductName = P.ProductName,
                                        Price = (sub != null && sub.Price != null) ? sub.Price : P.Price,
                                        Quantity = (sub != null && sub.Quantity != null) ? sub.Quantity : P.Quantity,
                                        Weight = P.Weight
                                    }).ToList();


            
            var resu = await AssignedDistributorDetailsByExecutiveId(dsrids.Executive);
            dsrempty.DistributorList = _mapper.Map<List<DsrDistributor>, List<DsrDistributorDrop>>(resu);
            var rstaile = await GetAssignedRetailorDetailsByDistributorId(dsrids.Distributor);
            dsrempty.RetailorList = _mapper.Map<List<DsrRetailor>, List<DsrRetailorDrop>>(rstaile);
            dsrempty.Executive = dsrids.Executive;
            dsrempty.Distributor = dsrids.Distributor;
            dsrempty.Retailor = dsrids.Retailor;
            return dsrempty;


        }



       public async Task<List<DsrExecutiveDrop>> GetExecutive()
        {

            //List<ExecutiveModel> executives = await _ExecutiveService.GetAllExecutives();
            List<ED_CreateModel> executives = await _ExecutiveService.GetExecutives();
            List<DsrExecutiveDrop> executiveList = executives.Select(c => new DsrExecutiveDrop
            {
                Id = c.Id,
                Executive = string.Concat(c.FirstName, " ", c.LastName),
            }).ToList();

            return executiveList;

        }

    }

}
