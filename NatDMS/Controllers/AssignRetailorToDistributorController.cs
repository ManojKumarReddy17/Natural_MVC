using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;

namespace NatDMS.Controllers
{
    public class AssignRetailorToDistributorController : Controller
    {
        private readonly IAssignRetailorToDistributorService _AssignRetailorToDistributorService;

        private readonly IMapper _mapper;

        public AssignRetailorToDistributorController(IAssignRetailorToDistributorService AssignRetailorToDistributorService, IMapper mapper)
        {

            _AssignRetailorToDistributorService = AssignRetailorToDistributorService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult> AssignRetailors(string distributorId, List<string> selectedRetailorIds)
        {

            if (ModelState.IsValid)
            {
                var assignRetailorModel = new RetailorToDistributor
                {
                    DistributorId = distributorId,
                    RetailorIds = selectedRetailorIds
                };

                var assignedResult = await _AssignRetailorToDistributorService.AssignRetailorToDistributor(assignRetailorModel);

                var viewModel = _mapper.Map<RetailorToDistributor, AssignRetailorToDistributorViewModel>(assignedResult);

                return View(viewModel);
            }
            return View();
        }
    }
}
