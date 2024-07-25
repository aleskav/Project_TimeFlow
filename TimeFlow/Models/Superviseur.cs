using System.ComponentModel.DataAnnotations;

namespace TimeFlow.Models
{
    public class Superviseur: User
    {
        [Display(Name = "Employés")]
        public IEnumerable<Employe> Employes { get; set; } = new List<Employe>();
    }
}
