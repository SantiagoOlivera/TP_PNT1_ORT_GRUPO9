using System;

namespace TP_PNT1_ORT.Models
{
    public class Signup
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }

    }
}
