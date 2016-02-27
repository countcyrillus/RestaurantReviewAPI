using System.Linq;
using Infrastructure.BusinessEntities;
using Infrastructure.Interfaces;
using Microsoft.Practices.Unity;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        [Dependency]
        public IDataAccessLayer _dataAccessLayer { get; set; }

        public Result AddRestaurant(Restaurant restaurant)
        {
            return _dataAccessLayer.AddRestaurant(restaurant);
        }

        public Result AddReview(Review review)
        {
            return _dataAccessLayer.AddReview(review);
        }

        public Result DeleteReview(string reviewID)
        {
            return _dataAccessLayer.DeleteReview(reviewID);
        }

        public Restaurant[] GetRestaurantsByCity(string cityName)
        {
            return _dataAccessLayer.GetRestaurantsByCity(cityName).Cast<Restaurant>().ToArray();
        }

        public Review[] GetReviewsByUser(string userName)
        {
            return _dataAccessLayer.GetReviewsByUser(userName).Cast<Review>().ToArray();
        }
    }
}
