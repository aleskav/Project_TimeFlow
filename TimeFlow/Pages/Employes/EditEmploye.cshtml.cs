using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Data;
using TimeFlow.Models;
using TimeFlow.Services;

namespace TimeFlow.Pages.Employes
{
    [Authorize(Roles = "Admin")]
    public class EditEmployeModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;

        public EditEmployeModel(TimeFlow.Data.ApplicationDbContext context, UserManager<User> userManager , UserService userService)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
        }

        [BindProperty]
        public Employe Employe { get; set; } = default!;

        public string Role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Employes == null)
            {
                return NotFound();
            }

            var employe =  await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (employe == null)
            {
                return NotFound();
            }

            var role = await _userManager.GetRolesAsync(employe);
            if (role == null)
            {
                return NotFound();
            }
            else if (role.Contains("Superviseur"))
            {
                return RedirectToPage("./EditSuperviseur", new { id });
            }
            else if (role.Contains("Admin"))
            {
                return RedirectToPage("./EditAdmin");
            }

            Role = "Employe";
            Employe = (Employe) employe;

            ViewData["SuperviseurId"] = new SelectList(_context.Superviseurs, "Id", "NomFamille");
            ViewData["Role"] = new SelectList(_context.Roles, "Name", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var exist = await _context.Employes.FirstOrDefaultAsync(u => u.Id.Equals(Employe.Id));

            exist.NomFamille = Employe.NomFamille;
            exist.Prenom = Employe.Prenom;
            exist.SuperviseurId = Employe.SuperviseurId;

            _context.Employes.Update(exist);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostChangeRole(string Role)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name.Equals(Role));

            if (role == null || role.Name == null )
            {
                return NotFound();
            }
            
            var result = true;
            if (role.Name.Equals("Superviseur"))
            {
                result = await _userService.toSuperviseur(Employe.Id);
            }
            else if (role.Name.Equals("Admin"))
            {
                result = await _userService.toEmploye(Employe.Id);
            }

            if (!result)
            {
                ModelState.AddModelError("Role", "Impossible de changer le role de l'employe");
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private bool EmployeExists(string id)
        {
          return (_context.Employes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
