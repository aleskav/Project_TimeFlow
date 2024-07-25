using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeFlow.Data;
using TimeFlow.Models;
using TimeFlow.utils;

namespace TimeFlow.Pages.FeuilleDeTemps
{
    [Authorize]
    public class AddTaskModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AddTaskModel> _logger; 
        
        public FeuilleTemps FeuilleDeTemps = default!;

        public SelectList Days { get; set; } = default!;
        public SelectList Tasks { get; set; } = default!;

        public AddTaskModel(ApplicationDbContext context, ILogger<AddTaskModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string id, string day )
        {
            FeuilleDeTemps = await _context.FeuilleTemps.Where(f => f.Id.Equals(id)).Include("FeuilleJours.JourTaches").FirstAsync();
            if (FeuilleDeTemps == null)
            {
                return RedirectToPage("./Index");
            }

            var employe = await _context.Employes
                .FirstOrDefaultAsync(e => e.Id.Equals(User.GetId()));

            // ajouter les tâches sauf du projet actif 
            var tasks = _context.Taches
                .Where(t => t.MembresCharge.Contains(employe))
                .Include(t => t.Projet)
                .Where(t => t.Projet.EstActif);

            Days = new SelectList(FeuilleDeTemps.FeuilleJours, "Id", "JourNom", day);
            Tasks = new SelectList(tasks.ToList(), "Id", "Titre");

            return Page();
        }


        [BindProperty]
        public JourTache JourTache { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string id, string? day)
        {
            if (ModelState.IsValid)
            {
                var exist = await _context.JourTaches
                    .Where(jt => jt.TacheId.Equals(JourTache.TacheId) && jt.FeuilleJourId.Equals(JourTache.FeuilleJourId))
                    .FirstOrDefaultAsync();

                if (exist != null)
                {
                    ModelState.AddModelError("", "Cette tâche existe déjà pour ce jour");
                    return await OnGetAsync(id, JourTache.FeuilleJourId);
                }

                var fj = await _context.FeuilleJours
                    .Where(fj => fj.Id.Equals(JourTache.FeuilleJourId))
                    .Include(fj => fj.JourTaches)
                    .FirstAsync();

                var ft = await _context.FeuilleTemps
                    .Where(ft => ft.Id.Equals(fj.FeuilleTempsId))
                    .Include("FeuilleJours.JourTaches")
                    .FirstAsync();

                // check if the time is not already taken
                foreach (var jt in fj.JourTaches)
                {
                    if (jt.HeureDebut <= JourTache.HeureDebut  && JourTache.HeureDebut < jt.HeureFin)
                    {
                        ModelState.AddModelError("", "Vous avez déjà une tâche à cette heure");
                        return await OnGetAsync(id, JourTache.FeuilleJourId);
                    }
                    else if (jt.HeureDebut <= JourTache.HeureFin && JourTache.HeureFin <= jt.HeureFin)
                    {
                        ModelState.AddModelError("", "Vous avez déjà une tâche à cette heure");
                        return await OnGetAsync(id, JourTache.FeuilleJourId);
                    }
                    else if (JourTache.HeureDebut <= jt.HeureDebut && jt.HeureFin <= JourTache.HeureFin)
                    {
                        ModelState.AddModelError("", "Vous avez déjà une tâche à cette heure");
                        return await OnGetAsync(id, JourTache.FeuilleJourId);
                    }
                }

                // check if duration is not too long
                TimeSpan hours = JourTache.HeureFin - JourTache.HeureDebut;
                TimeSpan max = new (40, 0, 0);
                TimeSpan minPerDay = new (0, 15, 0);

                if (hours < minPerDay)
                {
                    ModelState.AddModelError(string.Empty, "Vous devez travailler au moins 15 minutes par jour");
                    return await OnGetAsync(id, JourTache.FeuilleJourId);
                }

                if (ft.TotaleHours + hours > max)
                {
                    ModelState.AddModelError(string.Empty, "Vous avez dépassé le nombre d'heures de travail par semaine\n il vous reste " + (max - ft.TotaleHours).ToString(@"hh\:mm") + " ");
                    return await OnGetAsync(id, JourTache.FeuilleJourId);
                }
                _context.JourTaches.Add(JourTache);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else
            {
                return await OnGetAsync(id, JourTache.FeuilleJourId);
            }
        }
    }
}
