using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NatDMS.Controllers
{
    public class GuestProductController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IProductService _ProductService;
        private readonly IMapper _Mapper;
        private readonly IConfiguration _Configuration;


        public GuestProductController(ICategoryService CategoryService, IProductService ProductService, IMapper mapper, IConfiguration Configuration, IOptions<ApiDetails> apiDetails, HttpClient httpClient)
        {
            _CategoryService = CategoryService;
            _Mapper = mapper;
            _Configuration = Configuration;
            _ProductService = ProductService;

        }
        // GET: /<controller>/
        public async Task<ActionResult<List<EditProduct>>> Index8()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);

            return View(viewmodel);
        }

        public async Task<ActionResult<List<EditProduct>>> Index9()
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

        //[HttpPost]
        //public async Task<ActionResult<List<EditProduct>>> Index9(DisplayProduct_View search)
        //{


        //    return View();

        //}

        [HttpPost]
        public async Task<ActionResult<List<EditProduct>>> Index9(SearchProduct search)
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
<<<<<<< HEAD



        public async Task<ActionResult<List<EditProduct>>> Index10()
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
        public async Task<ActionResult<List<EditProduct>>> Index10(SearchProduct search)
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

        public async Task<ActionResult<List<EditProduct>>> Index12()
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


        public async Task<ActionResult<List<EditProduct>>> Index12(SearchProduct search)
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
=======
>>>>>>> 486ae68 (conflicts ressolved)



        public async Task<ActionResult<List<EditProduct>>> Index10()
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
        public async Task<ActionResult<List<EditProduct>>> Index10(SearchProduct search)
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

