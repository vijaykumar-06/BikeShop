using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BikeShop.Domain.Entities;
using BikeShop.Domain.Interfaces;
using BikeShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Repositories
{
    public class EfBikeRepository : IBikeRepository
    {
        private readonly BikeShopDbContext _ctx;
        public EfBikeRepository(BikeShopDbContext ctx) => _ctx = ctx;

        public Task<IReadOnlyList<Bike>> GetAllAsync()
            => _ctx.Bikes.ToListAsync().ContinueWith(t => (IReadOnlyList<Bike>)t.Result);

        public Task<Bike?> GetByIdAsync(int id)
            => _ctx.Bikes.FindAsync(id).AsTask();

        public Task AddAsync(Bike bike)
            => _ctx.Bikes.AddAsync(bike).AsTask();
        public Task<int> SaveChangesAsync()
        => _ctx.SaveChangesAsync();

        // ← new implementations:

        public Task<bool> ExistsAsync(Expression<Func<Bike, bool>> predicate)
            => _ctx.Bikes.AnyAsync(predicate);

        public Task<Bike?> FindAsync(Expression<Func<Bike, bool>> predicate)
            => _ctx.Bikes.FirstOrDefaultAsync(predicate);
    }
}
