﻿using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IStockOrderRepository
    {
        Task<IEnumerable<StockOrder>> ListAsync(DateTime date, string order);
        Task<StockOrder> FindAsync(int id);
        EntityState Update(StockOrder stockOrder);
        Task<EntityEntry> CreateAsync(StockOrder stockOrder);
        EntityEntry Delete(StockOrder stockOrder);
        Task SaveChangesAsync();
        bool StockOrderExists(int id);
    }
}