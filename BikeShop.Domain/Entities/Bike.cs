namespace BikeShop.Domain.Entities;

public class Bike
{
    public int Id { get; internal set; }
    public string Manufacturer { get; private set; }
    public string Model { get; private set; }
    public decimal Price { get; private set; }

    protected Bike() { }

    public Bike(string manufacturer, string model, decimal price)
    {
        if (string.IsNullOrWhiteSpace(manufacturer)) throw new ArgumentException(nameof(manufacturer));
        if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException(nameof(model));
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
        Manufacturer = manufacturer;
        Model = model;
        Price = price;
    }
}
