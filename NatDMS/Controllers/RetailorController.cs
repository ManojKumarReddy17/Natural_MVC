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
        private readonly IUnifiedService _unifiedservice;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public RetailorController(IRetailorService retailorservice, IMapper mapper, IUnifiedService unifiedservice, IConfiguration configuration)

        {
            _retailorservice = retailorservice;
            _unifiedservice = unifiedservice;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// DISPLAYING LIST OF ALL RETAILORS 
        /// </summary>     
        public async Task<ActionResult<List<RetailorModel>>> DisplayRetailors(int page = 1)
        {
            var retailorResult = await _retailorservice.GetAllRetailors();
            var retailorPgn = new PageNation<RetailorModel>(retailorResult, _configuration, page);

            var paginatedData = retailorPgn.GetPaginatedData(retailorResult);

            var mapped = _mapper.Map<List<RetailorModel>, List<EDR_DisplayViewModel>>(paginatedData);

            ViewBag.Pages = retailorPgn;

            var statesResult = await _unifiedservice.GetStates();

            var viewModel = new EDR_DisplayViewModel
            {
                RetailorList = paginatedData,
                StateList = statesResult
            };

            return View(viewModel);
        }



        /// <summary>
        /// GETTING RETAILOR DETAILS BY ID
        /// </summary>
        public async Task<ActionResult> RetailorDetailsBYId(string id)
        {
            var result = await _retailorservice.GetRetailorDetailsById(id);
            var mapped = _mapper.Map<RetailorViewModel>(result);

            return View(mapped);

        }

        /// <summary>
        /// GETTING CITIES LIST FOR DROPDOWN BASED ON STATE_ID
        /// </summary> 
        public async Task<IActionResult> GetCitiesbyStateId(string stateId)
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
        /// CREATING NEW RETAILOR
        /// </summary>
        public async Task<ActionResult> CreateRetailor()
        {
            var viewModel = new SaveRetailorViewModel();
            viewModel.States = await _unifiedservice.GetStates();
            return View(viewModel);
        }
        /// <summary>
        /// INSERTING CREATED RETAILOR DATA
        /// </summary>
        /// 
        [HttpPost]
        public async Task<ActionResult> CreateRetailor(SaveRetailorViewModel retailorViewModel)
        {
            if (ModelState.IsValid)
            {
                var distributor = _mapper.Map<SaveRetailorViewModel, RetailorModel>(retailorViewModel);

                var CreateRetailor = await _retailorservice.CreateRetailor(distributor);

                return RedirectToAction("DisplayRetailors", "Retailor");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Form submission failed. Please check the provided data");
                return View(retailorViewModel);
            }

        }


        /// <summary>
        ///  GETTING EXISTING DATA BY RETAILOR BY ID //
        /// </summary>

        public async Task<ActionResult> EditRetailor(string id)
        {
            var retailorDetails = await _retailorservice.GetRetailorById(id);
            var viewModel = _mapper.Map<RetailorEditViewModel>(retailorDetails);

            viewModel.States = await _unifiedservice.GetStates();
            viewModel.Cities = await _unifiedservice.GetCitiesbyStateId(retailorDetails.State);
            viewModel.Areas = await _unifiedservice.GetAreasByCityId(retailorDetails.City);
            return View(viewModel);
        }

        /// <summary>
        ///  POSTING AN UPDATED RETAILOR DATA //
        /// </summary>

        [HttpPost]
        public async Task<ActionResult> EditRetailor(string id, RetailorEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var updatedRetailor = _mapper.Map<RetailorEditViewModel, RetailorModel>(viewModel);
                await _retailorservice.UpdateRetailor(id, updatedRetailor);
                return RedirectToAction("DisplayRetailors", "Retailor");
            }
            else
            {
                viewModel.States = await _unifiedservice.GetStates();
                viewModel.Cities = await _unifiedservice.GetCitiesbyStateId(viewModel.State);
                viewModel.Areas = await _unifiedservice.GetAreasByCityId(viewModel.City);

                return View(viewModel);
            }

        }

        /// <summary>
        /// DELETING RETAILOR BY ID
        /// </summary>
        public async Task<IActionResult> DeleteRetailor(string RetailorId)
        {
            await _retailorservice.DeleteRetailor(RetailorId);
            return RedirectToAction("DisplayRetailors", "Retailor");
        }


        /// <summary>
        /// SEARCH RETAILOR 
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<EDR_DisplayViewModel>> SearchRetailor(EDR_DisplayViewModel model)
        {
            var search = _mapper.Map<EDR_DisplayViewModel, SearchModel>(model);
            var SearchResult = await _retailorservice.SearchRetailor(search);
            var statesResult = await _unifiedservice.GetStates();

            var viewModel = new EDR_DisplayViewModel
            {
                RetailorList = SearchResult,
                StateList = statesResult,
            };
            return View("DisplayRetailors", viewModel);

        }

    }
}




