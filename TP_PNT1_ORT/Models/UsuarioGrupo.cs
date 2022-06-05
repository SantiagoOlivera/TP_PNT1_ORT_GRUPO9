using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TP_PNT1_ORT.Models
{
    [Table("UsuariosGrupos")]
    public class UsuarioGrupo {

        [Key]
        public string email { get; set; }
        public Usuario usuario { get; set; }

        [Key]
        public int idGrupo { get; set; }
        public Grupo grupo { get; set; }


        //public UsuarioGrupo(int idGrupo, string email)
        //{
        //    this.idGrupo = idGrupo;
        //    this.email = email;
        //}


    }


   

}
