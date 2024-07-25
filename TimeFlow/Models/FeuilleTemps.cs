using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeFlow.utils;

namespace TimeFlow.Models
{
    public class FeuilleTemps
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Display(Name = "Année")]
        [Range(2000, 2100, ErrorMessage = "L'année doit être entre 2000 et 2100")]
        [Required(ErrorMessage = "L'année est requise")]
        public int Annee { get; set; } = DateTime.Now.Year;

        [Display(Name = "Semaine")]
        [Required(ErrorMessage = "La semaine est requise")]
        public int Semaine { get; set; } = WeekHelper.GetCurrentWeekNumber();

        [Display(Name = "Employé")]
        [ForeignKey("Employe")]
        [Required(ErrorMessage = "L'employé est requis")]
        public required string EmployeId { get; set; }

        [ForeignKey("EmployeId")]
        public required Employe Employe { get; set; }

        [Display(Name = "Est soumis")]
        public bool EstSoumis { get; set; } = false;

        [Display(Name = "Est confirmé")]
        public bool EstConfirme { get; set; } = false;

        [Display(Name = "Feuilles de jour")]
        public required List<FeuilleJour> FeuilleJours { get; set; } = new();

        [Display(Name = "Total des heures")]
        public TimeSpan TotaleHours => new (FeuilleJours.Sum(fj => fj.HoursTravaille.Ticks));

    }
}
