using GestiónDeMedicamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    interface IMedicineStockOrderRepository
    {
        Task<IEnumerable<MedicineStockOrder>> ListAsync();
    }
}
