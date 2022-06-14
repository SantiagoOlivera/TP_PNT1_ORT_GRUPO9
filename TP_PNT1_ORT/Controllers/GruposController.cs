using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TP_PNT1_ORT.Models;
using TP_PNT1_ORT.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;

namespace TP_PNT1_ORT.Controllers
{
    public class GruposController : Controller
    {
        private Regex _regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly GruposContext _context;

        public GruposController(
            GruposContext context
        )
        {
            _context = context;
        }

        // GET: GruposController
        public ActionResult Index()
        {

            ViewBag.grupos = this.List();

            return View();
        }

        private IEnumerable<Grupo> List()
        {

            //List<UsuarioGrupo> asd = this._context.UsuariosGrupos.ToList();
            //List<Usuario> usuarios = this._context.Usuarios.ToList(); 
            //List<UsuarioGrupo> listUsuarioGrupo = this._context.UsuariosGrupos.ToList();

            //this._context.UsuariosGrupos.ToList();
            //this._context.Usuarios.ToList();
            List<Grupo> listGrupos = this._context.Grupos.ToList();



            return listGrupos;
        }

        // GET: GruposController/Details/5
        public ActionResult Details(int id)
        {

            try
            {
                Grupo grupo = _context.Grupos
                    .Include(g => g.UsuariosGrupos)
                    .ThenInclude(g => g.usuario)
                    .Where(g => g.idGrupo == id)
                    .FirstOrDefault();


                if (grupo != null)
                {
                    return View(grupo);
                }

                return NotFound();

            }
            catch (Exception ex) {

                return View(ex);

            }


            return View();


        }   

        // GET: GruposController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GruposController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("nombre", "descripcion")] Grupo grupo
            //IFormCollection collection
            )
        {
            try
            {

                if (ModelState.IsValid)
                {

                    _context.Add(grupo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }


                return View();


            }
            catch
            {

                return View();

            }
        }

        // GET: GruposController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {

                if (id != null)
                {

                    Grupo grupo = _context.Grupos
                        .Include(g => g.UsuariosGrupos)
                        .ThenInclude(g => g.usuario)
                        .Where(g => g.idGrupo == id)
                        .FirstOrDefault();

                    if (grupo != null) {
                        return View(grupo);
                    }

                    return NotFound();

                }
            }
            catch (Exception ex)
            {

                return View(ex);

            }


            return View();

        }

   
        // POST: GruposController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
             Grupo grupo
        //IFormCollection collection
        )
        {
            try
            
            {

                if (ModelState.IsValid)
                {
                    Grupo oldGrupo = _context.Grupos
                           .Include(g => g.UsuariosGrupos)
                           .ThenInclude(g => g.usuario)
                           .Where(g => g.idGrupo == id)
                           .FirstOrDefault();

                    foreach (UsuarioGrupo ug in grupo.UsuariosGrupos)
                    {

                        UsuarioGrupo existe = oldGrupo.UsuariosGrupos.FirstOrDefault(x => x.idGrupo == ug.idGrupo && x.email == ug.email);

                        if (existe == null)
                        {
                            UsuarioGrupo nuevoUsuarioGrupo = new UsuarioGrupo();
                            nuevoUsuarioGrupo.idGrupo = ug.idGrupo;
                            nuevoUsuarioGrupo.email = ug.email;
                            oldGrupo.UsuariosGrupos.Add(nuevoUsuarioGrupo);
                        }

                    }


                    _context.Update(oldGrupo);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GruposController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id != null)
            {

                Grupo grupo = _context.Grupos
                    .Include(g => g.UsuariosGrupos)
                    .ThenInclude(g => g.usuario)
                    .Where(g => g.idGrupo == id)
                    .FirstOrDefault();

                if (grupo == null)
                {
                    return NotFound();
                }


                return View(grupo);
            }

            return View();
        }

        // POST: GruposController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(
            int id,
           [Bind("idGrupo", "nombre", "descripcion")] Grupo grupo
        //IFormCollection collection
        )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Remove(grupo);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        [HttpPost]

        public ActionResult AddJugador(Usuario jugador)
        {

            Usuario usuario = this._context.Usuarios
                .Where(u => u.email.Equals(jugador.email))
                .FirstOrDefault();

            return View();


        }

        [HttpPost("/Grupos/ListUsuarios")]
        public List<Usuario> ListUsuarios()
        {

            List<Usuario> listaUsuarios = this._context.Usuarios.ToList();


            return listaUsuarios;


        }

        [HttpPost("/Grupos/agregarUsuario")]
        public ActionResult agregarUsuario(string email, int idGrupo) {

            ActionResult rta = null;
            Usuario usuario = null;

            if (!string.IsNullOrEmpty(email) && this._regexEmail.IsMatch(email)) {

                usuario = this._context.Usuarios.FirstOrDefault(u => u.email.Equals(email));


                if (usuario == null)
                {

                    rta = StatusCode(500, "No existe usuario con el email ingresado!");

                }
                else {

                    Grupo grupo = _context.Grupos
                        .Include(g => g.UsuariosGrupos)
                        .ThenInclude(g => g.usuario)
                        .Where(g => g.idGrupo == idGrupo)
                        .FirstOrDefault();

                    //verificamos si ya existe el usuario en el grupo
                    bool encontrado = false;
                    int i = 0;
                    while (i < grupo.UsuariosGrupos.Count && !encontrado) {

                        if (grupo.UsuariosGrupos[i].email.Equals(email)) {
                            encontrado = true;
                        }

                        i++;
                    }

                    if (encontrado) {
                        rta = StatusCode(500, "El usuario ya existe en el grupo " + grupo.nombre + "!");
                    }
                    else {

                        rta = Json(usuario);

                    }


                }

            } else {

                rta = StatusCode(500, "El email del usuario ingresado esta vacio o es invalido!");

            }


            return rta;

        }

        [HttpPost("/Grupos/recargarJugadoresTabla")]
        public ActionResult recargarJugadoresTabla(
            int idGrupo,
            List<Usuario> usuarios
        ){
            try
            {

                if (idGrupo != null)
                {

                    Grupo grupo = _context.Grupos
                        .Include(g => g.UsuariosGrupos)
                        .ThenInclude(g => g.usuario)
                        .Where(g => g.idGrupo == idGrupo)
                        .FirstOrDefault();

                    foreach (Usuario usuario in usuarios) {
                        
                        UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
                        usuarioGrupo.email = usuario.email;
                        usuarioGrupo.idGrupo = idGrupo;

                        grupo.UsuariosGrupos.Add(usuarioGrupo);

                    }


                    if (grupo != null) {

                        return View(grupo);

                    }

                    return NotFound();

                }
            }
            catch (Exception ex)
            {

                return View(ex);

            }

            return View();

        }

    }

}
