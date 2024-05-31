using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<DistributorSalesReport>> GetDailySaleReport()
        {

            var reportResult = await _distributorSalesService.GetDsreport();


            if (reportResult != null)
            {

                var statesResult = await _unifiedService.GetStates();


                var defaultState = "Karnataka";
                var defaultCity = "Bengaluru";

                var viewModel = new DistributorSalesReport
                {
                    StateList = statesResult,
                    State = defaultState,
                    City = defaultCity,
                    //  Retailorlist = reportResult
                };

                return View(viewModel);
            }
            else
            {

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
        public async Task<IActionResult> GetretailorByArea(string areaId)
        {
            var result = await _distributorSalesService.GetAssignedRetailorByArea(areaId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> DisplayDsreport([FromBody] DistributorSalesReport Search)
        {
            var viewmo = await _distributorSalesService.SearchDSR(Search);
            return Json(viewmo);
        }
    }
}
