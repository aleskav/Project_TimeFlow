using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Data;
using TimeFlow.Models;

namespace TimeFlow.Pages.GestionTaches
{
    [Authorize(Roles = "Admin,Superviseur")]
    public class DeleteModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;

        public DeleteModel(TimeFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Tache Tache { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Taches == null)
            {
                return NotFound();
            }

            var tache = await _context.Taches.FirstOrDefaultAsync(m => m.Id == id);

            if (tache == null)
            {
                return NotFound();
            }
            else 
            {
                Tache = tache;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Taches == null)
            {
                return NotFound();
            }
            var tache = await _context.Taches
                .Include(t => t.JourTaches)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tache != null)
            {
                Tache = tache;
                if (Tache.JourTaches.Count > 0)
                {
                    ModelState.AddModelError("", "La tâche est liée à un ou plusieurs Feuilles. Veuillez supprimer les jours liés à la tâche avant de la supprimer.");
                    return Page();
                }

                _context.Taches.Remove(Tache);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
