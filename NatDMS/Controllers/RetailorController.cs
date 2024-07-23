﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
            const int pageSize = 10;

            var retailorResult = await _retailorservice.GetAllRetailors1(page, pageSize);
            var statesResult = await _unifiedservice.GetStates();

            int totalItems = retailorResult.TotalItems;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            int currentPage = page;

            var viewModel = new EDR_DisplayViewModel
            {
                RetailorList = retailorResult.Items,
                StateList = statesResult,
                CurrentPage = currentPage,
                TotalPageCount = totalPages
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
            var retailorDetails = await _retailorservice.GetRetailorDetailsById(id);
            var viewModel = _mapper.Map<RetailorEditViewModel>(retailorDetails);

            viewModel.States = await _unifiedservice.GetStates();
            viewModel.Cities = await _unifiedservice.GetCitiesbyStateId(retailorDetails.StateId);
            var area = await _unifiedservice.GetAreasByCityId(retailorDetails.CityId);
            //viewModel.Areas = area.Items;
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
                //updatedRetailor.Area = updatedRetailor.AreaId;
                updatedRetailor.City = updatedRetailor.CityId;
                updatedRetailor.State = updatedRetailor.StateId;
                await _retailorservice.UpdateRetailor(id, updatedRetailor);
                return RedirectToAction("DisplayRetailors", "Retailor");
            }
            else
            {
                viewModel.States = await _unifiedservice.GetStates();
                viewModel.Cities = await _unifiedservice.GetCitiesbyStateId(viewModel.StateId);
                var area = await _unifiedservice.GetAreasByCityId(viewModel.CityId);
                //viewModel.Areas = area.Items;

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
        /// SEARCH RETAILOR PARTIAL VIEW
        /// </summary>



      
        [HttpPost]
        public async Task<ActionResult<EDR_DisplayViewModel>> SearchRetailor(EDR_DisplayViewModel searchResultModel, bool? NonAssign)
        {
            var search = _mapper.Map<EDR_DisplayViewModel, SearchModel>(searchResultModel);

            var searchResult = await _retailorservice.SearchRetailor(search, NonAssign);
            var statesResult = await _unifiedservice.GetStates();

            if (searchResult == null || searchResult.Items == null)
            {
                // Handle the case where searchResult or searchResult.Items is null
                return PartialView("_SearchRetailorPartial", new EDR_DisplayViewModel
                {
                    RetailorList = new List<RetailorModel>(),
                    StateList = statesResult
                });
            }

            var viewModel = new EDR_DisplayViewModel
            {
                RetailorList = searchResult.Items,
                StateList = statesResult
            };

            return PartialView("_SearchRetailorPartial", viewModel);
        }
     











    }
}
     



