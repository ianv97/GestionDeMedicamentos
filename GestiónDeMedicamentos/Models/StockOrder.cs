﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Models
{
    public class StockOrder
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; } 

    }
}
