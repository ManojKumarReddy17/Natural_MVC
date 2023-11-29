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

        public RetailorController(IRetailorService retailorService, IMapper mapper, ILocationService locationService)
        {
            _retailorService = retailorService;
            _mapper = mapper;
            _locationservice = locationService;
        }
        [HttpGet]
        public async Task<ActionResult<RetailorModel>> DisplayRetailors(string searchTerm)
        {
            var retailors = await _retailorService.GetRetailors();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                retailors = retailors.Where(r => r.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            }

            

            var mapretailor = _mapper.Map<List<RetailorViewModel>>(retailors);
            return View(mapretailor);
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
