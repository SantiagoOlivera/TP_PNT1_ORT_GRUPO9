using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TP_PNT1_ORT.Models;
using TP_PNT1_ORT.Context;
using System.Threading.Tasks;

namespace TP_PNT1_ORT.Controllers
{
    public class GruposController : Controller
    {

        private GruposContext _context;

        public GruposController(GruposContext context)
        {
            _context = context;
        }

        // GET: GruposController
        public ActionResult Index() {

            ViewBag.grupos = this.List();

            return View();
        }

        private IEnumerable<Grupo> List()
        {

            List<Grupo> listaGrupos = this._context.grupos.ToList();


            return listaGrupos;
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
            [Bind("nombre","descripcion")] Grupo grupo
            //IFormCollection collection
            )
        {
            try
            {

                if (ModelState.IsValid) {

                    _context.Add(grupo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }


                return View();


            }catch{

                return View();

            }
        }

        // GET: GruposController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id != null){

                Grupo grupo = _context.grupos.Find(id);

                if (grupo == null)
                {
                    return NotFound();
                }


                return View(grupo);
            }
            
            return View();  
            
        }

        // POST: GruposController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("idGrupo","nombre", "descripcion")] Grupo grupo
        //IFormCollection collection
        )
        {
            try
            {

                if (ModelState.IsValid) {

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

                Grupo grupo = _context.grupos.Find(id);

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
        ){
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
        
    }
}
