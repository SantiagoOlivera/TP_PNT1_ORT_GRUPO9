using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TP_PNT1_ORT.Context;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UsuariosContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            UsuariosContext context
        )
        {
            _logger = logger;
            this._context = context;
        }

        public IActionResult Index(String message)
        {

            if (!String.IsNullOrEmpty(message)) { 
                ViewBag.Message = message;
            }

            String email = HttpContext.Session.GetString("email");

            if ( !String.IsNullOrEmpty(email)  ) {


                Usuario usuario = this._context.usuarios.FirstOrDefault(
                    x => x.email.Equals(email)
                );


                return View(usuario);


            }
            
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
