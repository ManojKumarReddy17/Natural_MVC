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
        public async Task<ActionResult> AssignDistributors([FromBody] DistributorToExecutivesForAssign lst)
        {
            if (ModelState.IsValid)
            {
                var assignDistributorModel = new DistributorToExecutive
                {
                   
                    ExecutiveId = lst.executiveId,
                    DistributorIds = lst.selectedDistributorIds
                };

                var assignedResult = await _assignDistributorToExecutiveService.AssignDsitributorToExecutive(assignDistributorModel);
                return Json(assignedResult.StatusCode);

            }
            return Json(null);

        }


    }
}
   