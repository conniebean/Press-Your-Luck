using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PressYourLuck.Models
{
    public class AuditContext : DbContext
    {
        public AuditContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<AuditType> AuditTypes { get; set; }
        public DbSet<Audit> Audits { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<AuditType>().HasData(
                new AuditType()
                {
                    AuditTypeID = 1,
                    Name = "Cash In"
                },
                new AuditType()
                {
                    AuditTypeID = 2,
                    Name = "Cash Out"
                },
                new AuditType()
                {
                    AuditTypeID = 3,
                    Name = "Win"
                },
                new AuditType()
                {
                    AuditTypeID = 4,
                    Name = "Lose"
                }
            );

        }
    }
}
