using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;


namespace NatDMS.Controllers
{
    public class GuestProductController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IProductService _ProductService;
        private readonly IMapper _Mapper;

        public GuestProductController(ICategoryService CategoryService, IProductService ProductService, IMapper mapper)
        {
            _CategoryService = CategoryService;
            _Mapper = mapper;
            _ProductService = ProductService;

        }


        // GET: /<controller>/ // this controller to display products with no search options//
        public async Task<ActionResult<List<EditProduct>>> Index8()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);

            return View(viewmodel);
        }

        //To Display The Products in Login Page  
        public async Task<ActionResult<List<EditProduct>>> Index11()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            var category1 = await _CategoryService.GetCategories();
            DisplayProduct_View viewmodel1 = new DisplayProduct_View
            {
                CategoryList = category1,
                product = viewmodel

            };

            return View(viewmodel1);
        }


        [HttpPost]
        public async Task<ActionResult<List<EditProduct>>> Index11(SearchProduct search)
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

      
    }


}

