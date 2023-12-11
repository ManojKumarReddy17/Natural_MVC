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

        private readonly IRetailorService _retailorService;
        private readonly IDistributorService _distributorService;

        public RetailorController(IRetailorService retailorService, IMapper mapper, ILocationService locationService, IDistributorService distributorService)
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


        public async Task<IActionResult> cityData(string Id)
        {
            var result = await _ICityService.GetCity(Id);
            return Json(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RetailorViewModel>> GetRetailorById(string id)
        {
            var retailor = await _retailorService.GetRetailorById(id);
            var mapretailor = _mapper.Map<List<RetailorViewModel>>(retailor);
            return Ok(mapretailor);
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
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
       





        }
    }
}
  

