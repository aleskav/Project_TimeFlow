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
    public class IndexModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;

        public IndexModel(TimeFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Tache> Taches { get;set; } = default!;

        public SelectList Projets { get; set; } = default!;

        public SelectList Members { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public SearchModel Search { get; set; } = default!;

        public class SearchModel
        {
            public string? Titre { get; set; }
            public string? ProjetId { get; set; }
            public string? MembreChargeId { get; set; }
            public DateTime? From { get; set; }
            public DateTime? To { get; set; }
        }

        public async Task OnGetAsync()
        {
           
            IQueryable<Tache> taches = _context.Taches
                .Include(t => t.MembresCharge)
                .Include(t => t.Projet);
                   
            if (Search.ProjetId != null)
            {
                taches = taches.Where(t => t.ProjetId.Equals(Search.ProjetId));
            }
            if (Search.MembreChargeId != null)
            {
                var employe = await _context.Employes
                    .FirstOrDefaultAsync(e => e.Id.Equals(Search.MembreChargeId));
                if (employe != null)
                {
                    taches = taches.Where(t => t.MembresCharge.Contains(employe));
                }
            }
            if (Search.From != null)
            {
                taches = taches.Where(t => t.DateCreer >= Search.From);
            }
            if (Search.To != null)
            {
                taches = taches.Where(t => t.DateCreer <= Search.To);
            }

            if (!string.IsNullOrEmpty(Search.Titre))
            {
                taches = taches.Where(t => t.Titre.Contains(Search.Titre));
            }

            Taches = await taches.ToListAsync();
            Projets = new SelectList(await _context.Projets.ToListAsync(), "Id", "Titre");
            Members = new SelectList(
                await _context.Employes
                    .Where(e => e.SuperviseurId == User.GetId())
                    .Select(e => new { e.Id, FullName = $"{e.Prenom} {e.NomFamille}" })
                    .ToListAsync(),
                "Id",
                "FullName");
        }
    }
}
