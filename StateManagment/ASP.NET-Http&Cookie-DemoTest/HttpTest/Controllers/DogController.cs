using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpTest.Controllers
{
    public class DogController : Controller
    {
        public IActionResult List()
        {
            var requestCookies = this.Request.Cookies;
            if (!requestCookies.ContainsKey("Authentication"))
            {
                return Unauthorized();
            }
            return View();
        }
        public IActionResult Search()
        {

            return View();
        }
    }
}
