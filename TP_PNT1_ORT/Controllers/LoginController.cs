using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TP_PNT1_ORT.Context;
using TP_PNT1_ORT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace TP_PNT1_ORT.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuariosContext _context;

        public LoginController(UsuariosContext context) {
            this._context = context;
        }

        public IActionResult Index(String message)
        {
            ViewBag.Message = message;

            return View("/Views/Login/Index.cshtml");
        }


        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(
           [FromForm] Login login
        ){

            Usuario usuario = this._context.usuarios.FirstOrDefault(
                x => x.email.Equals(login.email) 
                && 
                x.password.Equals(login.password)
            );

            
            if (usuario != null) {

                HttpContext.Session.SetString("email", usuario.email);
                
                
                
                return RedirectToAction("Index", "Home", new { message = "Bienvenido " + usuario.nombre + " " + usuario.apellido + "!" });


            }

            ViewBag.Message = "Email o password invalido!";
            
            return View("/Views/Login/Index.cshtml", login);

        }

    }
}
