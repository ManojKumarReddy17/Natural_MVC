using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Natural.Core.IServices;
using Natural.Core.Models;

namespace NatDMS.Controllers
{
    public class DailySaleReportsController : Controller
    {
        private readonly IDistributorSalesService _distributorSalesService;
        private readonly IDSRService _dsrservice;
        private readonly IMapper _mapper;
        public DailySaleReportsController(IDistributorSalesService distributorSalesService, IMapper mapper, IDSRService DSRService)
        {
            _distributorSalesService = distributorSalesService;
            _mapper = mapper;

            _dsrservice = DSRService;


        }
        [HttpGet]
        public async Task<ActionResult<DistributorSalesReport>> GetDailySaleReport()
        {
            var viewmodel = await _distributorSalesService.GetDsreport();
            return View(viewmodel);
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
        public async Task<ActionResult> DisplayDsreport([FromBody] DistributorSalesReport Search)
        {
            var viewmo = await _distributorSalesService.SearchDSR(Search);



            //var viewmodel = await _distributorSalesService.GetDsreport();
            //viewmodel.report = viewmo;


            return Json(viewmo);
        }
    }
}
