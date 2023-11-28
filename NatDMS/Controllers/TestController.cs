using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NatDMS.Controllers
{
    public class TestController : Controller
    {
        public ActionResult ManageDistributor()
        {
            return View();
        }

        public ActionResult ManageRetailor()
        {
            return View();
        }

        public ActionResult ManageCategories()
        {
            return View();
        }

        public ActionResult ManageProducts()
        {
            return View();
        }


        public ActionResult DailyDSR()
        {
            return View();

        }
        public ActionResult Reports()
        {
            return View();
        }
      
    }
}
       