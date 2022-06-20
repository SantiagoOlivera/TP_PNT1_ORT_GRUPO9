using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TP_PNT1_ORT.Controllers
{
    public class BaseController : Controller
    {
        
        public bool EsAdmin
        {
            get
            {
                string rol = User.FindFirstValue(ClaimTypes.Role);
                return rol.Equals("ADMIN");
            }
        }
        public string usuario
        {
            get
            {
                //string emailUsuario = null;
                //var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                //var a = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //var auth = HttpContext.User.Identity.IsAuthenticated; //User.FindFirstValue(ClaimTypes.NameIdentifier);

                //if (auth) {

                //    emailUsuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                //}
                string emailUsuario = HttpContext.Session.GetString("usuario");


                return emailUsuario;

            }
        }
    }
}
