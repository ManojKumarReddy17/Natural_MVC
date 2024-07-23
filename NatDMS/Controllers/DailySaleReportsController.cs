using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;
using System.Threading.Tasks;

namespace NatDMS.Controllers
{
    public class DailySaleReportsController : Controller
    {
        private readonly IDistributorSalesService _distributorSalesService;
        private readonly IDSRService _dsrservice;
        private readonly IMapper _mapper;
        private readonly IUnifiedService _unifiedService;

        public DailySaleReportsController(IDistributorSalesService distributorSalesService, IMapper mapper, IDSRService DSRService, IUnifiedService unifiedService)
        {
            _distributorSalesService = distributorSalesService;
            _mapper = mapper;
            _unifiedService = unifiedService;
            _dsrservice = DSRService;
        }

        [HttpGet]
        public async Task<ActionResult<DistributorSalesReport>> GetDailySaleReport(DistributorSalesReport Search)
        {



            try
            {
                var reportResult = await _distributorSalesService.GetDsreport();
                var viewmo = await _distributorSalesService.SearchDSR(Search);
                var statesResult = await _unifiedService.GetStates();

                var CityList = await _unifiedService.GetCities();

               

                var viewModel = new DistributorSalesReport
                {
                    StateList = statesResult,
                    CityList = CityList,
                    report = viewmo


                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                return RedirectToAction("Error", "Home");

            }
        }
            
        


        public async Task<JsonResult> GetAreasByCityId(string cityId)
        {
            var result = await _unifiedService.GetAreasByCityId(cityId);
            return Json(result);
        }

        public async Task<IActionResult> GetCitiesbyStateId(string stateId)
        {
            var result = await _unifiedService.GetCitiesbyStateId(stateId);
            return Json(result);
        }
        [HttpGet]
        public async Task<JsonResult> GetAExecutiveByArea(string areaId)
        {
            var result = await _distributorSalesService.GetExecutiveByArea(areaId);
            return Json(result);
        }

        //[HttpGet]
        //public async Task<JsonResult> GetDistributorByExecutiveId(string executiveId)
        //{
        //    var result = await _distributorSalesService.GetDistributorDetailsByExecutiveId(executiveId);
        //    return Json(result);
        //}
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

        [HttpPost]
        public async Task<ActionResult> DisplayDsreport([FromBody] DistributorSalesReport Search)
        {
            var viewmo = await _distributorSalesService.SearchDSR(Search);


            

            return Json(viewmo);
        }

    }
}
