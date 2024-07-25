using System.ComponentModel.DataAnnotations;

namespace TimeFlow.Models
{
    public class JourTache
    {
        public string TacheId {  get; set; }
        public string FeuilleJourId { get; set; }

        public Tache? Tache { get; set; }
        public FeuilleJour? FeuilleJour { get; set; }

        [Required(ErrorMessage = "L'heure de début est requise")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}") ]
        [Display(Name = "Heure de début")]
        [Range(typeof(DateTime), "06:00", "23:00", ErrorMessage = "l'heure doit être entre 06:00 et 23:00")]
        public DateTime HeureDebut   { get; set; }

        [Required(ErrorMessage = "L'heure de fin est requise")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")] 
        [Display(Name = "Heure de fin")]
        [Range(typeof(DateTime), "06:00", "23:00", ErrorMessage = "l'heure doit être entre 06:00 et 23:00")]
        public DateTime HeureFin { get; set; }

        public TimeSpan Duration => HeureFin - HeureDebut;
    }
}
