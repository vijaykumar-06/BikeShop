// in BikeShop.Application/Exceptions/DuplicateBikeException.cs
namespace BikeShop.Application.Exceptions
{
    public class DuplicateBikeException : Exception
    {
        public DuplicateBikeException(string message) : base(message) { }
    }
}
