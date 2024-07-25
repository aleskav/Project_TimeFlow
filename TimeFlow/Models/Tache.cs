using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace TimeFlow.Models
{
    public class Tache
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Le titre est requis")]
        public required string Titre { get; set; }

        [MaxLength(300, ErrorMessage = "La description ne peut pas dépasser 300 caractères")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Date de création")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Required(ErrorMessage = "La date de création est requise")]
        public required DateTime DateCreer { get; init; } = DateTime.Now;

        [ForeignKey("Projet")]
        [Required(ErrorMessage = "Le projet est requis")]
        public string ProjetId { get; set; }

        [Display(Name = "Membres chargé")]
        public List<Employe> MembresCharge { get; set; } = new();

        [ForeignKey("ProjetId")]
        public Projet Projet { get; set; }

        public List<FeuilleJour> FeuilleJours { get; set; } = new();

        public List<JourTache> JourTaches { get; set; } = new();
    }
}
