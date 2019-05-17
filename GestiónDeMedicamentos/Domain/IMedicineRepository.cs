using GestiónDeMedicamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> ListAsync();
    }
}
