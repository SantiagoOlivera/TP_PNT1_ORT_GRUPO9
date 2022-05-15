using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TP_PNT1_ORT.Models;
using TP_PNT1_ORT.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace TP_PNT1_ORT.Controllers
{
    public class GruposController : Controller
    {

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


                    //Grupo grupo = _context.Grupos.Find(id);
                    //grupo.UsuariosGrupos = this._context.UsuariosGrupos
                    //    .Where(g => g.idGrupo == id)
                    //    .Include(g => g.grupo)
                    //    .Include(g => g.usuario)
                    //    .ToList();


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
            [Bind("idGrupo", "nombre", "descripcion")] Grupo grupo
        //IFormCollection collection
        )
        {
            try
            {

                if (ModelState.IsValid)
                {

                    //Grupo grupo1 = this._context.Grupos.FirstOrDefault( x => x.idGrupo == id);

                    _context.Update(grupo);
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

                Grupo grupo = _context.Grupos.Find(id);

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

    }

}
