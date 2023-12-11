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

#nullable disable
namespace NatDMS.Controllers
{
    public class ExecutiveController : Controller

      
    {
        private readonly IExecutiveService _Service;
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;
        private readonly IStateService _state;
        private readonly ICityService _city;
        private readonly IAreaService _area;
        public ExecutiveController(IExecutiveService servicec, IMapper mapper, ILocationService location, IStateService state, ICityService city, IAreaService area)
        {
            _area = area;
            _city = city;
            _Service = servicec;
            _mapper = mapper;
            _state = state;
            _locationService = location;
        }
      
        /// <summary>
        /// Display all executives
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<ExecutiveModel>> DisplayExecutive()
        {
            var dspexe = await _Service.GetDeatils();
            var dspmap = _mapper.Map<List<ExecutiveModel>, List<ExecutiveViewModel>>(dspexe);
            return View(dspmap);
           
            return View(distributo);
        }
       

        /// <summary>
        /// Getting cities list in the dropdown based on the stateid
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> cityData(string Id)
        {
            var result = await _city.GetCity(Id);
            return Json(result);
        }

        /// <summary>
        /// Getting areas list in the dropdown based on the cityid
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetArea(string Id)
        {
            var result = await _area.GetArea(Id);

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

        /// <summary>
        /// Insert executive
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> CreateExecutive()
            
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
            var viewmodel = new SaveExecutiveViewModel();
            viewmodel.States=await _state.GetState();
            return View(viewmodel);
                     Text = area.AreaName,
                     Value = area.Id
                }).AsEnumerable()
            };
            model.State = executive.State;
            model.City = executive.City; 
            model.Area = executive.Area;               
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateExecutive(SaveExecutiveViewModel saveexecmdl)
        {
            
                if (ModelState.IsValid)
                {
                var createexecutive = _mapper.Map<SaveExecutiveViewModel,ExecutiveModel>(saveexecmdl);

                    await _executiveService.UpdateDistributor(id, update);

                var displayxexecutive = await _Service.CreateExecutive(createexecutive);
            
                return RedirectToAction("DisplayExecutive","Executive");
         }

            else
        {
                ModelState.AddModelError(string.Empty, "Form submission failed . please check the procided data");
                return View(saveexecmdl);

            }
        }

            public async Task<ActionResult> DeleteExecutive( string execmdl)
            {
            await _Service.DeleteExecutiveasync(execmdl);
            return RedirectToAction("DisplayExecutive","Executive");
            }

        public async Task<ActionResult> GetDetailsbyid(string id)
            {
            var result = await _Service.GetExecutiveDetailsById(id);
            var map =_mapper.Map<ExecutiveViewModel>(result);
            return View(map);
        }


    }
}
