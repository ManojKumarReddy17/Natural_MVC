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
using PagedList;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NatDMS.Controllers
{
    public class DisController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IProductService _ProductService;
        private readonly IMapper _Mapper;
        private readonly IConfiguration _Configuration;

        public DisController(ICategoryService CategoryService, IProductService ProductService, IMapper mapper, IConfiguration Configuration, IOptions<ApiDetails> apiDetails, HttpClient httpClient)
        {
            _CategoryService = CategoryService;
            _Mapper = mapper;
            _Configuration = Configuration;
            _ProductService = ProductService;

        }
        [HttpGet]
        public async Task<ActionResult<List<EditProduct>>> DisplayProduct(SearchProduct? search,int page = 1)
        {
           List< EditProduct> viewmodel = new List<EditProduct>();
            if (search != null)
            {
                var Searchmodel = _Mapper.Map<SearchProduct, ProductSearch>(search);
                var getproduct = await _ProductService.SearchProduct(Searchmodel);
                viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            }
            else {
                var getproduct = await _ProductService.GetAllProduct();
                viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);

            }

            var Pgn = new PageNation<EditProduct>(viewmodel, _Configuration, page);

            var paginatedData = Pgn.GetPaginatedData(viewmodel);
            ViewBag.Pages = Pgn;

            var category1 = await _CategoryService.GetCategories();
            DisplayProduct_View viewmodel1 = new DisplayProduct_View
            {
                CategoryList = category1,
                product = paginatedData

            };

            return View(viewmodel1);
        }
        public async Task<ActionResult<List<EditProduct>>> DisplayProduct1(SearchProduct? search, int? page )
        {
            List<EditProduct> viewmodel = new List<EditProduct>();
            IPagedList<DisplayProduct_View> STU = null;
            if (search != null)
            {
                var Searchmodel = _Mapper.Map<SearchProduct, ProductSearch>(search);
                var getproduct = await _ProductService.SearchProduct(Searchmodel);
                viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
                
            }
            else
            {
                var getproduct = await _ProductService.GetAllProduct();
                viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);

            }

            //var Pgn = new PageNation<EditProduct>(viewmodel, _Configuration, page);

            //var paginatedData = Pgn.GetPaginatedData(viewmodel);
            //ViewBag.Pages = Pgn;

            var category1 = await _CategoryService.GetCategories();
            DisplayProduct_View viewmodel1 = new DisplayProduct_View
            {
                CategoryList = category1,
                product = (List<EditProduct>)viewmodel.ToPagedList(page ?? 1, 3)

        };
            
            return View(viewmodel1);
        }
    }
}

