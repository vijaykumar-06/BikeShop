using BikeShop.Domain.Entities;
using BikeShop.Domain.Interfaces;
using BikeShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Repositories;

public class EfBikeRepository : IBikeRepository
{
    private readonly BikeShopDbContext _db;
    public EfBikeRepository(BikeShopDbContext db) => _db = db;

    public async Task AddAsync(Bike bike)
    {
        await _db.Bikes.AddAsync(bike);
        await _db.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Bike>> GetAllAsync()
        => await _db.Bikes.AsNoTracking().ToListAsync();

    public async Task<Bike> GetByIdAsync(int id)
        => await _db.Bikes.FindAsync(id);
}
