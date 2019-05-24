﻿using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> ListAsync(string name, string drug, decimal? proportion, string presentation, string laboratory, string order);
        Task<Medicine> FindAsync(int id);
        EntityState Update(Medicine medicine);
        Task<EntityEntry> CreateAsync(Medicine medicine);
        EntityEntry Delete(Medicine medicine);
        Task SaveChangesAsync();
        bool MedicineExists(int id);
    }
}
