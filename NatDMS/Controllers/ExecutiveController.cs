using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Natural_Core.Models;
using Naturals.Service.Service;
using System.Net;

namespace NatDMS.Controllers
{
    public class ExecutiveController : Controller

      
    {

        private readonly IExecutiveService _executiveService;
        private readonly IMapper _mapper;
        private readonly IStateService _IStateService;
        private readonly ICityService _ICityService;
        private readonly IAreaService _IAreaService;
        public ExecutiveController(IExecutiveService executiveService, IMapper mapper, IStateService IStateService, ICityService ICityService, IAreaService IAreaService)

        {
            _executiveService = executiveService;
            _IStateService = IStateService;
            _ICityService = ICityService;
            _IAreaService = IAreaService;

            _mapper = mapper;
        }
        // GET: HomeController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public async Task<ActionResult> GetState()
        {
            var result = await _IStateService.GetState();
            var distributo = _mapper.Map<List<StateModel>, List<StateViewModel>>(result);
           
            return View(distributo);
        }
            // GET: HomeController1/Create
            public ActionResult Create()
        {
            return View();
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

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public async Task<ActionResult<EditViewModel>> Edit(string id)
        {

            
            var executive = await _executiveService.GetExecutiveById(id);
            // Get states
            var statesResult = await _IStateService.GetState();
            // Get cities (assuming you have a service to fetch cities based on state ID)
            var citiesResult = await _ICityService.GetCity(/*"stn3"*/executive.State);

            // Assuming "stn3" is the selected state ID

            var AreaResult = await _IAreaService.GetArea(/*"ctn15"*/executive.City);
            var model = new EditViewModel
            {

                FirstName = executive.FirstName,
                LastName = executive.LastName,
                Email = executive.Email,
                MobileNumber = executive.MobileNumber,
                Address= executive.Address,
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
            model.State = executive.State;// Selected state
            model.City = executive.City; // Selected city, if needed
            model.Area = executive.Area;               
            return View(model);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<EditViewModel>> Edit(string id, /*IFormCollection*/EditViewModel collection)
        {
            //try
            //{
                if (ModelState.IsValid)
                {


                    //ExecutiveModel model = new ExecutiveModel();
                    var update = _mapper.Map<EditViewModel, ExecutiveModel>(collection);
                    //var update = _mapper.Map(collection, model);`
                    //update.State = collection.SelectedState;
                    //update.City = collection.SelectedCity;
                    //update.Area= collection.SelectedArea;

                    await _executiveService.UpdateDistributor(id, update);
                return RedirectToAction(nameof(GetState));
                }
                else { return View(collection); }
            }
            //catch
            //{
            //    return View();
            //}
        //}

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
