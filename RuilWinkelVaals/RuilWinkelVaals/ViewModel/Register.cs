using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RuilWinkelVaals.ViewModel
{
    public class Register
    {
        [Required(ErrorMessage = "Er is geen e-mailadres ingevuld")]
        [Display(Name = "Email adres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Er is geen wachtwoord ingevuld")]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Er is geen wachtwoord ingevuld")]
        [Display(Name = "Wachtwoord validatie")]
        public string ValidationPassword { get; set; }
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; }
        [Display(Name = "Achternaam")]
        public string Achternaam { get; set; }

        public int Balans { get; set; }

        public int AccountType { get; set; }

        public int Ledenpas { get; set; }
        [Display(Name = "Straat")]
        public string Straat { get; set; }
        [Display(Name = "Huisnummer")]
        public string Huisnummer { get; set; }
        [Display(Name = "Woonplaats")]
        public string Woonplaats { get; set; }
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Er is geen geboortedatum ingevuld")]
        [Display(Name = "Geboortedatum")]
        public DateTime? Geboortedatum { get; set; }
        [Display(Name = "Zakelijk?")]
        public bool Zakelijk { get; set; }
    }
}