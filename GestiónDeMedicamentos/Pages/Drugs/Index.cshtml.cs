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
    public class IndexModel : PageModel
    {
        private readonly GestiónDeMedicamentos.Database.PostgreContext _context;

        public IndexModel(GestiónDeMedicamentos.Database.PostgreContext context)
        {
            _context = context;
        }

        public IList<Drug> Drug { get;set; }

        public async Task OnGetAsync()
        {
            Drug = await _context.Drugs.ToListAsync();
        }
    }
}
