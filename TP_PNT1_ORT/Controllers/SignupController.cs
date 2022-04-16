using Microsoft.AspNetCore.Mvc;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup(
            [FromBody] Signup signup
        ){

            return null;
        }

    }




}
