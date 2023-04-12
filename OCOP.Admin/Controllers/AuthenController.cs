using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using OCOP.Business.System.AppUsers;
using OCOP.Utility;
using OCOP.ViewModel.System.AppUsers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Admin.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IConfiguration _configuration;

        public AuthenController(IAppUserService appUserService,IConfiguration configuration)
        {
            _appUserService = appUserService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove(SystemConstants.AdminSession);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid) return View(request);
            var result = await _appUserService.Authenticate(request);
            if (!result.IsSuccessed)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }
          
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true
            };
          
            HttpContext.Session.SetString(SystemConstants.AdminSession, result.ResultObj);         

            return RedirectToAction("Index", "Home");           
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove(SystemConstants.AdminSession);
            return Redirect("Index");
        }      
    }
}
