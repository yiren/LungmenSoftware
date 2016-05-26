using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading;
using System.Web;

namespace LungmenSoftware.Models.HOS
{
    public class HOSDbContext:DbContext
    {
        public HOSDbContext()
            :base("name=HOSData")
        {
            
        }

        public DbSet<FDDR> Fddrs { get; set; }
        public DbSet<FDI> Fdis { get; set; }
        public DbSet<FddrToFdi> FddrToFdis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FDDRConfiguration());
            modelBuilder.Configurations.Add(new FDIConfiguration());
            modelBuilder.Configurations.Add(new FddrToFdiConfiguration());
        }
    }

    public class FddrToFdiConfiguration
        :EntityTypeConfiguration<FddrToFdi>
    {

    }

    public class FDIConfiguration
        :EntityTypeConfiguration<FDI>
    {

    }

    public class FDDRConfiguration
        :EntityTypeConfiguration<FDDR>
    {

    }
}