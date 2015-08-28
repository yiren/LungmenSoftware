using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.CodeFirst.Entities;

namespace LungmenSoftware.Models.CodeFirst
{
    public class ChangeProcessDbContext:DbContext
    {
        public ChangeProcessDbContext()
            :base("name=ChangeProcessData")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ChangeProcessDbContext>());
        }

        public DbSet<ChangeRequest> ChangeRequests { get; set; }
        public DbSet<ChangeRequestStatusType> ChangeRequestStatusTypes { get; set; }
        public DbSet<ChangeRequestStatus> ChangeRequestStatuses { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ChangeRequestConfiguration());
            modelBuilder.Configurations.Add(new ChangeRequestStatusConfiguration());
            modelBuilder.Configurations.Add(new ChangeRequestStatusTypeConfiguration());
        }

        public class ChangeRequestConfiguration :
            EntityTypeConfiguration<ChangeRequest>
        {
            public ChangeRequestConfiguration()
            {
                HasKey(c => c.ChangeRequestId);
               // Property(c => c.ChangeRequestId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                Property(c => c.ApprovedBy).HasMaxLength(100);
                Property(c => c.CreatedBy).HasMaxLength(100).IsRequired();
                Property(c => c.ReviewBy).HasMaxLength(100);
                Property(c => c.SerialNumber).HasMaxLength(50).IsRequired();
                //Property(c => c.Description).HasMaxLength(150).IsRequired();
            }   
        }

        public class ChangeRequestStatusConfiguration :
            EntityTypeConfiguration<ChangeRequestStatus>
        {
            public ChangeRequestStatusConfiguration()
            {
                HasKey(s => s.ChangeStatusId);
                Property(s => s.InitialDate).IsRequired();
                Property(s => s.ChangeDate);
                Property(s => s.StatusTypeId).IsRequired();
                Property(s => s.ChangeRequestId).IsRequired();
                HasRequired(s => s.ChangeRequest)
                    .WithMany(c => c.ChangeRequestStatuses)
                    .HasForeignKey(s => s.ChangeRequestId);

                HasRequired(s => s.ChangeRequestStatusType)
                    .WithMany(c => c.ChangeRequestStatuses)
                    .HasForeignKey(s => s.StatusTypeId);
            }
        }
    }

    public class ChangeRequestStatusTypeConfiguration
        :EntityTypeConfiguration<ChangeRequestStatusType>
    {
        public ChangeRequestStatusTypeConfiguration()
        {
            HasKey(t => t.StatusTypeId);
            Property(t => t.StatusName).HasMaxLength(50)
                .IsRequired();
        }
    }
}