using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;

namespace NatDMS.Controllers
{
    public class RetailorController : Controller
    {

        private readonly IRetailorService _retailorservice;
        private readonly ILocationService _locationservice;
        private readonly IStateService _IStateService;
        private readonly ICityService _ICityService;
        private readonly IAreaService _IAreaService;

        private readonly IMapper _mapper;
        public RetailorController(IRetailorService retailorservice, IMapper mapper, ILocationService locationService, IStateService IStateService, ICityService ICityService, IAreaService IAreaService)

        {
            _retailorservice = retailorservice;
            _IStateService = IStateService;
            _ICityService = ICityService;
            _IAreaService = IAreaService;
            _locationservice = locationService;
            _mapper = mapper;
        }


        public async Task<ActionResult<RetailorModel>> DisplayRetailors()
        {
            var result = await _retailorservice.GetRetailors();
            var mapped = _mapper.Map<List<RetailorModel>, List<RetailorViewModel>>(result);

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



        public async Task<ActionResult> CreateRetailors()
        {
            var result = await _IStateService.GetState();
            var distributo = _mapper.Map<List<StateModel>, List<StateViewModel>>(result);
            ViewBag.State = distributo;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRetailors(SaveRetailorViewModel retailor)
        {
            if (ModelState.IsValid)
            {
                var createretailor = _mapper.Map<SaveRetailorViewModel, RetailorModel>(retailor);

                createretailor.City = Request.Form["CityId"];
                createretailor.State = Request.Form["StateId"];
                createretailor.Area = Request.Form["AreaId"];

                var displayretailor = await _retailorservice.CreateRetailors(createretailor);

                return RedirectToAction("DisplayRetailors", "Retailor");
            }

            else
            {
                return View(retailor);

            }



        }
    }
}
  

