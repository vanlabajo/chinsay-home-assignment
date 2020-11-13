using GoGreen.Domain.Aggregates.VegetableAggregate;
using GoGreen.Domain.Common;
using GoGreen.Infra.EFCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace GoGreen.Infra.EFCore
{
    public class GoGreenContext : DbContext, IUnitOfWork
    {
        public DbSet<Vegetable> Vegetables { get; set; }

        public GoGreenContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VegetableEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            _ = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
