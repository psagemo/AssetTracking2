﻿using AssetTracking2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2.Data
{
    internal class AssetTracker2Context :DbContext
    {
        public DbSet<Asset> Assets { get; set; } = null!;
        public DbSet<Office> Offices { get; set; } = null!;
        public DbSet<MobilePhone> MobilePhones { get; set; } = null!;
        public DbSet<Laptop> Laptops { get; set; } = null!;
        
        string connectionString = "data source=LAPTOP-0OPDGJ0V;initial catalog=master;trusted_connection=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
