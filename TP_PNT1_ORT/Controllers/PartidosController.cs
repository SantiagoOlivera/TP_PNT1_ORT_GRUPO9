using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TP_PNT1_ORT.Context;

namespace TP_PNT1_ORT.Models
{
    
    public class PartidosController : Controller
    {
        private readonly PartidosContext _context;
        

        public PartidosController(
           PartidosContext context
        ){

            _context = context;

        }

        public IActionResult Index()
        {



            //List<int> lista = this._context.partidos
            //    .GroupBy(g => g.mundial)
            //    //.Select(g => new { mundial = g.Key , value = 2010 })
            //    .Select(g => g.Key)
            //    .ToList();

            try
            {

                List<Mundial> mundiales = this._context.mundiales
                    .OrderByDescending(x=>x.anio)
                    .ToList();

                ViewBag.ErrorMessage = TempData["ErrorMessage"];

                return View(mundiales);
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        public IActionResult List(
            int anio
        )
        {

            ViewBag.anio = anio;

            List<Partido> lista = this._context.partidos
               .Where(g => g.mundial == anio)
               .OrderBy(o => o.grupo)
               .ThenBy(o => o.numeroDeFecha)
               .ToList();


            return View(lista);

        }

        public IActionResult Details(
            int anio
        ){
            try { 

                List<Partido> lista = this._context.partidos
                    .Where(g => g.mundial == anio)
                    .OrderBy(o => o.grupo)
                    .ThenBy(o => o.numeroDeFecha)
                    .ToList();

                if (lista.Count == 0) {

                    TempData["ErrorMessage"] = "Sin datos para mundial " + anio;

                    return RedirectToAction(nameof(Index));

                }

                return View(lista);

            }
            catch (Exception ex) {
                throw ex;
            }

            

        }

        public IActionResult Create(
            int anio
        ){

            ViewBag.anio = anio;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            Partido partido
        ){
            try
            {

                if (ModelState.IsValid)
                {

                    _context.partidos.Add(partido);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));

                }

                return View();

            }
            catch
            {

                return View();

            }
        }
        public IActionResult Edit(
            int idPartido
        ){

            Partido partidoDb = this._context.partidos
                .FirstOrDefault(p =>
                    p.idPartido == idPartido
                );

            return View(partidoDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            Partido partido
        )
        {
            try
            {

                if (ModelState.IsValid)
                {

                    _context.partidos.Update(partido);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));

                }

                return View();

            }
            catch
            {

                return View();

            }


        }


        public IActionResult Delete(
            int idPartido
        )
        {

            Partido partidoDb = this._context.partidos
                 .FirstOrDefault(p =>
                     p.idPartido == idPartido
                 );

            return View(partidoDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(
            Partido partido
        )
        {
            try
            {

                if (ModelState.IsValid)
                {

                    _context.partidos.Remove(partido);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));

                }

                return View();

            }
            catch
            {

                return View();

            }


        }

    }
}
