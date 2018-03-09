using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Web.Service.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace WebCoreMvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult MyLogin()
        {
            var s = HttpContext.Session;
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role,"1"),
                new Claim("Id","123")
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity));
            return Ok();
        }

        public IActionResult MyOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

    }
}
