using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Natural_Core.Models;

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


        
        [HttpGet]
        public async Task<ActionResult<DisplayProduct_View>> DisplayProduct(int page = 1)
        {
            const int pageSize = 10; // Adjust this based on your pagination size

            // Get paginated products
            var productResult = await _ProductService.GetAllProduct1(page);

            // Map GetProduct to EditProduct
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(productResult.Items);

            // Calculate pagination details
            int totalItems = productResult.TotalItems;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            int currentPage = page;

            // Get categories
            var categoryResult = await _CategoryService.GetCategories();

            // Prepare the view model
            var viewModel = new DisplayProduct_View
            {
                CategoryList = categoryResult,
                product = viewmodel, // Use the correct property for the list of products
                CurrentPage = currentPage,
                TotalPageCount = totalPages,
                TotalItems = totalItems
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductType>>> DisplayProductType()
        {
            var getproduct = await _ProductService.GetAllProductType();
            return Json(getproduct);
        }



        //[HttpPost]

        //public async Task<ActionResult<DisplayProduct_View>> DisplayProduct(SearchProduct search, int page = 1, int pageSize = 10)
        //{
        //    var searchModel = _Mapper.Map<SearchProduct, ProductSearch>(search);
        //    var getProductResult = await _ProductService.SearchProduct(searchModel);
        //    var searchResult = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getProductResult.Items);
        //    var categoryResult = await _CategoryService.GetCategories();

        //    if (searchResult == null)
        //    {
        //        searchResult = new List<EditProduct>();
        //    }

        //    int totalItems = searchResult.Count;
        //    int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        //    // Ensure that the page number is within the valid range
        //    page = page < 1 ? 1 : page;
        //    page = page > totalPages ? totalPages : page;

        //    var paginatedItems = searchResult.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        //    DisplayProduct_View viewModel = new DisplayProduct_View
        //    {
        //        CategoryList = categoryResult,
        //        product = paginatedItems,
        //        CurrentPage = page,
        //        TotalPageCount = totalPages
        //    };

        //    return View(viewModel);
        //}
        [HttpPost]
        public async Task<ActionResult<DisplayProduct_View>> DisplayProduct(SearchProduct search)
        {
            var searchModel = _Mapper.Map<SearchProduct, ProductSearch>(search);
            var getProductResult = await _ProductService.SearchProduct(searchModel);
            var searchResult = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getProductResult.Items);
            var categoryResult = await _CategoryService.GetCategories();

            if (searchResult == null)
            {
                searchResult = new List<EditProduct>();
            }

            DisplayProduct_View viewModel = new DisplayProduct_View
            {
                CategoryList = categoryResult,
                product = searchResult,
                CurrentPage = 1,  // Can be removed or set to a default value
                TotalPageCount = 1  // Can be removed or set to a default value
            };

            return View(viewModel);
        }












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
            UpdateProduct.Category = collection.CategoryId;
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
