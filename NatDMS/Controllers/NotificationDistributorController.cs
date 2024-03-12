using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;



namespace NatDMS.Controllers
{
    public class NotificationDistributorController : Controller
    {

        private readonly IDSRService _dsrservice;

        private readonly INotificationDistributorService _NotificationDistributorService;
        private readonly IMapper _mapper;


        public NotificationDistributorController(IDSRService dsrservice, IMapper mapper, INotificationDistributorService NotificationDistributorService)
        {
            _dsrservice = dsrservice;
            _NotificationDistributorService = NotificationDistributorService;
            _mapper = mapper;
           
        }


        public async Task<ActionResult<NotificationGetViewModel>> GetNotification()

        {

            var notifications = await _NotificationDistributorService.GetNotification();
            var viewmodel1 = _mapper.Map<List<Notification>, List<NotificationViewmodel>>((List<Notification>)notifications);
            NotificationGetViewModel viewmodel = new NotificationGetViewModel {notification = viewmodel1 };
            

            return View(viewmodel);

        }

        public async Task<JsonResult> SearchNotification([FromBody] NotificationGetViewModel search)
        {
                var searchnotification = _mapper.Map<NotificationGetViewModel, Dsrview>(search);
                var resultnotification = await _NotificationDistributorService.SearchNotification(searchnotification);
            var viewmodel1 = _mapper.Map<List<Notification>, List<NotificationViewmodel>>(resultnotification);

            return Json(viewmodel1);

        }


        public async Task<ActionResult<NotificationViewmodel>> Details(string id)
        {
            var notification = await _NotificationDistributorService.GetNotification_DistributorById(id);
          
            List<string> distributor = notification.distributorlist
  .Select(c => c.Distributor)
  .ToList();
            NotificationViewmodel viewmodel = new NotificationViewmodel
            {  Subject = notification.Body, 
                Body = notification.Subject,
                Distributor = distributor,
                Id= notification.Id
            };
            return View(viewmodel);
        }


        [HttpGet]
        public async Task<IActionResult> CreateNotification1()
        {
            var executivelist = await _dsrservice.GetExecutive();
            var newexecutivelist = _mapper.Map<List<DsrExecutiveDrop>, List<DsrExecutiveResourse>>(executivelist);
            NotificationViewmodel viewmodel = new NotificationViewmodel
            {
                ExecutiveList = newexecutivelist
            };

            return View(viewmodel);
        }


        public async Task<JsonResult> GetDistributorByExecutiveId(string executiveId)
        {
            var result = await _dsrservice.AssignedDistributorDetailsByExecutiveId(executiveId);

            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateNotification1(NotificationViewmodel viewmodel)
        {
            
            var  newexecutivelist = _mapper.Map<NotificationViewmodel, Notification>(viewmodel);
            
             newexecutivelist.distributorlist = viewmodel.Distributor.Select(x => new NotificationDistributor
            {
                Distributor = x
            }).ToList();
         var result =  await  _NotificationDistributorService.CreateNotification(newexecutivelist);
           

            if (result.StatusCode == 200)
            {
                return RedirectToAction("GetNotification", "NotificationDistributor");
            }
            else
            {
                var executivelist = await _dsrservice.GetExecutive();
                viewmodel.ExecutiveList = _mapper.Map<List<DsrExecutiveDrop>, List<DsrExecutiveResourse>>(executivelist);

                return View(viewmodel);
            }

        }


        [HttpGet]
        public async Task<ActionResult<NotificationViewmodel>> EditNotification(string id)
        {
            var notification = await _NotificationDistributorService.GetNotificationById(id);
           
            List<string> distributor = notification.distributorlist
    .Select(c => c.Distributor) 
    .ToList();


            string firstDistributor = notification.distributorlist
    .Select(c => c.Distributor)
    .FirstOrDefault();

            string notificationid = notification.distributorlist
    .Select(c => c.Notification)
    .FirstOrDefault();

            var executive =    await _NotificationDistributorService.GetExectuvieById(firstDistributor);

           var  distributorslist= await _dsrservice.AssignedDistributorDetailsByExecutiveId(executive);



            var executivelist = await _dsrservice.GetExecutive();
            var newexecutivelist = _mapper.Map<List<DsrExecutiveDrop>, List<DsrExecutiveResourse>>(executivelist);
            NotificationViewmodel viewmodel = new NotificationViewmodel
            {
                ExecutiveList = newexecutivelist,
                Executive = executive,
                Distributor = distributor,
                Body = notification.Body,
                Subject =notification.Subject,
                distributorlist = distributorslist,
                Id = notificationid


            };
     
            return View(viewmodel);

        }


        [HttpPost]
        public async Task<ActionResult<NotificationViewmodel>> EditNotification(NotificationViewmodel model)
        {

            var viewmodel = _mapper.Map<NotificationViewmodel, Notification>(model);
            string notificcationid = model.Id;
            viewmodel.distributorlist = model.Distributor.Select(x => new NotificationDistributor
            {
                Distributor = x,
               Notification =  notificcationid
            }).ToList();
         var result =   await _NotificationDistributorService.Updatenotification(viewmodel, notificcationid);

            return RedirectToAction("Details", new { id = notificcationid });
        }

        public async Task<ActionResult> DeleteNotification(string Id)
        {
            await _NotificationDistributorService.DeleteNotification(Id);
            return RedirectToAction("GetNotification", "NotificationDistributor");
        }

    }
}

