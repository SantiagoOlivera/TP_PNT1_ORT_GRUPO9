using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP_PNT1_ORT.Models;
using TP_PNT1_ORT.Context;
using System.Collections.Generic;
using System.Linq;

namespace TP_PNT1_ORT.Controllers
{
    public class UsuariosGruposController : Controller
    {
        private UsuariosGruposContext _context;

        public UsuariosGruposController(UsuariosGruposContext context) {
            _context = context;
        }

        // GET: UsuariosGruposController
        public ActionResult Index()
        {
            List<UsuarioGrupo> UsuariosGrupos = this._context.UsuariosGrupos.ToList();
            List<Usuario> Usuarios = this._context.Usuarios.ToList();
            List<Grupo> Grupos = this._context.Grupos.ToList();

            return View();
        }

        // GET: UsuariosGruposController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuariosGruposController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosGruposController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosGruposController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuariosGruposController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosGruposController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuariosGruposController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
