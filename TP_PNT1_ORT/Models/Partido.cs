using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP_PNT1_ORT.Models
{
    public class Partido{
        [Key]
        public int idPartido { get; set; }
        public int mundial { get; set; }
        public string grupo { get; set; }
        public string equipo1 { get; set; }
        public string equipo2 { get; set; }
        public int numeroDeFecha { get; set; }
        public string fecha { get; set; }
        public string golesEquipo1 { get; set; }
        public string golesEquipo2 { get; set; }


    }
}
