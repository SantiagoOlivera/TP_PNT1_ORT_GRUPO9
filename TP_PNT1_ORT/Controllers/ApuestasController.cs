using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP_PNT1_ORT.Context;
using TP_PNT1_ORT.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace TP_PNT1_ORT.Controllers
{
    public class ApuestasController : BaseController
    {

        private readonly ApuestasContext _context;
        private readonly UsuariosContext _usuariosContext;

        public ApuestasController(
            ApuestasContext context, 
            UsuariosContext usuariosContext
        ){
            _context = context;
            _usuariosContext = usuariosContext;
        }
        // GET: ApuestasController
        public ActionResult Index()
        {

            try{


                //_usuariosContext.Usuarios.Where(x => x.email.Equals(""));

                if (this.usuario != null)
                {
                    ViewData["usuario"] = this.usuario;

                    List<Grupo> grupos = this._context.UsuariosGrupos
                        .Where( x  =>  x.email.Equals(usuario) )
                        .Select(x => x.grupo)
                        .ToList();

                    return View(grupos);

                }


                ViewBag.ErrorMessage = "Login para hacer apuesta!";

                return View();

            }
            catch (Exception ex) {

                throw ex;

            }
            //if (HttpContext.Session.GetString("email") != null) {

            //    Console.WriteLine(HttpContext.Session.GetString("email"));

            //}

            

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

        [HttpGet]
        public ActionResult Edit(
            int idGrupo,
            string usuario,
            int mundial
        ) {

            try
            {

                List<Apuesta> apuestas = new List<Apuesta>();

                List<Partido> partidos = this._context.partidos
                    .Where(x => x.mundial == mundial)
                    .ToList();

                foreach (Partido p in partidos) {

                    Apuesta apuesta = this._context.apuestas
                        .FirstOrDefault(x =>
                            x.idGrupo == idGrupo
                            &&
                            x.idPartido == p.idPartido
                            &&
                            x.email.Equals(usuario)
                        );

                    if (apuesta == null)
                    {

                        apuestas.Add(new Apuesta()
                        {
                            email = usuario,
                            idGrupo = idGrupo,
                            idPartido = p.idPartido,
                            golesEquipo1 = null,
                            golesEquipo2 = null,
                            partido = p
                        });

                    }
                    else {

                        //si existe la apuesta
                        apuestas.Add(new Apuesta()
                        {
                            email = apuesta.email,
                            idGrupo = apuesta.idGrupo,
                            idPartido = apuesta.idPartido,
                            golesEquipo1 = apuesta.golesEquipo1,
                            golesEquipo2 = apuesta.golesEquipo2,
                            partido = apuesta.partido
                        });

                    }


                   

                }


                return View(apuestas);


            }
            catch (Exception ex) {

                throw ex;

            }

            
        }


        // GET: ApuestasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            List<Apuesta> apuestas
        ){

            try{

                foreach (Apuesta a in apuestas) {


                    if (a.golesEquipo1 != null && a.golesEquipo2 != null) {

                        Apuesta apuesta = this._context.apuestas.FirstOrDefault(x =>
                                x.idGrupo == a.idGrupo
                                &&
                                x.idPartido == a.idPartido
                                &&
                                x.email.Equals(a.email)
                        );


                        if (apuesta == null)
                        {
                            //si no existe agregamos
                            this._context.apuestas.Add(a);
                        }
                        else
                        {
                            //si existe lo actulizamos
                            apuesta.golesEquipo1 = a.golesEquipo1;
                            apuesta.golesEquipo2 = a.golesEquipo2;

                            this._context.apuestas.Update(apuesta);

                        }

                        this._context.SaveChanges();
                        
                    }
                    
                }


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex) {

                throw ex;

            }

            
        }
        

    }
}
