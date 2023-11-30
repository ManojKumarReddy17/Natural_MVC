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


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var create = _mapper.Map<LoginViewModel, LoginModel>(model);
                var contents = await _ILoginService.LoginAsync(create);

                if (contents.FirstName != null && contents.LastName != null)
                {
                    var claims = new List<Claim>
                 {
                new Claim(ClaimTypes.Surname, contents.FirstName),
                new Claim(ClaimTypes.Name, contents.LastName)
                };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                  //  Redirect to the distributor page

                    return RedirectToAction("DisplayDistributors", "Distributor");
                }

                ModelState.AddModelError(string.Empty, "INVALID CREDENTIALS");
            }

            return View(model);
        }
    }
}
