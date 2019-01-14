using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Utilities.ClassModels
{
    public class UserModel
    {
        public Guid id { get; set; }

        [Display(Name ="Nume Utilizator :")]
        public string Nume_Utilizator { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Parola :")]
        public string Parola { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Adresa email : ")]
        public string Email { get; set; }

        [Display(Name ="Numar Telefon : ")]
        public string Numar_Telefon { get; set; }

        [Display(Name ="Porecla : ")]
        public string Porecla { get; set; }
    }
}