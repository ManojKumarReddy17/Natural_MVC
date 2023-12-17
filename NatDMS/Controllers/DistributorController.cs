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
        private readonly IUnifiedService _unifiedservice;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public DistributorController(IDistributorService distributorservice, IMapper mapper, IUnifiedService unifiedservice, IConfiguration configuration)

        {
            _distributorservice = distributorservice;
            _unifiedservice = unifiedservice;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// DISPLAYING LIST OF ALL DISTRIBUTORS 
        /// </summary>
<<<<<<< Updated upstream
=======
        
        [HttpGet]
        public async Task<ActionResult<EDR_DisplayViewModel>> DisplayDistributors()             
        {
            var distributorlist = new List<DistributorModel>();
>>>>>>> Stashed changes

        [HttpGet]

        public async Task<ActionResult<List<DistributorModel>>> DisplayDistributors(int page = 1)
        {
            var distributorResult = await _distributorservice.GetAllDistributors();
            var distributorPgn = new PageNation<DistributorModel>(distributorResult, _configuration, page);

            var paginatedData = distributorPgn.GetPaginatedData(distributorResult);

            var mapped = _mapper.Map<List<DistributorModel>, List<DisplayViewModel>>(paginatedData);

            ViewBag.Pages = distributorPgn;

            var statesResult = await _unifiedservice.GetStates();

            var viewModel = new EDR_DisplayViewModel
            {
                DistributorList = paginatedData,
                StateList = statesResult
            };

            return View(viewModel);

        }



        /// <summary>
        /// GETTING DISTRIBUTOR DETAILS BY ID
        /// </summary>
        /// 

        [HttpGet]
        public async Task<ActionResult> DistributorDetailsBYId(string id)
        {
            var distribtuors = await _distributorservice.GetDistributorDetailsById(id);
            var mapped = _mapper.Map<DistributorViewModel>(distribtuors);
            return View(mapped);

        }
        /// <summary>
        /// GETTING CITIES LIST FOR DROPDOWN BASED ON STATE_ID
        /// </summary> 
        public async Task<ActionResult> GetCitiesbyStateId(string stateId)
        {
            var result = await _unifiedservice.GetCitiesbyStateId(stateId);
            return Json(result);
        }

        /// <summary>
        /// GETTING AREA'S LIST FOR DROPDOWN BASED ON CITY_ID
        /// </summary>
        public async Task<JsonResult> GetAreasByCityId(string cityId)
        {
            var result = await _unifiedservice.GetAreasByCityId(cityId);

            return Json(result);
        }

        /// <summary>
        /// CREATING NEW DISTRIBUTOR
        /// </summary>
        public async Task<ActionResult> CreateDistributor()
        {
            var viewModel = new ED_CreateViewModel
            {
                States = await _unifiedservice.GetStates()
            };

            return View(viewModel);
        }
        /// <summary>
        /// INSERTING CREATED DISTRIBUTOR DATA
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateDistributor(ED_CreateViewModel distributorModel)
        {
            if (ModelState.IsValid)
            {
                var distributor = _mapper.Map<ED_CreateViewModel, DistributorModel>(distributorModel);

                 await _distributorservice.CreateDistributor(distributor);

                return RedirectToAction("DisplayDistributors", "Distributor");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Form submission failed. Please check the provided data");
                return View(distributorModel);
            }

        }


        /// <summary>
        /// GETTING EXISTING DATA FOR UPDATE
        /// </summary>

        public async Task<ActionResult<ED_EditViewModel>> EditDistributor(string id)
        {
            var distributor = await _distributorservice.GetDistributorById(id);

            var statesResult = await _unifiedservice.GetStates();

            var citiesResult = await _unifiedservice.GetCitiesbyStateId(distributor.State);

            var AreaResult = await _unifiedservice.GetAreasByCityId(distributor.City);
            var model = new ED_EditViewModel
            {

                FirstName = distributor.FirstName,
                LastName = distributor.LastName,
                Email = distributor.Email,
                MobileNumber = distributor.MobileNumber,
                Address = distributor.Address,
                UserName = distributor.UserName,
                Password = distributor.Password,
                StateList = statesResult.Select(state => new SelectListItem
                {
                    Text = state.StateName,
                    Value = state.Id
                }).AsEnumerable(),
                CityList = citiesResult.Select(city => new SelectListItem
                {
                    Text = city.CityName,
                    Value = city.Id
                }).AsEnumerable(),

                AreaList = AreaResult.Select(area => new SelectListItem
                {
                    Text = area.AreaName,
                    Value = area.Id
                }).AsEnumerable()
            };
            model.State = distributor.State;
            model.City = distributor.City;
            model.Area = distributor.Area;
            return View(model);
        }

        /// <summary>
        /// POSTING UPDATED DISTRIBUTOR DATA
        /// </summary>
        /// 

        [HttpPost]
        public async Task<ActionResult<ED_EditViewModel>> EditDistributor(string id, ED_EditViewModel Editviewmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var update = _mapper.Map<ED_EditViewModel, DistributorModel>(Editviewmodel);

                    await _distributorservice.UpdateDistributor(id, update);

                    return RedirectToAction(nameof(DisplayDistributors));
                }
                else
                {
                    return View(Editviewmodel);
                }
            }
            catch
            {
                return View();
            }
        }


        /// <summary>
        /// DELETING DISTRIBUTOR BY ID
        /// </summary>


        public async Task<IActionResult> DeleteDistributor(string DistributorId)
        {

            await _distributorservice.DeleteDistributor(DistributorId);
            return RedirectToAction("DisplayDistributors", "Distributor");
        }

        [HttpPost]
        public async Task<ActionResult<EDR_DisplayViewModel>> SearchDistributor(EDR_DisplayViewModel model)
        {
            var search = _mapper.Map<EDR_DisplayViewModel, SearchModel>(model);
            var SearchResult = await _distributorservice.SearchDistributor(search);
            var statesResult = await _unifiedservice.GetStates();

            var viewModel = new EDR_DisplayViewModel
            {
                DistributorList = SearchResult,
                StateList = statesResult,
            };

            return View("DisplayDistributors", viewModel);
        }
    }
}
