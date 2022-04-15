using System;
using Microsoft.AspNetCore.Mvc;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(
           [FromBody] Login login
        ){

            
            return null;

        }

    }
}
