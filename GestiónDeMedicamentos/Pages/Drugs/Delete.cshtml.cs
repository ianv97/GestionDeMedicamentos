using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Models;

namespace GestiónDeMedicamentos.Pages.Drugs
{
    public class DeleteModel : PageModel
    {
        private readonly GestiónDeMedicamentos.Database.PostgreContext _context;

        public DeleteModel(GestiónDeMedicamentos.Database.PostgreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Drug Drug { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Drug = await _context.Drugs.FirstOrDefaultAsync(m => m.Id == id);

            if (Drug == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Drug = await _context.Drugs.FindAsync(id);

            if (Drug != null)
            {
                _context.Drugs.Remove(Drug);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
