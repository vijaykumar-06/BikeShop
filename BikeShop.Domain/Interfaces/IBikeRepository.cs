using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BikeShop.Domain.Entities;

namespace BikeShop.Domain.Interfaces
{
    public interface IBikeRepository
    {
        Task<IReadOnlyList<Bike>> GetAllAsync();
        Task<Bike?> GetByIdAsync(int id);
        Task AddAsync(Bike bike);
        Task<int> SaveChangesAsync();

        // ← add these two methods
        Task<bool> ExistsAsync(Expression<Func<Bike, bool>> predicate);
        Task<Bike?> FindAsync(Expression<Func<Bike, bool>> predicate);
    }
}
