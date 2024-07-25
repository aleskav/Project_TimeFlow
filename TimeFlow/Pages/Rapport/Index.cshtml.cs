using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Data;
using TimeFlow.Models;
using TimeFlow.utils;

namespace TimeFlow.Pages.Repport
{
    [Authorize(Roles = "Admin,Superviseur")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public FilterModel Filter { get; set; } = default!;
        
        public IList<Tache> Taches = default!;

        public SelectList Employes = default!;

        public class FilterModel
        {
            public string? Employe { get; set; }
            public DateTime? From { get; set; }
            public DateTime? To { get; set; }
            public bool Approved { get; set; } = true;
        }

        public async Task OnGetAsync()
        {
            Employes = new SelectList(
                await _context.Employes
                .Where(e => e.SuperviseurId == User.GetId())
                .Select(e => new { e.Id, FullName = $"{e.Prenom} {e.NomFamille}" }).ToListAsync(),
                "Id", 
                "FullName"
             );

            if (!string.IsNullOrEmpty(Filter.Employe))
            {
                var employe = await _context.Employes
                    .FirstOrDefaultAsync(e => e.Id.Equals(Filter.Employe));
                if (employe != null)
                {

                    IQueryable<Tache> fts = _context.Employes
                        .Where(e => e.Id.Equals(Filter.Employe))
                        .SelectMany(e => e.Taches)
                        .Include(t => t.Projet)
                        .Include(t => t.FeuilleJours)
                        .ThenInclude(fj => fj.JourTaches);

                    fts = fts.Include("FeuilleJours.FeuilleTemps");

                    if (Filter.From != null && Filter.From.HasValue)
                    {
                        fts = fts.Where(ft => ft.FeuilleJours.Any(fj => fj.Date >= Filter.From))
                            .Select(ft => new Tache
                            {
                                Id = ft.Id,
                                Titre = ft.Titre,
                                Description = ft.Description,
                                DateCreer = ft.DateCreer,
                                ProjetId = ft.ProjetId,
                                Projet = ft.Projet,
                                MembresCharge = ft.MembresCharge,
                                FeuilleJours = ft.FeuilleJours.Where(fj => fj.Date >= Filter.From).ToList(),
                            });
                    }

                    if (Filter.To != null && Filter.To.HasValue)
                    {
                        fts = fts.Where(ft => ft.FeuilleJours.Any(fj => fj.Date <= Filter.To))
                            .Select(ft => new Tache
                            {
                                Id = ft.Id,
                                Titre = ft.Titre,
                                Description = ft.Description,
                                DateCreer = ft.DateCreer,
                                ProjetId = ft.ProjetId,
                                Projet = ft.Projet,
                                MembresCharge = ft.MembresCharge,
                                FeuilleJours = ft.FeuilleJours.Where(fj => fj.Date <= Filter.To).ToList(),
                            });
                    }

                    Taches = await fts.ToListAsync();
                }

            } 
            else
            {
                Taches = new List<Tache>();
            }
        }
    }
}
