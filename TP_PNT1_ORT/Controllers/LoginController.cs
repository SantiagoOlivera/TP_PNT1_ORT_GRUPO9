using Microsoft.AspNetCore.Mvc;
using System;

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
            string email,
            string password
        ){

            Console.WriteLine(email);
            Console.WriteLine(password);

            return null;


        }
    }
}
