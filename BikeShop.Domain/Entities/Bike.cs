using System;

namespace BikeShop.Domain.Entities
{
    public class Bike
    {
        public int Id { get; internal set; }
        public Guid Ref { get; private set; }      // new
        public string Manufacturer { get; private set; }
        public string Model { get; private set; }
        public decimal Price { get; private set; }
        public string Category { get; private set; }      // new
        public string Colour { get; private set; }      // new
        public string Weight { get; private set; }      // new
        public string ImgUrl { get; private set; }      // new

        protected Bike() { } // EF

        public Bike(
            string manufacturer,
            string model,
            decimal price,
            string category,
            string colour,
            string weight,
            string imgUrl
        )
        {
            if (string.IsNullOrWhiteSpace(manufacturer)) throw new ArgumentException(nameof(manufacturer));
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException(nameof(model));
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException(nameof(category));
            if (string.IsNullOrWhiteSpace(colour)) throw new ArgumentException(nameof(colour));
            if (string.IsNullOrWhiteSpace(weight)) throw new ArgumentException(nameof(weight));
            if (string.IsNullOrWhiteSpace(imgUrl)) throw new ArgumentException(nameof(imgUrl));

            Manufacturer = manufacturer;
            Model = model;
            Price = price;
            Category = category;
            Colour = colour;
            Weight = weight;
            ImgUrl = imgUrl;
            Ref = Guid.NewGuid();
        }
    }
}
