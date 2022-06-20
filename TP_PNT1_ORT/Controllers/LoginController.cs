using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TP_PNT1_ORT.Context;
using TP_PNT1_ORT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Login(
           [FromForm] Login login
        ){

            Usuario usuario = this._context.Usuarios.FirstOrDefault(
                x => x.email.Equals(login.email) 
                && 
                x.password.Equals(login.password)
            );

            
            if (usuario != null) {

                HttpContext.Session.SetString("usuario", usuario.email);

                //ViewBag.usuario = usuario.email;

                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                // El lo que luego obtendré al acceder a User.Identity.Name
                identity.AddClaim(new Claim(ClaimTypes.Name, usuario.nombre));

                // Se utilizará para la autorización por roles
                identity.AddClaim(new Claim(ClaimTypes.Role, "ADMIN"));

                // Lo utilizaremos para acceder al Id del usuario que se encuentra en el sistema.
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.email));

                // Lo utilizaremos cuando querramos mostrar el nombre del usuario logueado en el sistema.
                identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.nombre));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                
                // En este paso se hace el login del usuario al sistema
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                //return RedirectToAction("Index", "Home", new { message = "Bienvenido " + usuario.nombre + " " + usuario.apellido + "!" });

                return View("/Views/Home/Index.cshtml");

            }

            ViewBag.Message = "Email o password invalido!";
            
            return View("/Views/Login/Index.cshtml", login);

        }

    }
}
