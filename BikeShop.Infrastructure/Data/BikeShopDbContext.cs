using Microsoft.EntityFrameworkCore;    // brings in DbContext & ModelBuilder
using BikeShop.Domain.Entities;         // brings in your Bike entity

namespace BikeShop.Infrastructure.Data
{
    public class BikeShopDbContext : DbContext   // ← inherits from DbContext
    {
        public BikeShopDbContext(DbContextOptions<BikeShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bike> Bikes { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Bike>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Ref).HasDefaultValueSql("NEWID()");
                b.Property(x => x.Manufacturer).IsRequired();
                b.Property(x => x.Model).IsRequired();
                b.Property(x => x.Price).HasColumnType("decimal(18,2)");
                b.Property(x => x.Category).IsRequired();
                b.Property(x => x.Colour).IsRequired();
                b.Property(x => x.Weight).IsRequired();
                b.Property(x => x.ImgUrl).IsRequired();
                b.HasIndex(x => new { x.Manufacturer, x.Model }).IsUnique();
            });
        }
    }
}
