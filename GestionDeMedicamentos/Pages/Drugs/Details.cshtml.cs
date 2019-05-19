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
    public class DetailsModel : PageModel
    {
        private readonly GestiónDeMedicamentos.Database.PostgreContext _context;

        public DetailsModel(GestiónDeMedicamentos.Database.PostgreContext context)
        {
            _context = context;
        }

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
    }
}
