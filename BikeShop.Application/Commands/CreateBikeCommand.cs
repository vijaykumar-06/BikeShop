// BikeShop.Application/Commands/CreateBikeCommand.cs

using MediatR;

namespace BikeShop.Application.Commands
{
    public class CreateBikeCommand : IRequest<int>
    {
        public string Manufacturer { get; }
        public string Model { get; }
        public decimal Price { get; }
        public string Category { get; }
        public string Colour { get; }
        public string Weight { get; }
        public string ImgUrl { get; }

        public CreateBikeCommand(
            string manufacturer,
            string model,
            decimal price,
            string category,
            string colour,
            string weight,
            string imgUrl
        )
        {
            Manufacturer = manufacturer;
            Model = model;
            Price = price;
            Category = category;
            Colour = colour;
            Weight = weight;
            ImgUrl = imgUrl;
        }
    }
}
