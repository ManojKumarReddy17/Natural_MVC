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
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public DSRController(IDSRService dsrservice,IMapper mapper, IHttpContextAccessor HttpContextAccessor)
        {
            _dsrservice = dsrservice;
            _mapper = mapper;
            _HttpContextAccessor = HttpContextAccessor;
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
            _HttpContextAccessor.HttpContext.Session.SetString("DSR", dsrjson);

            string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");

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

        //[HttpPost]
        //public async Task<JsonResult> SearchProduct([FromBody] Dsrcreate model)
        ////public async Task<ActionResult> SearchProduct([FromBody] Dsrcreate model)
        //{

        //    //string update = JsonConvert.SerializeObject(model);
        //    //HttpContext.Session.SetString("DSR", update);

        //    string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");
        //    //List<Dsrcreate> deserializedViewModel = JsonConvert.DeserializeObject<List<Dsrcreate>>(dsrjsonFromSession);
        //    Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);
        //    //string category = deserializedViewModel.Category;
        //    //CategoryModel category1 = new CategoryModel();
        //    var categoryName = await _categoryservice.GetCategoryById(deserializedViewModel.Category);
        //    string category = categoryName.CategoryName;
        //    string product = deserializedViewModel.Product;
        //    List<DsrProduct> productlist = deserializedViewModel.ProductList;

        //    List<DsrProduct> sear = new List<DsrProduct>();
        //    sear = productlist
        //    .Where(c =>
        //           (string.IsNullOrEmpty(category) || c.Category.StartsWith(category)) &&
        //           (string.IsNullOrEmpty(product) || c.ProductName.StartsWith(product, StringComparison.OrdinalIgnoreCase))
        //       )
        //       .Select(c => new DsrProduct
        //       {
        //           Id = c.Id,
        //           Category = c.Category,
        //           ProductName = c.ProductName,
        //           Price = c.Price,
        //           Quantity = c.Quantity,
        //           Weight = c.Weight

        //       })
        //       .ToList();

        //    return Json(sear);

        //}



        //[HttpPost]
        //public async Task<JsonResult> SearchProduct([FromBody] Dsrcreate model)
        //{

        //    string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");

        //    Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);

        //    var updatedprodctList = from s1 in deserializedViewModel.ProductList
        //                            join s2 in model.ProductList on s1.Id equals s2.Id into gj
        //                            from s2 in gj.DefaultIfEmpty()
        //                            select s2 != null ? s2 : s1;


        //    Dsrcreate updatemodel = new Dsrcreate()
        //    {
        //        ExecutiveList = deserializedViewModel.ExecutiveList,
        //        CategoryList = deserializedViewModel.CategoryList,
        //        ProductList = updatedprodctList.ToList()
        //    };

        //    string update = JsonConvert.SerializeObject(updatemodel);
            
        //    _HttpContextAccessor.HttpContext.Session.SetString("DSR", update);
            
         
        //    string FromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");

        //    Dsrcreate ViewModel = JsonConvert.DeserializeObject<Dsrcreate>(FromSession);

        //    var categoryName = await _categoryservice.GetCategoryById(model.Category);
        //    string category = categoryName.CategoryName;
        //    string product = model.Product;
        //    List<DsrProduct> productlist = ViewModel.ProductList;

        //    List<DsrProduct> sear = new List<DsrProduct>();
        //    sear = productlist
        //    .Where(c =>
        //           (string.IsNullOrEmpty(category) || c.Category.StartsWith(category)) &&
        //           (string.IsNullOrEmpty(product) || c.ProductName.StartsWith(product, StringComparison.OrdinalIgnoreCase))
        //       )
        //       .Select(c => new DsrProduct
        //       {
        //           Id = c.Id,
        //           Category = c.Category,
        //           ProductName = c.ProductName,
        //           Price = c.Price,
        //           Quantity = c.Quantity,
        //           Weight = c.Weight

        //       })
        //       .ToList();

        //    return Json(sear);

        //}


        [HttpPost]
        public async Task<JsonResult> SearchProduct([FromBody] Dsrcreate model)
        {
           
                string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");
                Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);

                var updatemodel = await _dsrservice.UpdateSession(deserializedViewModel, model);


                string update = JsonConvert.SerializeObject(updatemodel);
                _HttpContextAccessor.HttpContext.Session.SetString("DSR", update);


                string FromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");
                Dsrcreate ViewModel = JsonConvert.DeserializeObject<Dsrcreate>(FromSession);

                var sear = await _dsrservice.SearchProductsAsync(ViewModel, model);
                return Json(sear);
            

        }


        [HttpPost]
        public async Task<ActionResult<Dsrcreate>> CreateDsr([FromBody] Dsrcreate model)
        {
            string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");
            Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);

            var updatemodel = await _dsrservice.UpdateSession(deserializedViewModel, model);

            string update = JsonConvert.SerializeObject(updatemodel);
            _HttpContextAccessor.HttpContext.Session.SetString("DSR", update);

            string FromSession =  _HttpContextAccessor.HttpContext.Session.GetString("DSR");
            Dsrcreate ViewModel = JsonConvert.DeserializeObject<Dsrcreate>(FromSession);
            await _dsrservice.Insert(ViewModel);

            return Ok(ViewModel);
        }


        //only product in service layer//
        //[HttpPost]
        //public async Task<ActionResult<Dsrcreate>> CreateDsr([FromBody] Dsrcreate model)
        //{
        //    string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");

        //    Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);

        //    var updatedprodctList = await _dsrservice.UpdateSession(deserializedViewModel.ProductList, model.ProductList);

        //    Dsrcreate updatemodel = new Dsrcreate()
        //    {
        //        Executive = model.Executive,
        //        Distributor = model.Distributor,
        //        Retailor = model.Retailor,
        //        ProductList = updatedprodctList
        //    };


        //    string update = JsonConvert.SerializeObject(updatemodel);

        //    _HttpContextAccessor.HttpContext.Session.SetString("DSR", update);


        //    string FromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");

        //    Dsrcreate ViewModel = JsonConvert.DeserializeObject<Dsrcreate>(FromSession);


        //    return Ok(ViewModel);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Dsrcreate>> CreateDsr([FromBody] Dsrcreate model)
        //{
        //    string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");

        //    Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);

        //    //var updatedprodctList = from s1 in deserializedViewModel.ProductList
        //    //                        join s2 in model.ProductList on s1.Id equals s2.Id into gj
        //    //                        from s2 in gj.DefaultIfEmpty()
        //    //                        select s2 != null ? s2 : s1;


        //    //Dsrcreate updatemodel = new Dsrcreate()
        //    //{
        //    //    Executive = model.Executive,
        //    //    Distributor = model.Distributor,
        //    //    Retailor = model.Retailor,
        //    //    ProductList = updatedprodctList.ToList()
        //    //};

        //    //var updatedprodctList = (from s1 in deserializedViewModel.ProductList
        //    //                        join s2 in model.ProductList on s1.Id equals s2.Id into gj
        //    //                        from s2 in gj.DefaultIfEmpty()
        //    //                        select s2 != null ? s2 : s1).ToList();

        //  var   updatedprodctList= await _dsrservice.UpdateSession(deserializedViewModel.ProductList, model.ProductList);

        //    Dsrcreate updatemodel = new Dsrcreate()
        //    {
        //        Executive = model.Executive,
        //        Distributor = model.Distributor,
        //        Retailor = model.Retailor,
        //        ProductList = updatedprodctList
        //    };

        //    string update = JsonConvert.SerializeObject(updatemodel);

        //    _HttpContextAccessor.HttpContext.Session.SetString("DSR", update);


        //    string FromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");

        //    Dsrcreate ViewModel = JsonConvert.DeserializeObject<Dsrcreate>(FromSession);

        //    //foreach (var stu in model.ProductList)
        //    //{
        //    //    var existingproduct = deserializedViewModel.ProductList.FirstOrDefault(s => s.Id == stu.Id);
        //    //    if (existingproduct != null)
        //    //    {
        //    //        _mapper.Map(stu, existingproduct);
        //    //    }
        //    //}
        //    //return View(deserializedViewModel);

        //    //var updatedprodctList = from s1 in deserializedViewModel.ProductList
        //    //                        join s2 in model.ProductList on s1.Id equals s2.Id into gj
        //    //                        from s2 in gj.DefaultIfEmpty()
        //    //                        select s2 != null ? s2 : s1;

        //    //var updatedStudentList1 = from s1 in deserializedViewModel
        //    //                         join s2 in model on s1.Id equals s2.Id into gj
        //    //                         from s2 in gj.DefaultIfEmpty()
        //    //                         select s2 != null ? s2 : s1;





        //    //var CREAT = _mapper.Map<Dsrcreate>(updatedprodctList.ToList());
        //    //CREAT.Distributor = model.Distributor;
        //    //CREAT.Executive = model.Executive;
        //    //CREAT.Retailor = model.Retailor;

        //    return Ok(ViewModel);
        //}

    }
}
