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
        private readonly ILocationService _locationservice;
        private readonly IStateService _IStateService;
        private readonly ICityService _ICityService;
        private readonly IAreaService _IAreaService;

        private readonly IMapper _mapper;
        public RetailorController(IRetailorService retailorservice, IMapper mapper, ILocationService locationService, IStateService IStateService, ICityService ICityService, IAreaService IAreaService)

        {
            _retailorservice = retailorservice;
            _IStateService = IStateService;
            _ICityService = ICityService;
            _IAreaService = IAreaService;
            _locationservice = locationService;
            _mapper = mapper;
        }


        public async Task<ActionResult<RetailorModel>> DisplayRetailors()
        {
            var result = await _retailorservice.GetRetailors();
            var mapped = _mapper.Map<List<RetailorModel>, List<RetailorViewModel>>(result);

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



        public async Task<ActionResult> CreateRetailors()
        {
            var viewModel = new SaveRetailorViewModel();
            viewModel.States = await _IStateService.GetState();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRetailors(SaveRetailorViewModel retailorModel)
        {
            if (ModelState.IsValid)
            {
                var retailor = _mapper.Map<SaveRetailorViewModel, RetailorModel>(retailorModel);

                var Createretailor = await _retailorservice.CreateRetailors(retailor);

                return RedirectToAction("DisplayRetailors", "Retailor");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Form submission failed. Please check the provided data");
                return View(retailorModel);
            }

        }

        public async Task<IActionResult> DeleteRetailor(string retailorId)
        {
            await _retailorservice.DeleteRetailor(retailorId);
            return RedirectToAction("DisplayRetailors", "Retailor");
        }
        public async Task<ActionResult> RetailorDetails(string id)
        {
            var result = await _retailorservice.GetRetailorsById(id);
            var mapped = _mapper.Map<SaveRetailorViewModel>(result);

            return View(mapped);

        }
        
    }

}




