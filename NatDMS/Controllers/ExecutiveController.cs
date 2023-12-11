using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
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

        public async Task<ActionResult> CreateExecutive()
        {
            var viewmodel = new SaveExecutiveViewModel();
            viewmodel.States = await _state.GetState();
            return View(viewmodel);
        }

        // GET: HomeController1/Edit/5
        [HttpPost]
        public async Task<IActionResult> CreateExecutive(SaveExecutiveViewModel saveexecmdl)
        {
            
                if (ModelState.IsValid)
                {
                var createexecutive = _mapper.Map<SaveExecutiveViewModel,ExecutiveModel>(saveexecmdl); 

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
