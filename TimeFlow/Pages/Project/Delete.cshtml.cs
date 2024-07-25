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

namespace TimeFlow.Pages.Project
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
      public Projet Projet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Projets == null)
            {
                return NotFound();
            }

            var projet = await _context.Projets.FirstOrDefaultAsync(m => m.Id == id);

            if (projet == null)
            {
                return NotFound();
            }
            else 
            {
                Projet = projet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Projets == null)
            {
                return NotFound();
            }
            var projet = await _context.Projets.FindAsync(id);

            if (projet != null)
            {
                projet.EstActif = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
