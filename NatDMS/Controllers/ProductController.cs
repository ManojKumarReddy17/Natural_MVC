using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;
using System.Drawing;
using System.Net.Http;

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
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<ActionResult<List<Product>>> DisplayImage()
        {
           var getproduct = await _ProductService.GetAllProduct();
           var viewmodel = _Mapper.Map<List<ProductModel>,List<Product>>(getproduct);
               //viewmodel.


            return View(viewmodel);
        }

        // GET: ProductController/Create
        public async Task<ActionResult> CreateAsync()
        {
            var viewModel = new Product
            {
              Categorylist   = await _CategoryService.GetCategories()
        };
            
            return View(viewModel);
        }

       

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(/*IFormCollection*/ Product collection)
        {
            var UpdateProduct = _Mapper.Map<Product, ProductModel>(collection);
            using (var formData = new MultipartFormDataContent())
            {
                byte[] filebytes;
                using (var ms = new MemoryStream())

                {
                    await collection.UploadImage.CopyToAsync(ms);
                    filebytes = ms.ToArray();
                }
                formData.Add(new StringContent(collection.Category), "Category");
                formData.Add(new StringContent(collection.Product1), "Product1");
                formData.Add(new StringContent(collection.Quantity), "Quantity");
                formData.Add(new StringContent(collection.Weight.ToString()), "Weight");
                formData.Add(new StringContent(collection.Price.ToString()), "Price");


                //formData.Add(new StreamContent(collection.Image.OpenReadStream()), "Image", collection.Image.FileName);

                formData.Add(new ByteArrayContent(filebytes), "UploadImage", collection.UploadImage.FileName);

                var endpoint = "/Product/";
                /*var createexe =*/ await _httpClient.PostAsync(_httpClient.BaseAddress + endpoint, formData);
                return RedirectToAction("DisplayDistributors", "Distributor");
            }
            //await _ProductService.CreateProduct(UpdateProduct);
            //var viewModel = new Product();

            //var image = collection["Image"]; 
            //var weight = collection["Weight"];
            //int weightValue = Convert.ToInt32(weight);
            //var viewModel = new Product { Weight = weightValue, Image= image
            //};


            //return RedirectToAction("DisplayDistributors", "Distributor");



        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
