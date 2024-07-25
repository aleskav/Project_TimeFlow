using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Models;
using TimeFlow.utils;
using TimeFlow.ViewModels;

namespace TimeFlow.Pages.Approvement
{
    [Authorize(Roles = "Admin,Superviseur")]
    public class ReviewModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;

        public ReviewModel(TimeFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public FeuilleTemps Ft { get; set; } = default!; 

        public string WeekString { get; set; } = default!;

        public DecorationModel[,] Decoration { get; set; } = default!;


        public async Task OnPostConfirmerAsync(string id)
        {
            var feuille = await _context.FeuilleTemps.FindAsync(id);
            feuille.EstConfirme = true;
            _context.FeuilleTemps.Update(feuille);
            await _context.SaveChangesAsync();
            await OnGetAsync(id);
        }

        public async Task OnPostAnnulerAsync(string id)
        {
            var feuille = await _context.FeuilleTemps.FindAsync(id);
            feuille.EstConfirme = false;
            _context.FeuilleTemps.Update(feuille);
            await _context.SaveChangesAsync();
            await OnGetAsync(id);
        }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.FeuilleTemps == null)
            {
                return NotFound();
            }

            var feuilletemps = await _context.FeuilleTemps
                .Include(f => f.Employe)
                .Include(f => f.FeuilleJours)
                .ThenInclude(fj => fj.JourTaches)
                .ThenInclude(jt => jt.Tache)
                .ThenInclude(t => t.Projet)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (feuilletemps == null)
            {
                return NotFound();
            }
            else 
            {
                Ft = feuilletemps;
                Decoration = FeuilleDeTemps.IndexModel.calculateDecoration(feuilletemps);
                WeekString = WeekHelper.getWeekString(feuilletemps.Annee, feuilletemps.Semaine);
            }
            return Page();
        }
    }
}
