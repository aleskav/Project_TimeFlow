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
    public class DetailsModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;

        public DetailsModel(TimeFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Tache Tache { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Taches == null)
            {
                return NotFound();
            }

            var tache = await _context.Taches
                .Include(t => t.MembresCharge)
                .Include(t => t.Projet)
                .FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
