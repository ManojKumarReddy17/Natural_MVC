﻿using AutoMapper;
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
        private readonly IExecutiveService _ExecutiveService;
        private readonly IMapper _mapper;
        private readonly IUnifiedService _unifiedservice;
      
        public ExecutiveController(IExecutiveService ExecutiveService, IMapper mapper, IUnifiedService unifiedService)
        {
            _ExecutiveService = ExecutiveService;
            _mapper = mapper;
            _unifiedservice = unifiedService;   
        
        }


        /// <summary>
        /// DISPLAYING LIST OF ALL EXECUTIVES 
        /// </summary>
        public async Task<ActionResult<ExecutiveModel>> DisplayExecutives()
        {
            var dspexe = await _ExecutiveService.GetAllDeatils();
            var dspmap = _mapper.Map<List<ExecutiveModel>, List<ExecutiveViewModel>>(dspexe);
            return View(dspmap);
        }


        /// <summary>
        /// GETTING EXECUTIVE DETAILS BY ID
        /// </summary>
        /// 
        [HttpGet]
        public async Task<ActionResult> ExecutiveDetailsById(string id)
        {
            var distribtuors = await _ExecutiveService.GetExecutiveDetailsById(id);
            var mapped = _mapper.Map<ExecutiveViewModel>(distribtuors);
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
        /// CREATING NEW EXECUTIVE
        /// </summary>

        public async Task<ActionResult> CreateExecutive()
        {
            var viewmodel = new SaveExecutiveViewModel();
            viewmodel.States = await _unifiedservice.GetStates();
            return View(viewmodel);
        }

        /// <summary>
        /// INSERTING CREATED EXECUTIVE DATA
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateExecutive(SaveExecutiveViewModel saveexecmdl)
        {
            
                if (ModelState.IsValid)
                {
                var createexecutive = _mapper.Map<SaveExecutiveViewModel,ExecutiveModel>(saveexecmdl); 

                var displayxexecutive = await _ExecutiveService.CreateExecutive(createexecutive);
            
                return RedirectToAction(nameof(DisplayExecutives));
                }

            else
                {
                ModelState.AddModelError(string.Empty, "Form submission failed . please check the procided data");
                return View(saveexecmdl);

                }
        }

      
        /// <summary>
        /// GETTING EXISTING DATA FOR UPDATE
        /// </summary>

        public async Task<ActionResult<ED_EditViewModel>> EditExecutive(string id)
        {
            var executive = await _ExecutiveService.GetExecutiveById(id);

            var statesResult = await _unifiedservice.GetStates();

            var citiesResult = await _unifiedservice.GetCitiesbyStateId(executive.State);

            var AreaResult = await _unifiedservice.GetAreasByCityId(executive.City);
            var model = new ED_EditViewModel
            {

                FirstName = executive.FirstName,
                LastName = executive.LastName,
                Email = executive.Email,
                MobileNumber = executive.MobileNumber,
                Address = executive.Address,
                UserName = executive.UserName,
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

        /// <summary>
        /// POSTING UPDATED EXECUTIVE DATA
        /// </summary>
        /// 

        [HttpPost]
        public async Task<ActionResult<ED_EditViewModel>> EditExecutive(string id, ED_EditViewModel Editviewmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var update = _mapper.Map<ED_EditViewModel, ExecutiveModel>(Editviewmodel);

                    await _ExecutiveService.UpdateExecutive(id, update);

                    return RedirectToAction(nameof(DisplayExecutives));
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
        /// DELETING EXECUTIVE BY ID
        /// </summary>

        public async Task<ActionResult> DeleteExecutive(string ExecutiveId)
        {
            await _ExecutiveService.DeleteExecutive(ExecutiveId);
            return RedirectToAction(nameof(DisplayExecutives));
        }
    }
}
