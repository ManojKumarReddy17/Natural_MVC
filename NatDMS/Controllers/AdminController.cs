using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.Models;

namespace NatDMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        public AdminController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Verify(LoginModel loginModel)
        {
           var result = _mapper.Map<LoginModel,LoginResultModel>(loginModel);

            return View(result);
        }
    }
}