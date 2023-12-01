using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using PagedList.Mvc;
using PagedList;
using Naturals.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace NatDMS.Controllers
{
    public class DistributorController : Controller
    {

        private readonly IDistributorService _distributorservice;
        private readonly ILocationService _locationservice;
        private readonly IStateService _IStateService;
        private readonly ICityService _ICityService;
        private readonly IAreaService _IAreaService;

        private readonly IMapper _mapper;
        public DistributorController(IDistributorService distributorservice, IMapper mapper, ILocationService locationService, IStateService IStateService, ICityService ICityService, IAreaService IAreaService)

        {
            _distributorservice = distributorservice;
            _IStateService = IStateService;
            _ICityService = ICityService;
            _IAreaService = IAreaService;
            _locationservice = locationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DistributorModel>> DisplayDistributors()
        {
            var result = await _distributorservice.GetDistributors();
            var mapped = _mapper.Map<List<DistributorModel>,List<DistributorViewModel>>(result);

            return View(mapped);
        }   

        public async Task<IActionResult> cityData(string Id)
        {
            var result = await _ICityService.GetCity(Id);
            return Json(result);
        }

        public async Task<JsonResult> GetArea(string Id)
        {
            var result = await _IAreaService.GetArea(Id);

            return Json(result);
        }

     
        public async Task<ActionResult> CreateDistributor()
        {
            var result = await _IStateService.GetState();
            var distributo = _mapper.Map<List<StateModel>, List<StateViewModel>>(result);
            ViewBag.State = distributo;
            return View();
        }
      

        [HttpPost]
        public async Task<IActionResult> CreateDistributor(SaveDistributorViewModel distributorModel)
        {

            if (ModelState.IsValid)
            {
                var distributor = _mapper.Map<SaveDistributorViewModel, DistributorModel>(distributorModel);

                // Converting city,state and area names into an id's//

                distributor.City = Request.Form["CityId"];
                distributor.State = Request.Form["StateId"];
                distributor.Area = Request.Form["AreaId"];

                var Createldistributor = await _distributorservice.CreateDistributor(distributor);

                return RedirectToAction("DisplayDistributors", "Distributor");
            }

            else
            {
              
                return View(distributorModel);
            }
        }
    }
}