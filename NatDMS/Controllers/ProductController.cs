﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;
using Newtonsoft.Json;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;

namespace NatDMS.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IProductService _ProductService;
        private readonly IMapper _Mapper;
        private readonly IConfiguration _Configuration;
        private readonly HttpClient _httpClient;
        public ApiDetails _ApiDetails { get; set; }

        public ProductController(ICategoryService CategoryService, IProductService ProductService, IMapper mapper, IConfiguration Configuration, IOptions<ApiDetails> apiDetails, HttpClient httpClient)
        {
            _CategoryService = CategoryService;
            _Mapper = mapper;
            _Configuration = Configuration;
            _ProductService = ProductService;
            _httpClient = httpClient;
            _ApiDetails = apiDetails.Value;

            _httpClient = new HttpClient { BaseAddress = new Uri(_ApiDetails.Natrual_API) };
            _httpClient.DefaultRequestHeaders.Add("ApiKey", _ApiDetails.ApiKey);
        }
        // GET: ProductController
        public async Task<ActionResult<List<EditProduct>>> Index()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            //viewmodel.
            return View(viewmodel);
        }

        public async Task<ActionResult<List<EditProduct>>> Index2()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            //viewmodel.
            return View(viewmodel);
        }

        public async Task<ActionResult<List<EditProduct>>> Index3()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            //viewmodel.
            return View(viewmodel);
        }

        public async Task<ActionResult<List<EditProduct>>> Index4()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            //viewmodel.
            return View(viewmodel);
        }

        public async Task<ActionResult<List<EditProduct>>> Index5()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            //viewmodel.
            return View(viewmodel);
        }
        public async Task<ActionResult<List<EditProduct>>> Index6()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            //viewmodel.
            return View(viewmodel);
        }
        public async Task<ActionResult<List<EditProduct>>> Index7()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            //viewmodel.
            return View(viewmodel);
        }

        public async Task<ActionResult<List<EditProduct>>> Index8()
        {
            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            //viewmodel.
            return View(viewmodel);
        }
        public async Task<ActionResult<List<EditProduct>>> DisplayProduct()
        {

            var getproduct = await _ProductService.GetAllProduct();
            var viewmodel = _Mapper.Map<List<GetProduct>, List<EditProduct>>(getproduct);
            //viewmodel.
            return View(viewmodel);
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
            var RESPONSE = _ProductService.CreateProduct(UpdateProduct);
            return RedirectToAction("DisplayProduct", "Product");

        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> EditProduct(string id)
        {

            var produttdetails = await _ProductService.GetproductById(id);

            //var viewModel = _mapper.Map<ED_EditViewModel>(executive);
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
