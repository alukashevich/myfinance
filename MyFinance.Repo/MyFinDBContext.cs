using Microsoft.EntityFrameworkCore;
using MyFinance.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinance.Repo
{
    public class MyFinDbContext : DbContext
    {
        public MyFinDbContext(DbContextOptions<MyFinDbContext> options) : base(options)
        { }

        public DbSet<Credit> Credits { get; set; }
        public DbSet<MonthPayment> MonthPayments { get; set; }

        public DbSet<SpecialPercentagePeriod> SpecialPercentagePeriods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            const string myfinSchema = "MyFin";

            {
                var entity = builder.Entity<Credit>();
                entity.ToTable(nameof(Credit), myfinSchema);
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

                entity.HasMany(p => p.MonthPayments);
                entity.HasMany(p => p.SpecialPersentages);
            }

            {
                var entity = builder.Entity<MonthPayment>();
                entity.ToTable(nameof(MonthPayment), myfinSchema);
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            }

            {
                var entity = builder.Entity<SpecialPercentagePeriod>();
                entity.ToTable(nameof(SpecialPercentagePeriod), myfinSchema);
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
