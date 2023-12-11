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
#nullable disable

namespace NatDMS.Controllers
{
    public class DistributorController : Controller
    {

        private readonly IDistributorService _distributorservice;
        private readonly ILocationService _locationservice;
        private readonly IStateService _IStateService;
        private readonly ICityService _ICityService;
        private readonly IAreaService _IAreaService;
        private readonly IMapper _mapper;
        
        public DistributorController(IDistributorService distributorservice, IMapper mapper,IStateService IStateService, ICityService ICityService, IAreaService IAreaService)
        {
            _distributorservice = distributorservice;
            _IStateService = IStateService;
            _ICityService = ICityService;
            _IAreaService = IAreaService;
            _mapper = mapper;
        }


        public async Task<ActionResult<DistributorModel>> DisplayDistributors()
        {
            var result = await _distributorservice.GetDistributors();
            var mapped = _mapper.Map<List<DistributorModel>,List<DistributorViewModel>>(result);

            return View(mapped);
        }

        public async Task<IActionResult> cityData(string Id)
        {
            var result = await _ICityService.GetCity(Id);
            return Json(result);
        }

        public async Task<JsonResult> GetArea(string Id)
        {
            var result = await _IAreaService.GetArea(Id);
            return Json(result);
        }

        public async Task<ActionResult<DistributorViewModel>> DetailsAsync(string id)
        {
            var result = await _distributorservice.GetDistributorById(id);
            var mapped = _mapper.Map<DistributorModel, DistributorViewModel>(result);
            return View(mapped);
        }


        public async Task<ActionResult> CreateDistributor()
        {
            var result = await _IStateService.GetState();
            var distributo = _mapper.Map<List<StateModel>, List<StateViewModel>>(result);
            ViewBag.State = distributo;
            return View();
        }
      


        public async Task<ActionResult<EditViewModel>> Edit(string id)
        {




            var distributer = await _distributorservice.GetDistributorById(id);
           
            var statesResult = await _IStateService.GetState();
           
            var citiesResult = await _ICityService.GetCity(distributer.State);

            var AreaResult = await _IAreaService.GetArea(distributer.City);
            var model = new EditViewModel
            {

                FirstName = distributer.FirstName,
                LastName = distributer.LastName,
                Email = distributer.Email,
                MobileNumber = distributer.MobileNumber,
                Address = distributer.Address,
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
            model.State = distributer.State;
            model.City = distributer.City; 
            model.Area = distributer.Area;
            return View(model);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<EditViewModel>> Edit(string id, EditViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var update = _mapper.Map<EditViewModel, DistributorModel>(collection);

                    await _distributorservice.UpdateDistributor(id, update);

                    return RedirectToAction(nameof(DisplayDistributors));
                }
                else
                {
                    return View (collection);
                }
            }
            catch
            {
                return View();
            }
        }

        }

        //public JsonResult result (SaveDistributorViewModel Distributor)
        //{ return Json(Distributor); }
}
