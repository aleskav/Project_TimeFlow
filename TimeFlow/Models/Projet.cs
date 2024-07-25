using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeFlow.Models
{
    public class Projet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Le titre est requis")]
        public required string Titre { get; set; }

        [Required(ErrorMessage = "La Date de création est requise")]
        [Display(Name = "Date de création")]
        public required DateTime DateCreer { get; set; } = DateTime.Now;    

        public IList<Tache> Taches { get; set; } = new List<Tache>();

        public bool EstActif { get; set; } = true;
    }
}
