using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.Models;
using System.Security.Claims;
using AutoMapper;
using Natural.Core.IServices;
using Naturals.Service.Service;

namespace NatDMS.Controllers
{
    public class DistributorLoginController : Controller
    {
        private readonly IDistributorLoginService _IDistributorLoginServiceervice;

        private readonly IMapper _mapper;
        public DistributorLoginController(IDistributorLoginService IDistributorLoginServiceervice, IMapper mapper)
        {

            _IDistributorLoginServiceervice = IDistributorLoginServiceervice;
            _mapper = mapper;
        }

        public ActionResult DistributorLogins()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<ActionResult> DistributorLogins(DistributorLoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var create = _mapper.Map<DistributorLoginViewModel,DistributorLoginModel>(loginViewModel);
                var contents = await _IDistributorLoginServiceervice.DistributorLogin(create);

                if (contents.FirstName != null)
                {
                    // Claims Collecting //
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Surname, contents.FirstName),
                //new Claim(ClaimTypes.Name, contents.LastName)
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Map the contents back to the view model
                    var viewModel = _mapper.Map<DistributorLoginModel>(contents);

                    // Store the view model in TempData
                    TempData["DistributorLoginModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);

                    return RedirectToAction("GetDistributorReport", "DistributorLoginReports");
                }

                ModelState.AddModelError(string.Empty, "INVALID CREDENTIALS");
            }

            return View(loginViewModel); // Return the same view with the model in case of error
        }

    }
}
