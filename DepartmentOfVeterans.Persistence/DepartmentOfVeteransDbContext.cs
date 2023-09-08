using DepartmentOfVeterans.Application.Contracts;
using DepartmentOfVeterans.Domain.Common;
using DepartmentOfVeterans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentOfVeterans.Persistence
{
    public class DepartmentOfVeteransDbContext : DbContext
    {
        public DepartmentOfVeteransDbContext(DbContextOptions<DepartmentOfVeteransDbContext> options) : base(options) {            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartmentOfVeteransDbContext).Assembly);

            //seed data, added through migrations
            modelBuilder.Entity<User>().HasData(new User
            {
                UserEmail = "karl.johnson@example.com",
                FirstName = "Karl",
                LastName = "Johnson",
                UserName = "bigpeacock170",
                UserId = Guid.NewGuid(),
                UserPassword = "scuba1",
                Picture = "https://randomuser.me/api/portraits/med/men/6.jpg",
                Gender = "male"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                UserEmail = "margot.roche@example.com",
                FirstName = "Margot",
                LastName = "Roche",
                UserName = "goldenrabbit215",
                UserId = Guid.NewGuid(),
                UserPassword = "tiger234",
                Picture = "https://randomuser.me/api/portraits/med/women/21.jpg",
                Gender = "female"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                UserEmail = "potishana.buryak@example.com",
                FirstName = "Potishana",
                LastName = "Buryak",
                UserName = "tinymeercat589",
                UserId = Guid.NewGuid(),
                UserPassword = "betty05",
                Picture = "https://randomuser.me/api/portraits/med/men/6.jpg",
                Gender = "Male"
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
