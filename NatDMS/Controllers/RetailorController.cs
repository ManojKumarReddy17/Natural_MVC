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
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly IAreaService _areaService;

        private readonly IMapper _mapper;
        public RetailorController(IRetailorService retailorService, IMapper mapper, ILocationService locationService, IStateService stateService, ICityService cityService, IAreaService areaService)
        {
            _retailorservice = retailorService;
            _stateService = stateService;
            _cityService = cityService;
            _areaService = areaService;
            _locationservice = locationService;

            _mapper = mapper;
        }


        public async Task<ActionResult<RetailorModel>> DisplayRetailors()
        {
            var result = await _retailorservice.GetRetailors();
            var mapped = _mapper.Map<List<RetailorModel>, List<RetailorViewModel>>(result);

            return View(mapped);
        }

        [HttpGet]
        public async Task<ActionResult<RetailorModel>> DisplayRetailors(string searchTerm, string selectedState, string selectedCity, string selectedArea)
        {

            var retailors = await _retailorservice.GetRetailors();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                retailors = retailors.Where(r => r.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(selectedState))
            {
                retailors = retailors.Where(r => r.State.Equals(selectedState, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(selectedCity))
            {
                retailors = retailors.Where(r => r.City.Equals(selectedCity, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(selectedArea))
            {
                retailors = retailors.Where(r => r.Area.Equals(selectedArea, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(retailors);
        }

        public async Task<ActionResult> cityData(string Id)
        {
            var result = await _cityService.GetCity(Id);
            return Json(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RetailorViewModel>> GetRetailorById(string id)
        {
            var retailor = await _retailorservice.GetRetailorById(id);
            var mapretailor = _mapper.Map<List<RetailorViewModel>>(retailor);
            return Ok(mapretailor);
        }

        public async Task<JsonResult> GetArea(string Id)
        {
            var result = await _cityService.GetCity(Id);

            return Json(result);
        }

        public async Task<ActionResult> CreateRetailors()
        {
            var result = await _stateService.GetState();
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
