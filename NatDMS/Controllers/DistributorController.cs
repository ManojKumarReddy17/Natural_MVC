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
using System.Collections.Generic;
using System.Drawing.Printing;

namespace NatDMS.Controllers
{
    public class DistributorController : Controller
    {

        private readonly IDistributorService _distributorservice;
        private readonly IUnifiedService _unifiedservice;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public DistributorController(IDistributorService distributorservice, IMapper mapper, IUnifiedService unifiedservice, IConfiguration configuration, IRetailorService retailorService)

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
        //public async Task<ActionResult<List<DistributorModel>>> DisplayDistributors(int page = 1)
        //{
        //    var distributorResult = await _distributorservice.GetAllDistributorsAsync(page);
        //    var viewmodel = _mapper.Map<List<GetExecutive>, List<DistributorModel>>(distributorResult.Items);

        //    //var distributorPgn = new PageNation<DistributorModel>(viewmodel, _configuration, page);
        //    //var paginatedData = distributorPgn.GetPaginatedData(viewmodel);

        //    //ViewBag.Pages = distributorPgn;
        //    var statesResult = await _unifiedservice.GetStates();
        //    var viewModel = new EDR_DisplayViewModel
        //    {
        //        DistributorList = viewmodel,
        //        StateList = statesResult,
        //        TotalPageCount=distributorResult.TotalPageCount,
        //        TotalItems=distributorResult.TotalItems
        //    };


        //    ViewBag.CurrentPage = page;
        //    ViewBag.TotalPages = distributorResult.TotalPageCount;
        //    return View(viewModel);
        //}
        public async Task<ActionResult<EDR_DisplayViewModel>> DisplayDistributors(int page = 1)
        {
            var distributorResult = await _distributorservice.GetAllDistributorsAsync(page);
            var viewmodel = _mapper.Map<List<GetExecutive>, List<DistributorModel>>(distributorResult.Items);

            var statesResult = await _unifiedservice.GetStates();
            var viewModel = new EDR_DisplayViewModel
            {
                DistributorList = viewmodel,
                StateList = statesResult,
                TotalPageCount = distributorResult.TotalPageCount,
                TotalItems = distributorResult.TotalItems
            };

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = distributorResult.TotalPageCount;

            return View(viewModel);
        }


        /// <summary>
        /// SEARCH DISTRIBUTOR PARTIAL VIEW
        /// </summary>


        [HttpPost]
        public async Task<ActionResult<EDR_DisplayViewModel>> SearchDistributors(EDR_DisplayViewModel SearchResultmodel, bool? NonAssign, int page = 1)
        {
            const int pageSize = 10;

            var search = _mapper.Map<EDR_DisplayViewModel, SearchModel>(SearchResultmodel);

            var searchResult = await _distributorservice.SearchDistributor(search,NonAssign);
            var statesResult = await _unifiedservice.GetStates();

            if (searchResult == null || searchResult.Items == null)
            {
                return PartialView("_SearchDistributorsPartial", new List<DistributorModel>());
            }

            int totalItems = searchResult.Items.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var paginatedItems = searchResult.Items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var viewmodel = new EDR_DisplayViewModel


            {
                DistributorList = paginatedItems,
                StateList = statesResult,
                CurrentPage = page,
                TotalPageCount = totalItems
            };
            return PartialView("_SearchDistributorsPartial", viewmodel);

        }


        /// <summary>
        /// GETTING DISTRIBUTOR DETAILS BY ID
        /// </summary>
        /// 




        [HttpGet]
        public async Task<ActionResult<DistributorDetailsViewModel>> DistributorDetailsBYId(string id)
        {
          
                var distributors = await _distributorservice.GetDistributorDetailsById(id);
                var assignedDistributors = await _distributorservice.GetAssignedRetailorByDistributorId(id);

                if (distributors == null || assignedDistributors == null)
                {
                    return NotFound(); 
                }

                var mappedDistributors = _mapper.Map<DistributorModel,DistributorViewModel>(distributors);

                var distributorDetailsViewModel = new DistributorDetailsViewModel
                {
                    DistributorDetails = mappedDistributors,
                    AssignedRetailors = assignedDistributors,
                };

                return View(distributorDetailsViewModel);
            
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
                var distributor = _mapper.Map<ED_CreateViewModel, ExecutiveModel>(distributorModel);
                distributor.Area = distributorModel.Area.Select(x => new ExecutiveArea
                {
                    Area = x
                }).ToList();

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
            var viewModel = _mapper.Map<DistributorEditModel>(distributor);
            viewModel.StateList = await _unifiedservice.GetStates();
            viewModel.CityList = await _unifiedservice.GetCitiesbyStateId(distributor.StateId);
            var area = await _unifiedservice.GetAreasByCityId(distributor.CityId);
            viewModel.AreaList = area.Items;
            return View(viewModel);
        }


        /// <summary>
        /// POSTING UPDATED DISTRIBUTOR DATA
        /// </summary>



        [HttpPost]
        public async Task<ActionResult> EditDistributor(string id, DistributorEditModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                var update = _mapper.Map<DistributorEditModel, ExecutiveModel>(viewModel);
                update.State = viewModel.StateId;
                update.City = viewModel.CityId;
                update.Area.Add(new ExecutiveArea
                {
                    Area = viewModel.AreaId
                });
                //update.Area = viewModel.AreaId.Select(x => new ExecutiveArea
                //{
                //    Area = x,
                //}).ToList();

                await _distributorservice.UpdateDistributor(id, update);
                return RedirectToAction(nameof(DisplayDistributors));
            }
            else
            {
                viewModel.StateList = await _unifiedservice.GetStates();
                viewModel.CityList = await _unifiedservice.GetCitiesbyStateId(viewModel.State);
                var area = await _unifiedservice.GetAreasByCityId(viewModel.CityId);
                viewModel.AreaList = area.Items;
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
        //public async Task<ActionResult<EDR_DisplayViewModel>> SearchDistributor(EDR_DisplayViewModel SearchResultmodel)
        //{
        //    var search = _mapper.Map<EDR_DisplayViewModel, SearchModel>(SearchResultmodel);
        //    var SearchResult = await _distributorservice.SearchDistributor(search);
        //    var statesResult = await _unifiedservice.GetStates();

        //    var viewModel = new EDR_DisplayViewModel
        //    {
        //        DistributorList = SearchResult,
        //        StateList = statesResult,
        //    };

        //    return PartialView("_SearchDistributorPartial", viewModel);
        //}
        public async Task<ActionResult<EDR_DisplayViewModel>> SearchDistributor(EDR_DisplayViewModel SearchResultmodel, bool? NonAssign, int page = 1)
        {
            const int pazeSize = 10;

            
            try
            {
              

                var search = _mapper.Map<EDR_DisplayViewModel, SearchModel>(SearchResultmodel);

                var SearchResult = await _distributorservice.SearchDistributor(search,NonAssign);
                var statesResult = await _unifiedservice.GetStates();

                if (SearchResult == null || SearchResult.Items==null)
                {
                    // Handle the case where SearchResult is null, maybe return an empty view or appropriate response
                    return PartialView("_SearchDistributorPartial", new List<DistributorModel>());
                   
                    
                }

                int totalItems = SearchResult.Items.Count;
                int totalPages = (int)Math.Ceiling((double)totalItems / pazeSize);
                var paginatedItems = SearchResult.Items.Skip((page - 1) * pazeSize).Take(pazeSize).ToList();
                var viewModel = new EDR_DisplayViewModel

                {
                    DistributorList = paginatedItems,
                    StateList = statesResult,
                    CurrentPage= page,  
                    TotalPages = totalPages,    
                };

                return PartialView("_SearchDistributorPartial", viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Return an appropriate error view or message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<RetailorModel>>> ListOfRetailors(string DistributorId,int page = 1)
        {
            var retailorResult = await _distributorservice.GetNonAssignedRetailors();
            var retailorPgn = new PageNation<RetailorModel>(retailorResult.Items, _configuration, page);

            var paginatedData = retailorPgn.GetPaginatedData(retailorResult.Items);


            ViewBag.Pages = retailorPgn;

            var statesResult = await _unifiedservice.GetStates();

            var viewModel = new EDR_DisplayViewModel
            {
                RetailorList = paginatedData,
                StateList = statesResult
            };

            return View("_ListOfRetailors",viewModel);
        }

        /// <summary>
        /// SEARCH NON_ASSIGN_RETAILORS 
        /// </summary>
        

        [HttpPost]
        public async Task<JsonResult> SearchNonAssignedRetailors(EDR_DisplayViewModel SearchResultmodel)
        {
            var search = _mapper.Map<EDR_DisplayViewModel, SearchModel>(SearchResultmodel);
            var SearchResult = await _distributorservice.SearchNonAssignedRetailors(search);
            var statesResult = await _unifiedservice.GetStates();

            var viewModel = new EDR_DisplayViewModel
            {
                RetailorList = SearchResult.Items,
                StateList = statesResult
            };
            return Json(viewModel);

        }

        public async Task<IActionResult> DeleteAssignedRetailor(string retailorId, string distributorId)
        {

            var result = await _distributorservice.DeleteAssignedRetailor(retailorId, distributorId);

            return Json(result);
        }

    }
}

