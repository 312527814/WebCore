using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCoreMvc.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Web.Service.Domain;
using Microsoft.Extensions.Options;
using Web.Service.DataRepository;
using Web.Service;

namespace WebCoreMvc.Controllers
{
    [Authorize(Roles = "2,1")]
    public class HomeController : Controller
    {
        private Teacher teacher;
        public HomeController(Teacher teacher, IOptions<ConnectionSettings> con)
        {
            this.teacher = teacher;
        }
        public IActionResult Index()
        {
            foreach (var item in HttpContext.User.Claims)
            {

            }
            var role = HttpContext.User.Claims.First(s => s.Type == ClaimTypes.Role);
            var id = HttpContext.User.Claims.First(s => s.Type == "Id");
            return View();
        }


        public async Task<IActionResult> My()
        {

            await teacher.list("");
            

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string SS()
        {
            return "DDD";
        }
    }
}
