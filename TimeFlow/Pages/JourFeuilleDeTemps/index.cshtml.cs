using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeFlow.Data;
using TimeFlow.Models;

namespace TimeFlow.Pages.JourFeuilleDeTemps
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _db;

        public String FtId { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public async Task OnGet(string FtId)
        {
            if (FtId == null)
            {
                RedirectToPage("/FeuilleDeTemps/Index");
            }

            var ft = await _db.FeuilleTemps.FindAsync(FtId);
            if (ft == null)
            {
                RedirectToPage("/FeuilleDeTemps/Index");
            }
            else
            {
                FtId = ft.Id;
            }
        }


    }
}
