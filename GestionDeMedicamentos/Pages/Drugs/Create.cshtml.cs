using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Models;

namespace GestiónDeMedicamentos.Pages.Drugs
{
    public class CreateModel : PageModel
    {
        private readonly GestiónDeMedicamentos.Database.PostgreContext _context;

        public CreateModel(GestiónDeMedicamentos.Database.PostgreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Drug Drug { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Drugs.Add(Drug);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}