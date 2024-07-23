using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;

namespace NatDMS.Controllers
{
    public class CityController : Controller
    {
        private IUnifiedService _unifiedService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private object _unifiedservice;

        public CityController(ICityService cityService, IMapper mapper, IConfiguration configuration, IUnifiedService unifiedService)
        {
            _cityService = cityService;
            _mapper = mapper;
            _configuration = configuration;
            _unifiedService = unifiedService;
        }



        [HttpGet]

        public async Task<ActionResult<CityViewModel>> DisplayCity(int page)
        {

            var resultcity = await _cityService.GetCity();
            var viewmodel = _mapper.Map<List<CityModel>, List<CityViewModel>>(resultcity);
            var cityPgn = new PageNation<CityViewModel>(viewmodel, _configuration, page);
            var paginatedData = cityPgn.GetPaginatedData(viewmodel);
            ViewBag.Pages = cityPgn;
            var statesResult = await _unifiedService.GetStates();
            var viewModel = new CityViewModel
            {

                StateList = statesResult
            };
            return View(viewModel);

        }
        
        public async Task<ActionResult> CreateCity()
        {
            var viewModel = new CityViewModel
            {
                StateList = await _unifiedService.GetStates()
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult<CityViewModel>> CreateCity(CityViewModel cityModel)
        {

            var result = _mapper.Map<CityViewModel, CityModel>(cityModel);
            var createArea = await _cityService.CreateCity(result);
            return RedirectToAction("DisplayCity", "City");
        }

        [HttpGet]
        public async Task<ActionResult<CityViewModel>> EditCity(string Id)
        {
            var cityModel = await _cityService.GetCityById(Id);
            var CityViewModel = _mapper.Map<CityModel, CityViewModel>(cityModel);
            return View(CityViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditCity(string Id, CityViewModel CityViewModel)

        {
            if (ModelState.IsValid)
            {
                var value = await _cityService.GetCity();
                var stateid = value.Where(x => x.Id == CityViewModel.Id).Select(x => x.StateId).FirstOrDefault();
                CityViewModel.StateId = stateid;
                var cityModel = _mapper.Map<CityViewModel, CityModel>(CityViewModel);
                var updatedCity = await _cityService.UpdateCity(Id, cityModel);
                return RedirectToAction("DisplayCity", "City", new { id = updatedCity.StateId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Entered invalid credentials. Please re-enter the credentials.");
                return View(CityViewModel);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCity(string id)
        {
            await _cityService.DeleteCity(id);
            return Json(new { success = true, message = "City deleted successfully." });
        }



        public async Task<IActionResult> GetCitiesbyStateId(string stateId)
        {
            var result = await _unifiedService.GetCitiesbyStateId(stateId);
            return Json(result);
        }

        /// <summary>
        /// GETTING AREA'S LIST FOR DROPDOWN BASED ON CITY_ID
        /// </summary>
        public async Task<JsonResult> GetAreasByCityId(string cityId)
        {
            var result = await _unifiedService.GetAreasByCityId(cityId);

            return Json(result);
        }


        public async Task<IActionResult> Create()
        {

            List<StateModel> viewModel = await _unifiedService.GetStates();

            return View(viewModel);
        }


    }
}

