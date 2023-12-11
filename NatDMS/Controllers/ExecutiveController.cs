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
      

      
        public async Task<ActionResult> GetState()
        {
            var result = await _IStateService.GetState();
            var distributo = _mapper.Map<List<StateModel>, List<StateViewModel>>(result);
           
            return View(distributo);
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
            
            var statesResult = await _IStateService.GetState();

            var citiesResult = await _ICityService.GetCity(executive.State);

            var AreaResult = await _IAreaService.GetArea(executive.City);

            var model = new EditViewModel
            {

                FirstName = executive.FirstName,
                LastName = executive.LastName,
                Email = executive.Email,
                MobileNumber = executive.MobileNumber,
                Address= executive.Address,
                  UserName =executive.UserName,
         Password = executive.Password,
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
            model.State = executive.State;
            model.City = executive.City; 
            model.Area = executive.Area;               
            return View(model);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<EditViewModel>> Edit(string id, EditViewModel collection)
        {
            
                if (ModelState.IsValid)
                {
 
                    var update = _mapper.Map<EditViewModel, ExecutiveModel>(collection); 

                    await _executiveService.UpdateDistributor(id, update);

                return RedirectToAction(nameof(GetState));
                }
                else { return View(collection); }
            
         }

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
