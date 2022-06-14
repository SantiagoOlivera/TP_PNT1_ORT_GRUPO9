using System.ComponentModel.DataAnnotations;

namespace TP_PNT1_ORT.Models
{
    public class Apuesta
    {

        [Key]
        public int idGrupo { get; set; }
        [Key]
        public string email { get; set; }
        [Key]
        public int idPartido { get; set; }
        public int golesEquipo1 { get; set; }
        public int golesEquipo2 { get; set; }
        public Partido partido { get; set; }


    }
}
