using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TP_PNT1_ORT.Models
{
    public class Mundial {

        [Key]
        public int anio { get; set; }
        public string descripcion { get; set; }
        public string pais { get; set; }

    }

}
