// BikeShop.Application/DTOs/BikeDto.cs

namespace BikeShop.Application.DTOs
{
    public class BikeDto
    {
        public int Id { get; set; }
        public Guid Ref { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Colour { get; set; }
        public string Weight { get; set; }
        public string ImgUrl { get; set; }
    }
}
