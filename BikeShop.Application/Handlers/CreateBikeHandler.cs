using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BikeShop.Application.Commands;
using BikeShop.Application.Exceptions;    // your custom exception
using BikeShop.Domain.Entities;
using BikeShop.Domain.Interfaces;

namespace BikeShop.Application.Handlers
{
    public class CreateBikeHandler : IRequestHandler<CreateBikeCommand, int>
    {
        private readonly IBikeRepository _repo;
        public CreateBikeHandler(IBikeRepository repo) => _repo = repo;

        public async Task<int> Handle(CreateBikeCommand cmd, CancellationToken ct)
        {
            // Check if a bike with the same Manufacturer+Model already exists
            if (await _repo.ExistsAsync(b =>
                b.Manufacturer == cmd.Manufacturer &&
                b.Model == cmd.Model
            ))
            {
                // Instead of returning the existing Id, throw a 409-style exception
                throw new DuplicateBikeException(
                    $"A bike named “{cmd.Manufacturer} {cmd.Model}” already exists."
                );
            }

            // Otherwise create a new one
            var bike = new Bike(
                cmd.Manufacturer,
                cmd.Model,
                cmd.Price,
                cmd.Category,
                cmd.Colour,
                cmd.Weight,
                cmd.ImgUrl
            );

            await _repo.AddAsync(bike);
            await _repo.SaveChangesAsync();
            return bike.Id;
        }
    }
}
