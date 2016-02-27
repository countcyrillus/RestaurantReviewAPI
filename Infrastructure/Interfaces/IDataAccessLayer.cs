using Infrastructure.BusinessEntities;

namespace Infrastructure.Interfaces
{
    /// <summary>
    /// The IDataAccessLayer interface is used by the RestaurantService to store and
    /// retrieve data from the backend.
    /// </summary>
    public interface IDataAccessLayer
    {
        IRestaurant[] GetRestaurantsByCity(string cityName);

        Result AddRestaurant(IRestaurant restaurant);

        Result AddReview(IReview review);

        IReview[] GetReviewsByUser(string userName);

        Result DeleteReview(string reviewID);
    }
}
