using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilWinkelVaals.ViewModel.RestorePassword
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Er is geen wachtwoord ingevuld")]
        [Display(Name = "Wachtwoord")]
        public string password { get; set; }

        [Required(ErrorMessage = "Er is geen wachtwoord ingevuld")]
        [Display(Name = "Bevestig wachtwoord")]
        public string passwordValidation { get; set; }
    }
}
