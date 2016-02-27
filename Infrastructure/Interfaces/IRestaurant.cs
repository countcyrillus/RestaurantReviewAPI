namespace Infrastructure.Interfaces
{
    public interface IRestaurant
    {
        string RestaurantID { get; set; }

        string Name { get; set; }

        string AddressLine1 { get; set; }

        string AddressLine2 { get; set; }

        string City { get; set; }

        string State { get; set; }

        string ZipCode { get; set; }

        string PhoneNumber { get; set; }
    }
}
