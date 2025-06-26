using MediatR;
using BikeShop.Domain.Entities;
using BikeShop.Domain.Interfaces;
using BikeShop.Application.Commands;

namespace BikeShop.Application.Handlers;

public class CreateBikeHandler : IRequestHandler<CreateBikeCommand, int>
{
    private readonly IBikeRepository _repo;
    public CreateBikeHandler(IBikeRepository repo) => _repo = repo;

    public async Task<int> Handle(CreateBikeCommand cmd, CancellationToken _)
    {
        var bike = new Bike(cmd.Manufacturer, cmd.Model, cmd.Price);
        await _repo.AddAsync(bike);
        return bike.Id;
    }
}
