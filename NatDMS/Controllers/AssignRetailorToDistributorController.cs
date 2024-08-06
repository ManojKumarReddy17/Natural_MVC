using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;

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
        //public async Task<ActionResult> AssignRetailors(string distributorId, List<string> selectedRetailorIds)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var assignRetailorModel = new RetailorToDistributor
        //        {
        //            DistributorId = distributorId,
        //            RetailorIds = selectedRetailorIds
        //        };

        //        var assignedResult = await _AssignRetailorToDistributorService.AssignRetailorToDistributor(assignRetailorModel);

        //        return Json(assignedResult.StatusCode);
        //    }
        //    return Json(null);
        //}
        public async Task<ActionResult> AssignRetailors([FromBody] RetailorToDistributorForAssign lst)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var assignRetailorModel = new RetailorToDistributor
                    {
                        DistributorId = lst.distributorId,
                        RetailorIds = lst.selectedRetailorIds
                    };

                    var assignedResult = await _AssignRetailorToDistributorService.AssignRetailorToDistributor(assignRetailorModel);

                    if (assignedResult != null)
                    {
                        // Log success
                        Console.WriteLine("AssignRetailorToDistributor Service called successfully");
                        return Json(assignedResult.StatusCode);
                    }
                    else
                    {
                        // Log null result
                        Console.WriteLine("Assigned result is null");
                        return Json("Error: Assigned result is null");
                    }
                }
                else
                {
                    // Log invalid model state
                    Console.WriteLine("Model state is invalid");
                    return Json("Error: Model state is invalid");
                }
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine("Exception occurred: " + ex.Message);
                return Json("Error: Exception occurred");
            }
        }
        
    }
}