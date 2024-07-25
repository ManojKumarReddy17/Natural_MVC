using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;
using System.Reflection;

namespace NatDMS.Controllers
{
    public class DistributorLoginReportsController : Controller
    {
        private readonly IExecutiveService _ExecutiveService;
        private readonly IDistributorLoginReportsService _distributorLoginReportsService;
        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;
        private readonly IDSRService _dSRService;
        public DistributorLoginReportsController(IAreaService areaService,IDistributorLoginReportsService distributorLoginReportsService, IMapper mapper, IDSRService dSRService, IExecutiveService ExecutiveService)
        {
            _distributorLoginReportsService = distributorLoginReportsService;
            _mapper = mapper;
            _dSRService = dSRService;
            _areaService = areaService;
            _ExecutiveService = ExecutiveService;
        }

        [HttpPost]
    
        public async Task<ActionResult> DisplayDsreport([FromBody] DistributorLoginSample Search)
        {
            

            var viewmo = await _distributorLoginReportsService.SearchDSR(Search);
            if (viewmo == null)
            {
                return Json(new { error = "No data found" });
            }
            return Json(viewmo);
        }
        

        public async Task<ActionResult> GetDistributorReport()
        {
            DistributorLoginModel viewModel;
            
            if (TempData["DistributorLoginModel"] != null)
            {
                viewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DistributorLoginModel>(TempData["DistributorLoginModel"].ToString());

                if (viewModel.Id != null)
                {
                    var result = await _dSRService.GetAssignedRetailorDetailsByDistributorIds(viewModel.Id);
                    ViewBag.RetailorList = result;
                }
                if (viewModel.report == null)
                {
                    viewModel.Distributor = viewModel.Id;

                    var create = _mapper.Map<DistributorLoginModel, DistributorLoginSample>(viewModel);
                    var result = await _distributorLoginReportsService.SearchDSR(create);
                    viewModel.report = result;
                }
                if (viewModel.exeId != null)
                {
                    var areaDetail = await _ExecutiveService.GetAreaByExecutive(viewModel.exeId);
                    viewModel.Areas = areaDetail; // Set AreaName in the view model
                    //ViewBag.AreaList = areaDetail;
                }

                return View(viewModel); // Pass the model to the view
            }
            else
            {
                // Fetch the default report if TempData is null
                viewModel = await _distributorLoginReportsService.GetDsreport();
                return View(viewModel); // Handle the case where TempData is null
            }
        }
    

        public async Task<JsonResult> GetRetailorByDistributorId(string distributorId)
        {
            var result = await _dSRService.GetAssignedRetailorDetailsByDistributorId(distributorId);
            return Json(result);
        }

       
    }
}
