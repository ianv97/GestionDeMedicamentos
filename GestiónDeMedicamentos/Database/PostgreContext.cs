using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Database
{
    public class PostgreContext : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }

        public PostgreContext(DbContextOptions<PostgreContext> options) : base(options)
        {

        }

        public DbSet<GestiónDeMedicamentos.Models.Drug> Drug { get; set; }
    }
}
