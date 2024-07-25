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
    public class IndexModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;

        public IndexModel(TimeFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public SearchModel Search { get; set; } = default!;

        public class SearchModel
        {
            public string? Titre { get; set; }
            public DateTime? From { get; set; }
            public DateTime? To { get; set; }

            public bool EstActif { get; set; } = true;
        }

        public IList<Projet> Projets { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Projets != null)
            {
                IQueryable<Projet> projects = _context.Projets;
                if (Search.EstActif)
                {
                    projects = projects.Where(p => p.EstActif);
                }
                else
                {
                    projects = projects.Where(p => p.EstActif == false);
                }

                if (!string.IsNullOrEmpty(Search.Titre))
                {
                    projects = projects.Where(p => p.Titre.Contains(Search.Titre));
                }
                
                if (Search.From != null)
                {
                    projects = projects.Where(p => p.DateCreer >= Search.From);
                }

                if (Search.To != null)
                {
                    projects = projects.Where(p => p.DateCreer <= Search.To);
                }

                Projets = await projects.ToListAsync();
            }
        }
    }
}
