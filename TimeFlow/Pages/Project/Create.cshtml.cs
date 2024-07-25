using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeFlow.Data;
using TimeFlow.Models;

namespace TimeFlow.Pages.Project
{
    [Authorize(Roles = "Admin,Superviseur")]
    public class CreateModel : PageModel
    {
        private readonly TimeFlow.Data.ApplicationDbContext _context;

        public CreateModel(TimeFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Projet Projet { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if ( _context.Projets == null || Projet == null)
            {
                return Page();
            }

            _context.Projets.Add(Projet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
