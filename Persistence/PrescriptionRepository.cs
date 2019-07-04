using GestionDeMedicamentos.Controllers;
using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Domain;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Persistence
{
    public class PrescriptionRepository : BaseRepository, IPrescriptionRepository
    {
        public PrescriptionRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<PaginatedList<Prescription>> ListAsync(string date, string order, int? pageNumber, int? pageSize)
        {
            var prescriptions = _context.Prescriptions.Where(p => date == null || p.Date.ToString().StartsWith(date));

            bool descending = false;
            if (order != null)
            {
                order = order.Substring(0, 1).ToUpper() + order.Substring(1, order.Length - 1);
                if (order.EndsWith("_desc"))
                {
                    order = order.Substring(0, order.Length - 5);
                    descending = true;
                }

                if (descending)
                {
                    prescriptions = prescriptions.OrderByDescending(e => EF.Property<object>(e, order));
                }
                else
                {
                    prescriptions = prescriptions.OrderBy(e => EF.Property<object>(e, order));
                }
            }

            return await PaginatedList<Prescription>.CreateAsync(prescriptions, pageNumber ?? 1, pageSize ?? 0);
        }

        public async Task<Prescription> FindAsync(int id)
        {
            return await _context.Prescriptions.Include(p => p.MedicinePrescriptions).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<EntityEntry> CreateAsync(Prescription prescription)
        {
            return await _context.Prescriptions.AddAsync(prescription);
        }

        public EntityEntry Delete(Prescription prescription)
        {
            return _context.Prescriptions.Remove(prescription);
        }

        public bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(e => e.Id == id);
        }
    }
}
