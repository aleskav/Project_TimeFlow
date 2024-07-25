using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeFlow.Models
{
    public class User: IdentityUser
    {

        [Required(ErrorMessage = "Le prénom est requis")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Display(Name = "Nom de famille")]
        [Required(ErrorMessage = "Le nom de famille est requis")]
        public string NomFamille { get; set; }

        [Display(Name = "Date d'embauche")]
        [Required(ErrorMessage = "La date d'embauche est requise")]
        public DateTime DateEmbauche { get; set; } = DateTime.Now;

    }
}
