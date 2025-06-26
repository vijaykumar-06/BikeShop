using MediatR;
using BikeShop.Domain.Interfaces;
using BikeShop.Application.DTOs;
using BikeShop.Application.Queries;

namespace BikeShop.Application.Handlers;

public class GetAllBikesHandler : IRequestHandler<GetAllBikesQuery, List<BikeDto>>
{
    private readonly IBikeRepository _repo;
    public GetAllBikesHandler(IBikeRepository repo) => _repo = repo;

    public async Task<List<BikeDto>> Handle(GetAllBikesQuery _, CancellationToken __)
    {
        var bikes = await _repo.GetAllAsync();
        return bikes
          .Select(b => new BikeDto
          {
              Id = b.Id,
              Manufacturer = b.Manufacturer,
              Model = b.Model,
              Price = b.Price
          })
          .ToList();
    }
}
