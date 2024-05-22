using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;

namespace NatDMS.Controllers
{
    public class MapController : Controller
    {
        private readonly IMapService _mapservice;
        private readonly IMapper _Mapper;

        public MapController(IMapService mapservice, IMapper mapper)
        {
            _mapservice = mapservice;
            _Mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetLatLung()
        {
            var result = await _mapservice.GetLatLung();
            return Json(result);
        }
       
    }
}
