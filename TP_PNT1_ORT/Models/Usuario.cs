using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TP_PNT1_ORT.Models
{
    [Table("Usuarios")]
    public class Usuario{

        [Key]
        public string email { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public ICollection<UsuarioGrupo> UsuariosGrupos { get; set; }


        public Usuario() { }

        public Usuario(Signup signup) {

            this.email = signup.email;
            this.nombre = signup.nombre;
            this.apellido = signup.apellido;
            this.fechaNacimiento = signup.fechaNacimiento;
            this.password = signup.password;

        }
    }
}
