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
            var executivelist = dsrResult.ExecutiveList;
            var dsrlist = dsrResult.dsr;
            var  newexecutivelist  =   _mapper.Map<List<DsrExecutiveDrop>, List<DsrExecutiveResourse>>(executivelist);
            var newdsrlidt = _mapper.Map<List<DSRModel>, List<DsrResourse>>(dsrlist);

            DSRViewModel viewmodel = new DSRViewModel
            {
                ExecutiveList = newexecutivelist,
                dsr = newdsrlidt

            };
            
            return View(viewmodel);
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
            
            return Json(result);
        }


        public async Task<JsonResult> GetRetailorByDistributorId(string distributorId)
        {
            var result = await _dsrservice.GetAssignedRetailorDetailsByDistributorId(distributorId);
           
            return Json(result);
        }

        public async Task<ActionResult<DsrInsert>> Details(string dsrid)
        {
           var dsr = await _dsrservice.Details(dsrid);
            return View(dsr);

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
        

    

       

        


        [HttpPost]
        public async Task<JsonResult> CreateDsr([FromBody] Dsrcreate model)
        {
            string dsrjsonFromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");
            Dsrcreate deserializedViewModel = JsonConvert.DeserializeObject<Dsrcreate>(dsrjsonFromSession);

            var updatemodel = await _dsrservice.UpdateSession(deserializedViewModel, model);

            string update = JsonConvert.SerializeObject(updatemodel);
            _HttpContextAccessor.HttpContext.Session.SetString("DSR", update);

            string FromSession = _HttpContextAccessor.HttpContext.Session.GetString("DSR");
            Dsrcreate ViewModel = JsonConvert.DeserializeObject<Dsrcreate>(FromSession);


            var insertdata = await _dsrservice.onlyUpdateaInsert(ViewModel);
            var result = await _dsrservice.Insert(insertdata);

           

            return Json(result);
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
