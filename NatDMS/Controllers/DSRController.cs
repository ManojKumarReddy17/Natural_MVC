using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
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


       

    }
}
