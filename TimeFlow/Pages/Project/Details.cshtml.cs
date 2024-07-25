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
    public class DetailsModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;

        public DetailsModel(TimeFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Projet Projet { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Projets == null)
            {
                return NotFound();
            }

            var projet = await _context.Projets
                .Include(p => p.Taches)
                .ThenInclude(t => t.MembresCharge)
                .FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
