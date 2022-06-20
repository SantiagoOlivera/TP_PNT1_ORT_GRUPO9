using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TP_PNT1_ORT.Context;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Controllers
{
    public class PodiosController : Controller
    {

        private readonly GruposContext _gruposContext;
        private readonly PartidosContext _partidosContext;
        private readonly ApuestasContext _apuestasContext;

        public PodiosController(
            GruposContext gruposContext,
            PartidosContext partidosContext,
            ApuestasContext apuestasContext
        ) {
            this._gruposContext = gruposContext;
            this._partidosContext = partidosContext;
            this._apuestasContext = apuestasContext;
        }

        // GET: PodioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PodioController/Details/5
        public ActionResult Details(
            int idGrupo
        ) {

            UsuarioGrupo usuarioGrupo = this._gruposContext.UsuariosGrupos
                .Include(g => g.usuario)
                .Include(g => g.grupo)
                .Where(x => x.idGrupo == idGrupo)
                .FirstOrDefault();

            Grupo grupo = _gruposContext.Grupos
                       .Include(g => g.UsuariosGrupos)
                       .ThenInclude(g => g.usuario)
                       .Where(g => g.idGrupo == idGrupo)
                       .FirstOrDefault();

            List<Partido> partidos = this._partidosContext.partidos
                .Where(x => x.mundial == 2022)
                .ToList();


            List<PosicionPodio> posiciones = new List<PosicionPodio>();

            foreach (UsuarioGrupo ug in grupo.UsuariosGrupos) {


                List<Apuesta> apuestas = this._apuestasContext.apuestas
                    .Where(x => x.email.Equals(ug.email))
                    .ToList();

                int puntosUsuario = 0;

                foreach (Apuesta apuesta in apuestas) {

                    Partido partido = partidos.Find(x => x.idPartido == apuesta.idPartido);

                    if (partido != null) {

                        //si el partido tiene definido el resultado
                        if (
                            partido.golesEquipo1 != null 
                            && 
                            partido.golesEquipo2 != null
                        ) {

                            if (
                                apuesta.golesEquipo1 == partido.golesEquipo1
                                &&
                                apuesta.golesEquipo2 == partido.golesEquipo2
                            )
                            {
                                puntosUsuario += 3;
                            }

                        }
                    }

                }

                PosicionPodio posicion = new PosicionPodio(
                    ug.usuario,
                    puntosUsuario
                );

                posiciones.Add(posicion);

            }

            
            Podio podio = new Podio();
            podio.grupo = grupo;
            podio.posiciones = posiciones.OrderByDescending(x=> x.puntos).ToList();

            return View(podio);
        }

    }
}
