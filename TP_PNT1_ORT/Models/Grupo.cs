using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP_PNT1_ORT.Models
{
    public class Grupo{

        [Key]
        public int idGrupo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public IList<UsuarioGrupo> UsuariosGrupos { get; set; } = new List<UsuarioGrupo>();

    }
}
