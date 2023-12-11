using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;

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
            var Create = _mapper.Map<LoginViewModel, LoginModel>(model);

            if (ModelState.IsValid)
            {
                var contents = await _ILoginService.LoginAsync(Create);

                if (contents.FirstName != null && contents.LastName != null)
                {
                   
                 return RedirectToAction("DisplayExecutive", "Executive", contents);

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "INVALID CREDENTIALS");
                    return View(model);
                }
            }
            return View();
        }

    }
}