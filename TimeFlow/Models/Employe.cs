
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeFlow.Models
{
    public class Employe: User
    {
        [ForeignKey("Superviseur")]
        public string? SuperviseurId { get; set; }

        public Superviseur? Superviseur { get; set; }

        public List<Tache> Taches { get; set; } = new();


        public List<FeuilleTemps> FeuilleTemps { get; set; } = new();
    }
}
