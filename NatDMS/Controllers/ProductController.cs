using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;


namespace NatDMS.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IProductService _ProductService;
        private readonly IMapper _Mapper;
        private readonly IConfiguration _Configuration;
        

        public ProductController(ICategoryService CategoryService, IProductService ProductService, IMapper mapper, IConfiguration Configuration, IOptions<ApiDetails> apiDetails, HttpClient httpClient)
        {
            _CategoryService = CategoryService;
            _Mapper = mapper;
            _Configuration = Configuration;
            _ProductService = ProductService;
           
        }


        // GET: ProductController
      

        [HttpGet]
        public async Task<ActionResult<List<EditProduct>>> DisplayProduct(int page = 1)
        {

            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
<<<<<<< HEAD
            
            var distributorPgn = new PageNation<EditProduct>(viewmodel, _Configuration, page);

            var paginatedData = distributorPgn.GetPaginatedData(viewmodel);
            ViewBag.Pages = distributorPgn;

=======
>>>>>>> 486ae68 (conflicts ressolved)
            var category1 = await _CategoryService.GetCategories();
            DisplayProduct_View viewmodel1 = new DisplayProduct_View
            {
                CategoryList = category1,
<<<<<<< HEAD
                product = paginatedData

            };

            return View(viewmodel1);
        }


        [HttpPost]
        public async Task<ActionResult<List<EditProduct>>> DisplayProduct(SearchProduct search)
        {
            var Searchmodel = _Mapper.Map<SearchProduct, ProductSearch>(search);
            var getproduct = await _ProductService.SearchProduct(Searchmodel);
            var SearchResult = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);

           
            

            var category1 = await _CategoryService.GetCategories();
            DisplayProduct_View viewmodel = new DisplayProduct_View
            {
                CategoryList = category1,
                product = SearchResult
            };
            return View(viewmodel);
        }


        //    [HttpPost]


        //public async Task<ActionResult<List<EditProduct>>> DisplayProduct(SearchProduct search)
        //{

        //    var Searchmodel = _Mapper.Map<SearchProduct, ProductSearch>(search);
        //    var getproduct = await _ProductService.SearchProduct(Searchmodel);
        //    var SearchResult = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);

        //    var category1 = await _CategoryService.GetCategories();
        //    DisplayProduct_View viewmodel = new DisplayProduct_View
        //    {
        //        CategoryList = category1,
        //        product = SearchResult

        //    };

        //    return View(viewmodel);
        //}

        //[HttpPost]
        //public async Task<ActionResult<List<EditProduct>>> DisplayProduct(SearchProduct search, int page = 1)
        //{
        //    var Searchmodel = _Mapper.Map<SearchProduct, ProductSearch>(search);
        //    var getproduct = await _ProductService.SearchProduct(Searchmodel);
        //    var SearchResult = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);

        //    var distributorPgn = new PageNation<EditProduct>(SearchResult, _Configuration, page);
        //    var paginatedData = distributorPgn.GetPaginatedData(SearchResult);

        //    var category1 = await _CategoryService.GetCategories();
        //    DisplayProduct_View viewmodel = new DisplayProduct_View
        //    {
        //        CategoryList = category1,
        //        product = paginatedData
        //    };

        //    return View(viewmodel);
        //}

        //public async Task<ActionResult<List<EditProduct>>> DisplayProduct(SearchProduct search, int page = 1)
        //{

        //    var Searchmodel = _Mapper.Map<SearchProduct, ProductSearch>(search);
        //    var getproduct = await _ProductService.SearchProduct(Searchmodel);
        //    var SearchResult = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
        //    var distributorPgn1 = new PageNation<EditProduct>(SearchResult, _Configuration, page);

        //    var paginatedData1 = distributorPgn1.GetPaginatedData(SearchResult);
        //    ViewBag.Pages = distributorPgn1;
        //    var category1 = await _CategoryService.GetCategories();
        //    DisplayProduct_View viewmodel = new DisplayProduct_View
        //    {
        //        CategoryList = category1,
        //        product = paginatedData1

        //    };

        //    return PartialView("_DisplayProduct", viewmodel);
        //}


=======
                product = viewmodel

            };

            return View(viewmodel1);
        }
        [HttpPost]
        public async Task<ActionResult<List<EditProduct>>> DisplayProduct(SearchProduct search)
        {
            var Searchmodel = _Mapper.Map<SearchProduct, ProductSearch>(search);
            var getproduct = await _ProductService.SearchProduct(Searchmodel);
            var SearchResult = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);

            var category1 = await _CategoryService.GetCategories();
            DisplayProduct_View viewmodel1 = new DisplayProduct_View
            {
                CategoryList = category1,
                product = SearchResult

            };

            return View(viewmodel1);
        }
>>>>>>> 486ae68 (conflicts ressolved)
        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var ProductById = await _ProductService.GetproductDetailsById(id);
            var mapped = _Mapper.Map<GetProduct, EditProduct>(ProductById);
            return View(mapped);
        }

        // GET: ProductController/Create
        public async Task<ActionResult> CreateAsync()
        {
            var viewModel = new Product
            {
                Categorylist = await _CategoryService.GetCategories()
            };

            return View(viewModel);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<ActionResult> CreateAsync(/*IFormCollection*/ Product collection)
        {
            var UpdateProduct = _Mapper.Map<Product, ProductModel>(collection);
            var RESPONSE = await _ProductService.CreateProduct(UpdateProduct); 
            var result = _Mapper.Map<ProductResponse, ProductResult>(RESPONSE);

            return RedirectToAction("Details", new { id = result.Id });
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> EditProduct(string id)
        {

            var produttdetails = await _ProductService.GetproductById(id);   
            var viewModel = _Mapper.Map<GetProduct, EditProduct>(produttdetails);
            viewModel.Categorylist = await _CategoryService.GetCategories();
            return View(viewModel);
        }

        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        // POST: ProductController/Edit/5

        [HttpPost]
        public async Task<ActionResult> EditProduct(EditProduct collection, string id)
        {
            var UpdateProduct = _Mapper.Map<EditProduct, ProductModel>(collection);
            
            var res = await _ProductService.UpdateProduct(id, UpdateProduct);
            
            return RedirectToAction("Details", new { id = id });
           
        }

        // GET: ProductController/Delete/5
        //to delete image
        public async Task<ActionResult> DeleteAsync(String Id)
        {
            var result = await _ProductService.DeleteImage(Id);

            return Json(result);

            
        }

        //to delete product data(image also)
        public async Task<IActionResult> DeleteProduct(string ProductId)
        {

            await _ProductService.DeleteProduct(ProductId);
            return RedirectToAction("DisplayProduct", "Product");
        }





    }
}
