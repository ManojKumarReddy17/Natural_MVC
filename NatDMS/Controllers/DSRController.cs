using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;

namespace NatDMS.Controllers
{
    public class DSRController : Controller
    {
        private readonly IDSRService _dsrservice;
        private readonly IMapper _mapper;
        public DSRController(IDSRService dsrservice,IMapper mapper)
        {
            _dsrservice = dsrservice;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<DSRModel>>> DisplayDsrs()
        {
            var dsrResult = await _dsrservice.GetDsrAll();
            var mapped = _mapper.Map<List<DSRModel>, List<DSRViewModel>>(dsrResult);

            return View(mapped);
        }

    }
}
