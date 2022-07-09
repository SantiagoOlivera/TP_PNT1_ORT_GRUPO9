using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TP_PNT1_ORT.Context;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Controllers
{
    public class MundialesController : Controller
    {

        private readonly MundialesContext _context;

        public MundialesController(MundialesContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            try
            {
                List<Mundial> mundiales = this._context.Mundiales.OrderByDescending(x => x.anio).ToList();

                return View(mundiales);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            Mundial mundial
        )
        {
            try{

                this._context.Mundiales.Add(mundial);
                this._context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex) {

                throw ex;           
    
            }

        }

        public IActionResult Edit(
            int anio
        )
        {
            try
            {

                Mundial mundial = this._context.Mundiales.FirstOrDefault(x => x.anio == anio);

                if (mundial != null) {
                    return View(mundial);
                }

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            Mundial mundial
        )
        {
            try
            {

                this._context.Mundiales.Update(mundial);
                this._context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public IActionResult Details(
            int anio
        )
        {
            try
            {

                Mundial mundial = this._context.Mundiales.FirstOrDefault(x => x.anio == anio);

                if (mundial != null)
                {
                    return View(mundial);
                }

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public IActionResult Delete(
            int anio
        )
        {
            try
            {

                Mundial mundial = this._context.Mundiales.FirstOrDefault(x => x.anio == anio);

                if (mundial != null)
                {
                    return View(mundial);
                }

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(
           Mundial mundial
       )
        {
            try
            {
                List<Partido> partidos = this._context.Partidos
                    .Where(x => x.mundial == mundial.anio)
                    .ToList();

                if (partidos.Count > 0)
                {

                    ViewBag.ErrorMessage = "No se puede eliminar el mundial porque ya tiene partidos asociados!";

                    return Delete(mundial.anio);

                }
                else {

                    this._context.Mundiales.Remove(mundial);
                    this._context.SaveChanges();
                    return RedirectToAction(nameof(Index));

                }

            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

    }
}
