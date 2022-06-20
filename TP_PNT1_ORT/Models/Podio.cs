using System.Collections.Generic;

namespace TP_PNT1_ORT.Models
{
    public class Podio
    {
        public Grupo grupo { get; set; }
        public List<PosicionPodio> posiciones { get; set; }

    }

    public class PosicionPodio {

        public Usuario usuario { get; set; }

        public int puntos { get; set; }

        public PosicionPodio(
            Usuario usuario,
            int puntos
        ) { 
            this.usuario = usuario;
            this.puntos = puntos;
        }

    }
}
