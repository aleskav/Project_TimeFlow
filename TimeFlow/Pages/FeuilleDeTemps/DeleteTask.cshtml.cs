using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Data;
using TimeFlow.Models;

namespace TimeFlow.Pages.FeuilleDeTemps
{
    public class DeleteTaskModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;
        private readonly ILogger<DeleteTaskModel> _logger;
        public DeleteTaskModel(TimeFlow.Data.ApplicationDbContext context, ILogger<DeleteTaskModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public JourTache JourTache { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string feuilleJourId, string tacheId)
        {

            _logger.LogWarning("feuilleJourId: " + feuilleJourId);

            if (feuilleJourId == null || tacheId == null)
            {

                return NotFound();
            }

            var jourtache = await _context.JourTaches
                .FirstOrDefaultAsync(m => m.FeuilleJourId.Equals(feuilleJourId) && m.TacheId.Equals(tacheId))
                ;
            if (jourtache == null)
            {
                return NotFound();
            }
            else 
            {
                JourTache = jourtache;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            var jourtache = await _context.JourTaches.FindAsync(JourTache.FeuilleJourId, JourTache.TacheId);

            if (jourtache != null)
            {
                JourTache = jourtache;
                _context.JourTaches.Remove(JourTache);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
