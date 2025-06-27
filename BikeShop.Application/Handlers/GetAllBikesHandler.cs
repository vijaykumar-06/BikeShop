using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BikeShop.Application.DTOs;
using BikeShop.Application.Queries;
using BikeShop.Domain.Interfaces;

namespace BikeShop.Application.Handlers
{
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
                    Ref = b.Ref,
                    Manufacturer = b.Manufacturer,
                    Model = b.Model,
                    Price = b.Price,    // e.g. "€415.00"
                    Category = b.Category,
                    Colour = b.Colour,
                    Weight = b.Weight,
                    ImgUrl = b.ImgUrl
                })
                .ToList();
        }
    }
}
