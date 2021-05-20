using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilWinkelVaals.ViewModel
{
    public class Login
    {
        [Required(ErrorMessage = "Er is geen e-mailadres ingevuld")]
        [Display(Name = "E-mailadres")]
        public string emailAddress { get; set; }
        [Required(ErrorMessage = "Er is geen wachtwoord ingevuld")]
        [Display(Name = "Wachtwoord")]
        public string password { get; set; }
        public int loginAttempts { get; set; }
    }
}
