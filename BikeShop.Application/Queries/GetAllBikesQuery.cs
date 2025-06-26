using BikeShop.Application.DTOs;
using MediatR;

namespace BikeShop.Application.Queries;

public class GetAllBikesQuery : IRequest<List<BikeDto>> { }
