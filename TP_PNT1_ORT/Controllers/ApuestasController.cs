using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP_PNT1_ORT.Context;
using TP_PNT1_ORT.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TP_PNT1_ORT.Controllers
{
    public class ApuestasController : Controller
    {

        private readonly ApuestasContext _context;

        public ApuestasController(
           ApuestasContext context
       )
        {
            _context = context;
        }
        // GET: ApuestasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApuestasController/Details/5
        public ActionResult Details(
            int idGrupo,
            string email
        ){

            UsuarioGrupo usuarioGrupo = this._context.UsuariosGrupos
                    .Include(g => g.grupo)
                    .Include(g => g.usuario)
                    .Where(g => g.idGrupo == idGrupo && g.email.Equals(email))
                    .FirstOrDefault();

            ViewBag.usuarioGrupo = usuarioGrupo;

            List<Apuesta> apuestas = this._context.apuestas
                .Where(x =>
                    x.idGrupo == idGrupo &&
                    x.email.Equals(email)
                ).ToList();

            foreach (Apuesta apuesta in apuestas) { 
                apuesta.partido = this._context.partidos.FirstOrDefault(x => x.idPartido == apuesta.idPartido);
            }

            return View(apuestas);
        }

        // GET: ApuestasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApuestasController/Create
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

        // GET: ApuestasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApuestasController/Edit/5
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

        // GET: ApuestasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApuestasController/Delete/5
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
