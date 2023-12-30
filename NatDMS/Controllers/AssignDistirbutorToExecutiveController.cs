using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;

namespace NatDMS.Controllers
{
    public class AssignDistirbutorToExecutiveController : Controller
    {
        private readonly IAssignDistributorToExecutiveService _assignDistributorToExecutiveService;
       
        private readonly IMapper _mapper;

        public AssignDistirbutorToExecutiveController(IAssignDistributorToExecutiveService assignDistributorToExecutiveService, IMapper mapper)
        {

            _assignDistributorToExecutiveService = assignDistributorToExecutiveService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAssignDistributortoExecutive(AssignDistributorToExecutiveViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedResult = _mapper.Map<AssignDistributorToExecutiveViewModel, DistributorToExecutive>(model);

                var assignedResult = await _assignDistributorToExecutiveService.AssignDsitributorToExecutive(mappedResult);
                return View(assignedResult);
            }
            else
            {
                return View(model);

            }
        }
    }
}