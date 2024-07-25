using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Data;
using TimeFlow.Models;
using TimeFlow.utils;

namespace TimeFlow.Pages.GestionTaches
{
    [Authorize(Roles = "Admin,Superviseur")]
    public class CreateModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;

        public CreateModel(TimeFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Projets"] = new SelectList(_context.Projets.Where(p => p.EstActif), "Id", "Titre");
            ViewData["Membres"] = new SelectList(
                _context.Employes
                    .Where(e => e.SuperviseurId == User.GetId())
                    .Select(e => new { e.Id, FullName = $"{e.Prenom} {e.NomFamille}" }), 
                "Id", 
                "FullName"
            );
            return Page();
        }

        [BindProperty]
        public Tache Tache { get; set; } = default!;

        [BindProperty]
        public List<string> MembresCharges { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if ( _context.Taches == null || Tache == null)
            {
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Tache.MembresCharge = await _context.Employes.Where(e => MembresCharges.Contains(e.Id)).ToListAsync();
            _context.Taches.Add(Tache);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
