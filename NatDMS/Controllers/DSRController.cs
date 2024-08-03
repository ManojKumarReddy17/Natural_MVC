using System.Collections;
using System.Security.Cryptography;
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
        private readonly IDistributorSalesService _distributorSalesService;
        private readonly IAreaService _areaService;
        private readonly IDSRService _dsrservice;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IUnifiedService _unifiedService;
        public DSRController(IDSRService dsrservice,IMapper mapper, IHttpContextAccessor HttpContextAccessor,IAreaService areaService, IDistributorSalesService distributorSalesService, IUnifiedService unifiedService)
        {
            _dsrservice = dsrservice;
            _mapper = mapper;
            _HttpContextAccessor = HttpContextAccessor;
            _areaService = areaService; 
            _distributorSalesService = distributorSalesService;
            _unifiedService = unifiedService;
        }

        public async Task<ActionResult<List<DSRModel>>> DisplayDsrs()
        {
            var dsrResult = await _dsrservice.GetDsrAll();
            var executivelist = dsrResult.ExecutiveList;
            var statesResult = await _unifiedService.GetStates();

            var CityLists = await _unifiedService.GetCities();
            var dsrlist = dsrResult.dsr;
            var  newexecutivelist  =   _mapper.Map<List<DsrExecutiveDrop>, List<DsrExecutiveResourse>>(executivelist);
            var newdsrlidt = _mapper.Map<List<DSRModel>, List<DsrResourse>>(dsrlist);
            
            DSRViewModel viewmodel = new DSRViewModel
            {
                StateList = statesResult,
                CityList = CityLists,
                ExecutiveList = newexecutivelist,
                dsr = newdsrlidt
                

            };
            
            return View(viewmodel);
        }

        [HttpGet]
        public async Task<ActionResult<Dsrcreate>> CreateDsr()
        {

            var viewmodel = await _dsrservice.CreateDsr();
            //var resultarea = await _areaService.GetAreas(0);

            //var lst = _mapper.Map<List<AreaModel>, List<AreaCUmodel>>(resultarea.Items);
            //ViewBag.AreaList = lst;

            string dsrjson = JsonConvert.SerializeObject(viewmodel);
            _HttpContextAccessor.HttpContext.Session.SetString("DSR", dsrjson);

            string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");

            Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);
          

            return View(deserializedViewModel);
        }

        public async Task<JsonResult> GetAreasByCityId(string cityId)
        {
            var result = await _unifiedService.GetAreasByCityId(cityId);
            return Json(result);
        }
        [HttpGet]
        public async Task<JsonResult> GetCitiesbyStateId(string stateId)
        {
            var result = await _unifiedService.GetCitiesbyStateId(stateId);
            //ViewBag.StateList = result;
            return Json(result);
        }

        public async Task<JsonResult> GetExecutiveByArea(string areaId)
        {
            var result = await _distributorSalesService.GetExecutiveByArea(areaId);
            return Json(result);
        }
    
        [HttpGet]
        public async Task<JsonResult> GetDistributorByExecutiveId(string executiveId)
        {
            var result = await _dsrservice.AssignedDistributorDetailsByExecutiveId(executiveId);

            return Json(result);
        }
        [HttpGet]
        public async Task<JsonResult> GetRetailorByDistributorId(string distributorId)
        {
            var result = await _dsrservice.GetAssignedRetailorDetailsByDistributorIds(distributorId);
            return Json(result);
        }
        public async Task<ActionResult<DsrInsert>> Details(string dsrid)
        {
            if (dsrid != "null")
            {
                var dsr = await _dsrservice.Details(dsrid);
                dsr.dsrid = dsrid;
                return View(dsr);
            }
            else {
                return RedirectToAction("DisplayDsrs", "DSR");
            }

        }


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
        public async Task<JsonResult> DsrSearchProduct([FromBody] DSRViewModel model)
        {

            var searh = _mapper.Map<DSRViewModel, Dsrview>(model);

            var se = await _dsrservice.dsrsearch(searh);
            return Json(se);

        }
        


        //[HttpPost]
        //public async Task<JsonResult> CreateDsr([FromBody] Dsrcreate model)
        //{
        //    string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");
        //    Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);

        //    var updatemodel = await _dsrservice.UpdateSession(deserializedViewModel, model);

        //    string update = JsonConvert.SerializeObject(updatemodel);
        //    _HttpContextAccessor.HttpContext.Session.SetString("DSR", update);

        //    string FromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");
        //    Dsrcreate ViewModel = JsonConvert.DeserializeObject<Dsrcreate>(FromSession);


        //    var insertdata = await _dsrservice.onlyUpdateaInsert(ViewModel);
        //    var result = await _dsrservice.Insert(insertdata);

           

        //    return Json(result);
        //}
        [HttpPost]

        public async Task<ActionResult> CreateDsr([FromBody] Dsrcreate model)
        {
            if(ModelState.IsValid)
            {
                string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");
                Dsrcreate deserializedViewModel = !string.IsNullOrEmpty(dsrjsonFromSession)
                    ? JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession)
                    : new Dsrcreate();
                deserializedViewModel.Area = model.Area;
                deserializedViewModel.AreaList = model.AreaList;
                Dsrcreate updatedModel = await _dsrservice.UpdateSession(deserializedViewModel, model);
                string updatedJson = JsonConvert.SerializeObject(updatedModel);
                _HttpContextAccessor.HttpContext.Session.SetString("DSR", updatedJson);

                var insertData = await _dsrservice.onlyUpdateaInsert(updatedModel);
                var result = await _dsrservice.Insert(insertData);


                return Json(result);
            }
            else
            {
                // return Json(new { error = "An error occurred: " + ex.Message });
                return RedirectToAction("DisplayDsrs", "DSR");
            }
        }



        public async Task<ActionResult> Deletedsr(string dsrId)
        {
            await _dsrservice.DeleteDsr(dsrId);
            return RedirectToAction("DisplayDsrs", "DSR");
        }


        public async Task<ActionResult<Dsrcreate>> Edit(string dsrid)

        {
          var viewmodel =   await _dsrservice.editDsr(dsrid);
            viewmodel.dsrid = dsrid; // added to send dsrid while calling api
            var resultarea = await _areaService.GetAreas(0);

            var lst = _mapper.Map<List<AreaModel>, List<AreaCUmodel>>(resultarea.Items);
            ViewBag.AreaList = lst;

            string dsrjson = JsonConvert.SerializeObject(viewmodel);
            _HttpContextAccessor.HttpContext.Session.SetString("DSREdit", dsrjson);

            string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSREdit");

            Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);


            return View(deserializedViewModel);

        }

        [HttpPost]
        public async Task<JsonResult> EditSearchProduct([FromBody] Dsrcreate model)
        {

            string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSREdit");
            Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);

            var updatemodel = await _dsrservice.UpdateSession(deserializedViewModel, model);


            string update = JsonConvert.SerializeObject(updatemodel);
            _HttpContextAccessor.HttpContext.Session.SetString("DSREdit", update);


            string FromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSREdit");
            Dsrcreate ViewModel = JsonConvert.DeserializeObject<Dsrcreate>(FromSession);

            var sear = await _dsrservice.SearchProductsAsync(ViewModel, model);
            return Json(sear);

        }

        [HttpPost]
        public async Task<JsonResult> EditDsr([FromBody] Dsrcreate model)
        {
            string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSREdit");
            Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);
            deserializedViewModel.CreatedDate = model.CreatedDate;
            var updatemodel = await _dsrservice.UpdateSession(deserializedViewModel, model);

            string update = JsonConvert.SerializeObject(updatemodel);
            _HttpContextAccessor.HttpContext.Session.SetString("DSREdit", update);

            string FromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSREdit");
            Dsrcreate ViewModel = JsonConvert.DeserializeObject<Dsrcreate>(FromSession);



            var updatedata = await _dsrservice.onlyUpdateaInsert(ViewModel);
            var result = await _dsrservice.Updatedsr(updatedata);

            
            return Json(result);
        }

    }
}
