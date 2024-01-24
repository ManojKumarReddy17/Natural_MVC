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
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NatDMS.Controllers
{
    public class DistributorController : Controller
    {

        private readonly IDistributorService _distributorservice;
        private readonly IUnifiedService _unifiedservice;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public DistributorController(IDistributorService distributorservice, IMapper mapper, IUnifiedService unifiedservice, IConfiguration configuration,IRetailorService retailorService)

        {
            _distributorservice = distributorservice;
            _unifiedservice = unifiedservice;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// DISPLAYING LIST OF ALL DISTRIBUTORS 
        /// </summary>


        [HttpGet]
        public async Task<ActionResult<List<DistributorModel>>> DisplayDistributors(int page = 1)
        {
            var distributorResult = await _distributorservice.GetAllDistributors();
            var distributorPgn = new PageNation<DistributorModel>(distributorResult, _configuration, page);

            var paginatedData = distributorPgn.GetPaginatedData(distributorResult);
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

        public async Task<ActionResult> EditDistributor(string id)
        {
            var distributor = await _distributorservice.GetDistributorById(id);
            var viewModel = _mapper.Map<ED_EditViewModel>(distributor);
            viewModel.StateList = await _unifiedservice.GetStates();
            viewModel.CityList = await _unifiedservice.GetCitiesbyStateId(distributor.State);
            viewModel.AreaList = await _unifiedservice.GetAreasByCityId(distributor.City);
            return View(viewModel);
        }


        /// <summary>
        /// POSTING UPDATED DISTRIBUTOR DATA
        /// </summary>



        [HttpPost]
        public async Task<ActionResult> EditDistributor(string id, ED_EditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var update = _mapper.Map<ED_EditViewModel, DistributorModel>(viewModel);
                await _distributorservice.UpdateDistributor(id, update);
                return RedirectToAction(nameof(DisplayDistributors));
            }
            else
            {
                viewModel.StateList = await _unifiedservice.GetStates();
                viewModel.CityList = await _unifiedservice.GetCitiesbyStateId(viewModel.State);
                viewModel.AreaList = await _unifiedservice.GetAreasByCityId(viewModel.City);
                return View(viewModel);
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

        /// <summary>
        /// SEARCH DISTRIBUTOR PARTIAL VIEW
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<EDR_DisplayViewModel>> SearchDistributor(EDR_DisplayViewModel SearchResultmodel)
        {
            var search = _mapper.Map<EDR_DisplayViewModel, SearchModel>(SearchResultmodel);
            var SearchResult = await _distributorservice.SearchDistributor(search);
            var statesResult = await _unifiedservice.GetStates();

            var viewModel = new EDR_DisplayViewModel
            {
                DistributorList = SearchResult,
                StateList = statesResult, 
            };

            return PartialView("_SearchDistributorPartial", viewModel);
        }

        [HttpGet]
        public async Task<ActionResult<List<RetailorModel>>> ListOfRetailors(int page = 1)
        {
            var retailorResult = await _distributorservice.GetNonAssignedRetailors();
            var retailorPgn = new PageNation<RetailorModel>(retailorResult, _configuration, page);

            var paginatedData = retailorPgn.GetPaginatedData(retailorResult);


            ViewBag.Pages = retailorPgn;

            var statesResult = await _unifiedservice.GetStates();

            var viewModel = new EDR_DisplayViewModel
            {
                RetailorList = paginatedData,
                StateList = statesResult
            };

            return View("_ListOfRetailors",viewModel);
        }
       
        [HttpPost]
        public async Task<JsonResult>SearchRetailor(EDR_DisplayViewModel SearchResultmodel)
        {
            var search = _mapper.Map<EDR_DisplayViewModel, SearchModel>(SearchResultmodel);
            var SearchResult = await _distributorservice.SearchRetailor(search);
            var statesResult = await _unifiedservice.GetStates();

            var viewModel = new EDR_DisplayViewModel
            {
                RetailorList = SearchResult,
                StateList = statesResult,
            };
            return Json(viewModel);

        }


    }
}