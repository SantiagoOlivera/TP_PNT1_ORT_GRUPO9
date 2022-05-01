using System;

namespace TP_PNT1_ORT.Models
{
    public class Signup
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmarPassword { get; set; }

    }
}
