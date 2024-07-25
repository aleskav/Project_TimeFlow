using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeFlow.Models
{
    public class FeuilleJour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Jour")]
        [Required(ErrorMessage = "Le jour est requis")]
        public required string JourNom { get; set; }

        [Display(Name = "Total des heures")]
        public TimeSpan HoursTravaille => new (JourTaches.Sum(r => r.Duration.Ticks));

        [ForeignKey("FeuilleTemps")]
        [Required(ErrorMessage = "La feuille de temps est requise")]
        public string FeuilleTempsId { get; set; } = default!;
        public FeuilleTemps FeuilleTemps { get; set; } = default!;

        public List<Tache> Taches { get; set; } = new();
        public List<JourTache> JourTaches { get; set; } = new();

    }
}
