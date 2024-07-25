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

namespace TimeFlow.Pages.Approvement
{
    [Authorize(Roles = "Admin,Superviseur")]
    public class IndexModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;

        public IndexModel(TimeFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FeuilleTemps> FeuilleTemps { get;set; } = default!;

        public SelectList Members { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public FilterModel Filter { get; set; } = default!;

        public class FilterModel
        {
            public string EmployeId { get; set; } = default!;

            public DateTime? From { get; set; }

            public DateTime? To { get; set; }

            public bool Approved { get; set; } = false;
        }
        public async Task OnGetAsync()
        {
            Members = new SelectList(
                await _context.Employes
                    .Where(e => e.SuperviseurId == User.GetId())
                    .Select(e => new { e.Id, FullName = $"{e.Prenom} {e.NomFamille}" }).ToListAsync(),
                "Id",
                "FullName"
                );

            IQueryable<FeuilleTemps> ft = _context.FeuilleTemps
                .Where(ft => ft.EstSoumis == true)
                .Include(f => f.Employe)
                .Where(ft => ft.Employe.SuperviseurId == User.GetId());
            
            if (!string.IsNullOrEmpty(Filter.EmployeId))
            {
                ft = ft.Where(f => f.EmployeId.Equals(Filter.EmployeId));
            }
            if (Filter.From != null && Filter.From.HasValue)
            {
                ft = ft.Where(f => WeekHelper.getMonday(f.Annee, f.Semaine) >= Filter.From);
            }
            if (Filter.To != null && Filter.To.HasValue)
            {
                ft = ft.Where(f => WeekHelper.getMonday(f.Annee, f.Semaine) <= Filter.To);
            }

            if (Filter.Approved)
            {
                ft = ft.Where(f => f.EstConfirme == true);
            }
            else
            {
                ft = ft.Where(f => f.EstConfirme == false);
            }

            FeuilleTemps = await ft.ToListAsync();
        }
    }
}
