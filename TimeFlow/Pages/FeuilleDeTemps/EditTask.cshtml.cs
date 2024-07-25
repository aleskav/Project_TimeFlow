using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Data;
using TimeFlow.Models;

namespace TimeFlow.Pages.FeuilleDeTemps
{
    public class EditTaskModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;
        private readonly ILogger<EditTaskModel> _logger;
        public EditTaskModel(TimeFlow.Data.ApplicationDbContext context, ILogger<EditTaskModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public JourTache JourTache { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string feuilleJourId, string tacheId)
        {
            
            if (feuilleJourId == null || tacheId == null)
            {

                return NotFound();
            }

            var jourtache =  await _context.JourTaches
                .FirstOrDefaultAsync(m => m.FeuilleJourId.Equals(feuilleJourId) && m.TacheId.Equals(tacheId));
            if (jourtache == null)
            {
                return NotFound();
            }
            JourTache = jourtache;
           
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var jourTacheExist = await _context.JourTaches
                .Where(jt => jt.FeuilleJourId.Equals(JourTache.FeuilleJourId) && jt.TacheId.Equals(JourTache.TacheId))
                .Include("FeuilleJour.FeuilleTemps")
                .FirstOrDefaultAsync();

            if (jourTacheExist == null)
            {
                return NotFound();
            }

            var fj = jourTacheExist.FeuilleJour;
            var ft = fj.FeuilleTemps;

            TimeSpan addedHours = (JourTache.HeureFin - JourTache.HeureDebut) - (jourTacheExist.HeureFin - jourTacheExist.HeureDebut);
            TimeSpan max = new (40, 0, 0);
            TimeSpan minPerDay = new(0, 15, 0);

            if ((JourTache.HeureFin - JourTache.HeureDebut) < minPerDay)
            {
                ModelState.AddModelError(string.Empty, "Vous devez travailler au moins 15 minutes par jour");
                return await OnGetAsync(JourTache.FeuilleJourId, JourTache.TacheId);
            }

            if (ft.TotaleHours + addedHours > max)
            {
                ModelState.AddModelError("", "Vous avez dépassé le nombre d'heures de travail par semaine\n il vous reste "
                    + (max - ft.TotaleHours).ToString(@"hh\:mm") + "");
                return Page();
            }

            jourTacheExist.HeureDebut = JourTache.HeureDebut;
            jourTacheExist.HeureFin = JourTache.HeureFin;

            _context.JourTaches.Update(jourTacheExist);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
