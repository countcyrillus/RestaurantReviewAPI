using Infrastructure.BusinessEntities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestaurantService
{
    /// <summary>
    /// The RestaurantReview API Service. The service can be used with either
    /// an EntityFramework DataAccessLayer or an XML file based DataAccessLayer.
    /// </summary>
    [ServiceContract]
    public interface IService
    {
        /// <summary>
        /// Add a restaurant to the DB. If the restaurant exists the return result
        /// will have the error message and an error flag
        /// </summary>
        /// <param name="restaurant">Restaurant info</param>
        /// <returns></returns>
        [OperationContract]
        Result AddRestaurant(Restaurant restaurant);

        /// <summary>
        /// Get a list of restaurants by city. If the city name is empty all restauants
        /// are returned
        /// </summary>
        /// <param name="cityName">The name of the City</param>
        /// <returns></returns>
        [OperationContract]
        Restaurant[] GetRestaurantsByCity(string cityName);

        /// <summary>
        /// Add a review for a restaurant. The Restaurant ID passed in the review
        /// object must be valid for the review to be added.
        /// </summary>
        /// <param name="review">Review info</param>
        /// <returns></returns>
        [OperationContract]
        Result AddReview(Review review);

        /// <summary>
        /// Get a list of reviews based on the username. If no username is passed no results
        /// are returned
        /// </summary>
        /// <param name="userName">The username</param>
        /// <returns></returns>
        [OperationContract]
        Review[] GetReviewsByUser(string userName);

        /// <summary>
        /// Delete a review based on the review ID passed.
        /// </summary>
        /// <param name="reviewID"></param>
        /// <returns></returns>
        [OperationContract]
        Result DeleteReview(string reviewID);
    }
}
