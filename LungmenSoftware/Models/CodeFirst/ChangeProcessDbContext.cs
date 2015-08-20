using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst
{
    public class ChangeProcessDbContext:DbContext
    {
        public ChangeProcessDbContext()
            :base("name=ChangeProcessData")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}