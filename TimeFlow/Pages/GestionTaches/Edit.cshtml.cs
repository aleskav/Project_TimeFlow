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
    public class EditModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;

        public EditModel(TimeFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TacheModel Tache { get; set; } = default!;

        public class TacheModel
        {
            public string Id { get; set; } = default!;
            public string Titre { get; set; } = default!;
            public string Description { get; set; } = default!;
            public string ProjetId { get; set; } = default!;
            public List<string> ? MembresCharges { get; set; } = default!;

            public DateTime DateCreer { get; set; } = default!;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Taches == null)
            {
                return NotFound();
            }

            var tache =  await _context.Taches
                .Include(t => t.MembresCharge)
                .Include(t => t.Projet)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tache == null)
            {
                return NotFound();
            }

            if (!tache.Projet.EstActif)
            {
                ModelState.AddModelError(string.Empty, "Le projet est inactif, vous ne pouvez pas modifier la tâche.");
                return Page();
            }

            Tache = new TacheModel
            {
                Id = tache.Id,
                Titre = tache.Titre,
                Description = tache.Description,
                ProjetId = tache.ProjetId,
                DateCreer = tache.DateCreer,
                MembresCharges = tache.MembresCharge.Select(e => e.Id).ToList()
            };

            ViewData["MembreChargeId"] = new SelectList(
                _context.Employes
                    .Where(e => e.SuperviseurId == User.GetId())
                    .Select(e => new { e.Id, FullName = $"{e.Prenom} {e.NomFamille}" }), 
                "Id", 
                "FullName"
            );

            ViewData["ProjetId"] = new SelectList(_context.Projets.Where(p => p.EstActif), "Id", "Titre");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await OnGetAsync(Tache.Id);
            }

            var exist = await _context.Taches
                .Where(t => t.Id.Equals(Tache.Id))
                .Include(t => t.MembresCharge)
                .FirstOrDefaultAsync();

            if (exist == null)
            {
                return NotFound();
            }

            exist.Titre = Tache.Titre;
            exist.Description = Tache.Description;
            exist.ProjetId = Tache.ProjetId;

            if (Tache.MembresCharges != null)
            {
                // les membres à supprimer (ceux qui ne sont pas dans la liste et qui sont supervisés par l'utilisateur)
                var membersToDelete  = exist.MembresCharge.Where(m => (
                    m.SuperviseurId == User.GetId() &&
                    !Tache.MembresCharges.Contains(m.Id) 
                )).ToList();

                foreach (var membre in membersToDelete)
                {
                    exist.MembresCharge.Remove(membre);
                }

                var membres = await _context.Employes
                    .Where(e => Tache.MembresCharges.Contains(e.Id))
                    .ToListAsync();
                
                foreach (var membre in membres)
                {
                    if (!exist.MembresCharge.Contains(membre))
                    {
                        exist.MembresCharge.Add(membre);
                    }
                }
            }

            _context.Taches.Update(exist);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
