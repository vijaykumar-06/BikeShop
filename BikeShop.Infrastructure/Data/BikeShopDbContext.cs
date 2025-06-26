using BikeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace BikeShop.Infrastructure.Data;

public class BikeShopDbContext : DbContext
{
    public BikeShopDbContext(DbContextOptions<BikeShopDbContext> options)
      : base(options) { }

    public DbSet<Bike> Bikes { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Bike>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Manufacturer).IsRequired();
            b.Property(x => x.Model).IsRequired();
            b.Property(x => x.Price).HasColumnType("decimal(18,2)");
        });
    }
}
