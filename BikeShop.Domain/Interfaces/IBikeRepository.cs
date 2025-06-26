using BikeShop.Domain.Entities;

namespace BikeShop.Domain.Interfaces;

public interface IBikeRepository
{
    Task<IReadOnlyList<Bike>> GetAllAsync();
    Task<Bike> GetByIdAsync(int id);
    Task AddAsync(Bike bike);
}
