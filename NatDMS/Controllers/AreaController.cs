using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;

namespace NatDMS.Controllers
{
    public class AreaController : Controller
    {

        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;
        private readonly IUnifiedService _unifiedService;
        private readonly IConfiguration _configuration;

        public AreaController(IAreaService areaService, IMapper mapper, IUnifiedService unifiedService,
            IConfiguration configuration)
        {
            _areaService = areaService;
            _mapper = mapper;
            _unifiedService = unifiedService;
            _configuration = configuration;
        }
        public async Task<ActionResult> GetCitiesbyStateId(string stateId)
        {
            var result = await _unifiedService.GetCitiesbyStateId(stateId);
            return Json(result);
        }

        /// <summary>
        /// GETTING AREA'S LIST FOR DROPDOWN BASED ON CITY_ID
        /// </summary>


        public async Task<JsonResult> GetAreasByCityId(string cityId)
        {
            var result = await _unifiedService.GetAreasByCityId(cityId);

            return Json(result);
        }



        [HttpGet]
        public async Task<ActionResult<AreaCUmodel>> DisplayAreas(int page = 1)
        {
            // Retrieve data and map to AreaViewModel
            var resultarea = await _areaService.GetAreas();

            var viewmodel = _mapper.Map<List<AreaModel>, List<AreaCUmodel>>(resultarea);
            var areaPgn = new PageNation<AreaCUmodel>(viewmodel, _configuration, page);
            var paginatedData = areaPgn.GetPaginatedData(viewmodel);

            ViewBag.Pages = areaPgn;
            var statesResult = await _unifiedService.GetStates();
            var CityResult = await _unifiedService.GetCities();
            var viewModel = new AreaViewModel
            {
                AreaList = resultarea,
                StateList = statesResult,
                CityList= CityResult
            };
            return View(viewModel);


        }
        public async Task<ActionResult<AreaDisplayModel>> AreaDetailsById(string id)
        {
            var AreaDetai = await _areaService.GetAreaDetailsbyId(id);
            if (AreaDetai == null)
            {
                return NotFound();
            }
            var mapped = _mapper.Map<AreaModel, AreaDisplayModel>(AreaDetai);
            return View(mapped);

        }
        public async Task<ActionResult> CreateArea()
        {
            var viewModel = new AreaDisplayModel
            {
                StateList = await _unifiedService.GetStates(),
                CityList = await _unifiedService.GetCities()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<AreaDisplayModel>> CreateArea(AreaDisplayModel Areamodel)
        {
            //if (ModelState.IsValid)
            //{
            //    var result = _mapper.Map<AreaDisplayModel, AreaModel>(Areamodel);
            //    var createArea = await _areaService.CreateAreas(result);
            //   // TempData["CreatedArea"] = createArea;
            //    return RedirectToAction("DisplayAreas", "Area");
            //}
            //else
            //{
            //    ModelState.AddModelError(String.Empty, "Entered Invalid credentials, Please Re Enter the Credentials");
            //    return View(Areamodel);
            //}

            var result = _mapper.Map<AreaDisplayModel, AreaModel>(Areamodel);
            var createArea = await _areaService.CreateAreas(result);
            // TempData["CreatedArea"] = createArea;
            return RedirectToAction("DisplayAreas", "Area");

        }


        
        [HttpGet]
        public async Task<ActionResult<AreaDisplayModel>> EditArea(String Id)
        {
            var Areamodel = await _areaService.GetAreaDetailsbyId(Id);
            var areaedit = _mapper.Map<AreaModel, AreaDisplayModel>(Areamodel);
            
            return View(areaedit);


        }
        [HttpPost]
        public async Task<ActionResult> EditArea(string Id, AreaDisplayModel areadisplaymodel)
        {
            if (areadisplaymodel.Id != null && areadisplaymodel.AreaName != null)
            {
                var value = await _areaService.GetAreas();
                //var stateid = value.Where(x => x.Id == areadisplaymodel.Id).Select(x => x.StateId).FirstOrDefault();
                //areadisplaymodel.StateId = stateid;
                var cityid = value.Where(x => x.Id == areadisplaymodel.Id).Select(x => x.CityId).FirstOrDefault();
                areadisplaymodel.CityId = cityid;

                var area = _mapper.Map<AreaDisplayModel, AreaModel>(areadisplaymodel);
                var result = await _areaService.EditArea(Id, area);

                return RedirectToAction("DisplayAreas", "Area", new { id = result.CityId });
               


            }
            else
            {
               
                ModelState.AddModelError(String.Empty, "Entered Invalid credentials, Please Re Enter the Credentials");
                return View(areadisplaymodel);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAreas(string Id)
        {
            await _areaService.DeleteArea(Id);
            return RedirectToAction("DisplayAreas", "Area");
        }

        

    }
}

