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
        private readonly IMapper _mapper;
        private readonly ILocationService _locationservice;
        private readonly IRetailorService _retailorService;
        private readonly IDistributorService _distributorService;

        public RetailorController(IRetailorService retailorService, IMapper mapper, ILocationService locationService, IDistributorService distributorService)
        {
            _retailorService = retailorService;
            _mapper = mapper;
            _locationservice = locationService;
            _distributorService = distributorService;
        }

        [HttpGet]
        public async Task<ActionResult<RetailorModel>> DisplayRetailors(string searchTerm, string selectedState,string selectedCity,string selectedArea)
        {
            
            var retailors = await _retailorService.GetRetailors();

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

            var mapretailor = _mapper.Map<List<RetailorViewModel>>(retailors);
            return View(mapretailor);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RetailorViewModel>> GetRetailorById(string id)
        {
            var retailor = await _retailorService.GetRetailorById(id);
            var mapretailor = _mapper.Map<List<RetailorViewModel>>(retailor);
            return Ok(mapretailor);
        }


        public async Task<ActionResult> CreateRetailors()
        {
            var states = await _locationservice.GetStates();
            var cities = await _locationservice.GetCities();
            var areas = await _locationservice.GetAreas();

            ViewBag.States = new SelectList(states, "Id", "StateName");
            ViewBag.Cities = new SelectList(cities, "Id", "CityName");
            ViewBag.Areas = new SelectList(areas, "Id", "AreaName");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRetailors(RetailorViewModel retailorViewModel)
        {
            if (ModelState.IsValid)
            {
                var retailors = _mapper.Map<RetailorViewModel, RetailorModel>(retailorViewModel);
                retailors.State = Request.Form["StateId"];
                retailors.City = Request.Form["CityId"];
                retailors.Area = Request.Form["AreaId"];

                var createretailor = await _retailorService.CreateRetailors(retailors);
                return Ok("INSERTED SUCCESSFULLY");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest("Validation failed. Errors: " + string.Join(", ", errors));
            }
        }
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
       


    }
}
