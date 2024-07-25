using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeFlow.Models;

namespace TimeFlow.Pages.Employes
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(TimeFlow.Data.ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<UserWithRoles> Users { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                Users = _userManager.Users.Select(u => new UserWithRoles
                {
                    Id = u.Id,
                    Email = u.Email,
                    NomFamille = u.NomFamille,
                    Prenom = u.Prenom,
                    DateEmbauche = u.DateEmbauche,
                    roles = _userManager.GetRolesAsync(u).Result.ToList()
                }).ToList();
            }
        }

        public class UserWithRoles
        {
            public string Id { get; set; } = default!;
            public string Email { get; set; } = default!;

            public string NomFamille { get; set; } = default!;
            public string Prenom { get; set; } = default!;
            public DateTime DateEmbauche { get; set; } = default!;

            public List<string>? roles { get; set; } = default!;
        }   
    }
}
