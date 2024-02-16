using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;
using Newtonsoft.Json;

namespace NatDMS.Controllers
{
    public class DSRController : Controller
    {
        private readonly IDSRService _dsrservice;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryservice;
        public DSRController(IDSRService dsrservice,IMapper mapper, ICategoryService categoryservice)
        {
            _dsrservice = dsrservice;
            _mapper = mapper;
            _categoryservice = categoryservice;
        }

        public async Task<ActionResult<List<DSRModel>>> DisplayDsrs()
        {
            var dsrResult = await _dsrservice.GetDsrAll();
            var mapped = _mapper.Map<List<DSRModel>, List<DSRViewModel>>(dsrResult);

            return View(mapped);
        }

        public async Task<ActionResult<Dsrcreate>> CreateDsr()
        {

            var viewmodel = await _dsrservice.CreateDsr();
            string dsrjson = JsonConvert.SerializeObject(viewmodel);
            HttpContext.Session.SetString("DSR", dsrjson);

            string dsrjsonFromSession = HttpContext.Session.GetString("DSR");

            Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);
            return View(deserializedViewModel);
        }

        public async Task<JsonResult> GetDistributorByExecutiveId(string executiveId)
        {
            var result = await _dsrservice.AssignedDistributorDetailsByExecutiveId(executiveId);
            //var mapped = _mapper.Map<IEnumerable<DsrDistributor>, IEnumerable<DsrDistributorDrop>>(result);

            return Json(result);
        }


        public async Task<JsonResult> GetRetailorByDistributorId(string distributorId)
        {
            var result = await _dsrservice.GetAssignedRetailorDetailsByDistributorId(distributorId);
            //var mapped = _mapper.Map<IEnumerable<DsrRetailor>, IEnumerable<DsrRetailorDrop>>(result);

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> SearchProduct([FromBody] Dsrcreate model)
        //public async Task<ActionResult> SearchProduct([FromBody] Dsrcreate model)
        {

            string update = JsonConvert.SerializeObject(model);
            HttpContext.Session.SetString("DSR", update);

            string dsrjsonFromSession = HttpContext.Session.GetString("DSR");
            //List<Dsrcreate> deserializedViewModel = JsonConvert.DeserializeObject<List<Dsrcreate>>(dsrjsonFromSession);
            Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);
            //string category = deserializedViewModel.Category;
            //CategoryModel category1 = new CategoryModel();
            var categoryName = await _categoryservice.GetCategoryById(deserializedViewModel.Category);
            string category = categoryName.CategoryName;
            string product = deserializedViewModel.Product;
            List<DsrProduct> productlist = deserializedViewModel.ProductList;

            List<DsrProduct> sear = new List<DsrProduct>();
            sear = productlist
            .Where(c =>
                   (string.IsNullOrEmpty(category) || c.Category.StartsWith(category)) &&
                   (string.IsNullOrEmpty(product) || c.ProductName.StartsWith(product, StringComparison.OrdinalIgnoreCase))
               )
               .Select(c => new DsrProduct
               {
                   Id = c.Id,
                   Category = c.Category,
                   ProductName = c.ProductName,
                   Price = c.Price,
                   Quantity = c.Quantity,
                   Weight = c.Weight

               })
               .ToList();

            return Json(sear);

        }

    }
}
