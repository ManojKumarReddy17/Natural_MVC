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
        public async Task<ActionResult> AssignDistributors(string executiveId, List<string> selectedDistributorIds)
        {
            if (ModelState.IsValid)
            {
                var assignDistributorModel = new DistributorToExecutive
                {
                    ExecutiveId = executiveId,
                    DistributorIds = selectedDistributorIds
                };

                var assignedResult = await _assignDistributorToExecutiveService.AssignDsitributorToExecutive(assignDistributorModel);

                var viewModel = _mapper.Map<DistributorToExecutive, AssignDistributorToExecutiveViewModel>(assignedResult);

                return View(viewModel);
            }
            return View();
        }
    }
}