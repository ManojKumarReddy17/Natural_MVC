using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using PagedList.Mvc;
using PagedList;
namespace NatDMS.Controllers
{
    public class DistributorController : Controller
    {

        private readonly IDistributorService _distributorservice;
        private readonly ILocationService _locationservice;
        private readonly IMapper _mapper;
        public DistributorController(IDistributorService distributorservice, IMapper mapper, ILocationService locationService)

        {
            _distributorservice = distributorservice;
            _locationservice = locationService;
            _mapper = mapper;
        }


        public async Task<ActionResult<DistributorModel>> DisplayDistributors()
        {
            var result = await _distributorservice.GetDistributors();
            var mapped = _mapper.Map<List<DistributorModel>,List<DistributorViewModel>>(result);

            return View(mapped);
        }


        public async Task<IActionResult> CreateDistributor()
        {
            var cities = await _locationservice.GetCities();
            var states = await _locationservice.GetStates();
            var areas = await _locationservice.GetAreas();

            ViewBag.Cities = new SelectList(cities, "Id", "CityName");
            ViewBag.States = new SelectList(states, "Id", "StateName");
            ViewBag.Areas = new SelectList(areas, "Id", "AreaName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistributor(DistributorViewModel distributorModel)
        {
            if (ModelState.IsValid)
            {

                var distributor = _mapper.Map<DistributorViewModel,DistributorModel>(distributorModel);

                // Converting city,state and area names into an id's//

                distributor.City = Request.Form["CityId"];
                distributor.State = Request.Form["StateId"];
                distributor.Area = Request.Form["AreaId"];


                var createdDistributor = await _distributorservice.CreateDistributor(distributor);


                return RedirectToAction("DisplayDistributors", "Distributor");
            }

            else
            {
                // If ModelState is not valid, reload the dropdowns and return to the Create view

                var cities = await _locationservice.GetCities();
                var states = await _locationservice.GetStates();
                var areas = await _locationservice.GetAreas();
                ViewBag.Cities = new SelectList(cities, "Id", "CityName");
                ViewBag.States = new SelectList(states, "Id", "StateName");
                ViewBag.Areas = new SelectList(areas, "Id", "AreaName");
                ModelState.AddModelError(string.Empty, "Entered Invalid crednetials, Please Re Enter the Crendentials");
                return View(distributorModel);
            }
        }
    }
}