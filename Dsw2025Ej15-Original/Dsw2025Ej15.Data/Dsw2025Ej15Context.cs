using Dsw2025Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2025Ej15.Data;

public class Dsw2025Ej15Context: DbContext
{
    public Dsw2025Ej15Context(DbContextOptions<Dsw2025Ej15Context> options)
        : base(options)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>(eb =>
        {
            eb.ToTable("Categories");
            eb.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
        });
        modelBuilder.Entity<SubCategory>(eb =>
        {
            eb.ToTable("SubCategories");
            eb.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
        });
        modelBuilder.Entity<Product>(eb =>
        {
            eb.ToTable("Products");
            eb.Property(p => p.Sku)
            .HasMaxLength(20)
            .IsRequired();
            eb.Property(p => p.Name)
            .HasMaxLength(60);
            eb.Property(p => p.CurrentUnitPrice)
            .HasPrecision(15, 2);
        });
    }
}
