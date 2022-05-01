using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TP_PNT1_ORT.Models
{
    public class Grupo{

        [Key]
        public int idGrupo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        //public Usuario admin { get; set; }
        //public List<Usuario> jugadores { get; set; }


    }
}
