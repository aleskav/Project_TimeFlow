using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using TimeFlow.Data;
using TimeFlow.Models;
using TimeFlow.utils;
using TimeFlow.ViewModels;

namespace TimeFlow.Pages.FeuilleDeTemps
{
    [Authorize]
    public partial class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        public TimeSpan HeuresRestantes { get; set; } = new TimeSpan(40, 0, 0);

        public DateTime Monday
        {
            get
            {
                return WeekHelper.getMonday(WeekString);
            }
        }

        public int Year
        {
            get
            {
                return WeekHelper.getYear(WeekString);
            }
        }


        public int WeekNumber { 
            get 
            {
                return WeekHelper.getWeekNumber(WeekString);            
            } 
        }

        [BindProperty, DataType("Week")]
        [Display(Name = "Semaine")]
        public string WeekString { get; set; } = WeekHelper.getWeekString(DateTime.Now);
        
        public FeuilleTemps Ft { get; set; } = default!;
        
        public List<Tache> Taches { get; set; }= default!;

        public DecorationModel[,] Decoration { get; set; } = new DecorationModel[7, 24];

        public async Task OnPostWeekChange()
        {
            await initializeFt();
            Taches = await _db.Employes
                .Where(e => e.Id.Equals(User.GetId()))
                .Include(e => e.Taches)
                .SelectMany(e => e.Taches)
                .ToListAsync();

            HeuresRestantes = calculerHeuresRestantes();
            Decoration = calculateDecoration(Ft);
        }

        public async Task OnPostSoumettreAsync(string id)
        {
            var feuille = await _db.FeuilleTemps.FindAsync(id);
            feuille.EstSoumis = true;
            _db.FeuilleTemps.Update(feuille);
            await _db.SaveChangesAsync();
            await OnGet(null);
        }

        //constructor
        public IndexModel( ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

  
        public async Task OnGet(string? monday) 
        {
            if (monday != null)
            {
                WeekString = WeekHelper.getWeekString(DateTime.Parse(monday));
            }
            else
            {
                WeekString = WeekHelper.getWeekString(DateTime.Now);
            }

            await initializeFt();


            Taches = await _db.Employes
                .Where(e => e.Id.Equals(User.GetId()))
                .Include(e => e.Taches)
                .SelectMany(e => e.Taches)
                .ToListAsync(); 

            HeuresRestantes = calculerHeuresRestantes();
            Decoration = calculateDecoration(Ft);
        }

        public static DecorationModel[,] calculateDecoration(FeuilleTemps Ft) {

            var decoration = new DecorationModel[8, 24];
            foreach (var jour in Ft.FeuilleJours)
            {
                foreach (var jourTache in jour.JourTaches)
                {
                    int startQuarter = jourTache.HeureDebut.Minute / 15;
                    int endQuarter = jourTache.HeureFin.Minute / 15;

                    var d = new DecorationModel
                    {
                        Tache = jourTache.Tache,
                        Span = (jourTache.HeureFin.Hour - jourTache.HeureDebut.Hour-1)*4 + 4 - startQuarter + endQuarter,
                        StartHourIndex = jourTache.HeureDebut.Hour,
                        EndHourIndex = jourTache.HeureFin.Hour,
                        StartQuarterIndex = startQuarter,
                        EndQuarterIndex = endQuarter,
                        FeuilleJourId = jour.Id
                    };

                    for (int i = jourTache.HeureDebut.Hour; i <= jourTache?.HeureFin.Hour; i++)
                    {
                        decoration[((int)jour.Date.DayOfWeek + 6) % 7 + 1, i] = d;
                    }
                }
            }

            return decoration;
        }

        private TimeSpan calculerHeuresRestantes()
        {
            TimeSpan total = new(40, 0, 0);

            return total - Ft.TotaleHours;
        }

        private async Task initializeFt()
        {
            var ft = await _db.FeuilleTemps
                .Where(
                    ft => ft.Semaine.Equals(WeekNumber) 
                    && ft.Annee == Year
                    && ft.EmployeId.Equals(User.GetId())
                 )
                .Include("FeuilleJours.JourTaches.Tache.Projet")
                .FirstOrDefaultAsync();

            var employe = (Employe)await _userManager.GetUserAsync(User);

            if (employe == null)
            {
                return;
            }
            
            if (ft == null)
            {
                ft = new FeuilleTemps
                {
                    Employe = employe,
                    Semaine = WeekNumber,
                    Annee = Year,
                    EmployeId = employe.Id,
                    FeuilleJours = WeekHelper.InitializeJours(WeekNumber, Year)
                };

                var added = _db.FeuilleTemps.Add(ft);
                _db.SaveChanges();

                Ft = added.Entity;
            }
            else if (ft != null)
            {
                Ft = ft;
            }
        }
    }
}
