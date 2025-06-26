using MediatR;

namespace BikeShop.Application.Commands;

public class CreateBikeCommand : IRequest<int>
{
    public string Manufacturer { get; }
    public string Model { get; }
    public decimal Price { get; }

    public CreateBikeCommand(string manufacturer, string model, decimal price)
    {
        Manufacturer = manufacturer;
        Model = model;
        Price = price;
    }
}
