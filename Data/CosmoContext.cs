using design_pattern_repository.Domain.Entities;
using design_pattern_repository.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace design_pattern_repository.Data
{
    public class CosmoContext : DbContext
    {
        public DbSet<Planet> Planet { get; set; }
        public DbSet<Star> Star { get; set; }

        public CosmoContext(DbContextOptions<CosmoContext> options) : base(options) { 
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(ConnectionDatabase.GetConnectionDatabase().GetConnectionString());
        }
        public CosmoContext()
        {

        }
    }
}
