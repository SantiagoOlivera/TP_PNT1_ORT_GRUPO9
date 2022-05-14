using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using TP_PNT1_ORT.Context;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Controllers
{
    public class SignupController : Controller
    {

        private UsuariosContext _context;

        public SignupController(UsuariosContext context) {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("/Views/Signup/Index.cshtml");
        }

        

        [HttpPost("Signup")]
        public IActionResult Signup(
            [FromForm] Signup signup
        )
        {

            try
            {
                ViewBag.ErrorMessage = null;


                if (!signup.password.Equals(signup.confirmarPassword))
                {

                    ViewBag.ErrorMessage = "Las passwords no son iguales!";

                    return Index();

                }

                Usuario usuario = new Usuario(signup);
                
                //Usuario usuario1 = _context.usuarios.Find(x=> x.email == usuario.email);
                
                _context.Add(usuario);
                _context.SaveChanges();

                //ViewBag.Message = "Usuarios registrado correctamente! Ingrese para continuar";

                return RedirectToAction("Index", "Login", new { message = "Usuarios registrado correctamente! Ingrese para continuar" } );
                //return Redirect(_loginController.Index());


            } catch(DbUpdateException ex)  {

                ViewBag.ErrorMessage = ex.Message;
                SqliteException error = (SqliteException)ex.InnerException;
                
                if (error.SqliteErrorCode == 19) {
                    ViewBag.ErrorMessage = "Error: ya existe un usuario con este email";
                }


                return Index();

            }
            

        }

    }




}
