using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using System.Security.Claims;

namespace NatDMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService _ILoginService;

        private readonly IMapper _mapper;
        public HomeController(ILoginService ILoginService, IMapper mapper)
        {

            _ILoginService = ILoginService;
            _mapper = mapper;
        }

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Admin Login 
        /// </summary>
        
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var create = _mapper.Map<LoginViewModel, LoginModel>(loginViewModel);
                var contents = await _ILoginService.LoginAsync(create);

                if (contents.FirstName != null && contents.LastName != null)
                {

                 // Claims Collecting //
                    var claims = new List<Claim>
                 {
                new Claim(ClaimTypes.Surname, contents.FirstName),
                new Claim(ClaimTypes.Name, contents.LastName)
                };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("DisplayDistributors", "Distributor");
                }

                ModelState.AddModelError(string.Empty, "INVALID CREDENTIALS");
            }

            return View(loginViewModel);
        }
    }
}
