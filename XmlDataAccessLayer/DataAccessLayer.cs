using Infrastructure.Interfaces;
using System;
using Infrastructure.BusinessEntities;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Configuration;
using System.IO;
using System.Linq;
using Infrastructure;

namespace XmlDataAccessLayer
{
    /// <summary>
    /// This implementation of the DataAccessLayer uses Xml Serialization to
    /// store and retrieve data on the disk in XML files.
    /// </summary>
    public class DataAccessLayer : IDataAccessLayer
    {
        public Result AddRestaurant(IRestaurant restaurant)
        {
            Result result = new Result() { IsSuccessful = true };
            string filePath = ConfigurationManager.AppSettings["RestaurantsXMLFilePath"];
            List<Restaurant> restaurants = new List<Restaurant>();

            //Get a list of existing restaurants from disk
            try
            {
                restaurants = deserializeFromDisk<List<Restaurant>>(filePath);
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.Message = e.Message;
            }

            //Check if the restaurant already exists and if so return
            //an error message.
            if (doesRestaurantExist((Restaurant)restaurant, restaurants))
            {
                result.IsSuccessful = false;
                result.Message = Constants.ErrorMessageRestaurantExists;
                return result;
            }

            //Generate new ID and add the restaurant to the list of
            //restaurants
            restaurant.RestaurantID = Guid.NewGuid().ToString();
            restaurants.Add((Restaurant)restaurant);

            //Save the list to disk
            result = serializeToDisk(restaurants, filePath);

            return result;
        }

        public Result AddReview(IReview review)
        {
            string filePath = ConfigurationManager.AppSettings["ReviewsXMLFilePath"];
            Result result = new Result() { IsSuccessful = true };
            List<Review> reviews = new List<Review>();

            //Get a list of existing restaurants from disk
            IRestaurant[] restaurants = GetRestaurantsByCity(string.Empty);

            //Check if the restaurant ID is valid
            if (!isRestaurantIDValid(review.RestaurantID, restaurants))
            {
                result.IsSuccessful = false;
                result.Message = Constants.ErrorMessageInvalidRestaurantID;
                return result;
            }

            //If the restaurant ID is valid, get a list of reviews from disk
            try
            {
                reviews = deserializeFromDisk<List<Review>>(filePath);
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.Message = e.Message;
            }

            //Generate new ID for the review
            review.ReviewID = Guid.NewGuid().ToString();
            reviews.Add((Review)review);

            //Save all reviews to disk
            try
            {
                result = serializeToDisk<List<Review>>(reviews, filePath);
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.Message = e.Message;
            }

            return result;
        }

        public Result DeleteReview(string reviewID)
        {
            if (string.IsNullOrWhiteSpace(reviewID))
            {
                return new Result() { IsSuccessful = false, Message = Constants.ErrorMessageInvalidReviewID };
            }

            string filePath = ConfigurationManager.AppSettings["ReviewsXMLFilePath"];
            Result result = new Result() { IsSuccessful = true };
            List<Review> reviews = new List<Review>();

            //Get a list of reviews from disk
            try
            {
                reviews = deserializeFromDisk<List<Review>>(filePath);
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.Message = e.Message;
            }

            var reviewToBeRemoved = reviews.Where(rev => reviewID.Equals(rev.ReviewID, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (reviewToBeRemoved == null)
            {
                result.IsSuccessful = false;
                result.Message = Constants.ErrorMessageInvalidReviewID;
                return result;
            }

            reviews.Remove(reviewToBeRemoved);

            //Save reviews to disk
            try
            {
                result = serializeToDisk<List<Review>>(reviews, filePath);
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.Message = e.Message;
            }

            return result;
        }

        public IRestaurant[] GetRestaurantsByCity(string cityName)
        {
            string filePath = ConfigurationManager.AppSettings["RestaurantsXMLFilePath"];
            List<Restaurant> restaurants = new List<Restaurant>();

            //Get a list of existing restaurants from disk
            try
            {
                restaurants = deserializeFromDisk<List<Restaurant>>(filePath);
            }
            catch (Exception e)
            {

            }

            if (string.IsNullOrWhiteSpace(cityName))
            {
                return restaurants.ToArray();
            }

            var restaurantsByCity = restaurants.Where(res => cityName.Equals(res.City, StringComparison.OrdinalIgnoreCase));
            if (restaurantsByCity != null)
            {
                restaurants = restaurantsByCity.ToList();
            }

            return restaurants.ToArray();
        }

        public IReview[] GetReviewsByUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return new List<Review>().ToArray();
            }

            string filePath = ConfigurationManager.AppSettings["ReviewsXMLFilePath"];
            Result result = new Result() { IsSuccessful = true };
            List<Review> reviews = new List<Review>();

            //Get a list of reviews from disk
            try
            {
                reviews = deserializeFromDisk<List<Review>>(filePath);
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.Message = e.Message;
            }

            //Search for the reviews based on the user
            var reviewsByUser = reviews.Where(rev => userName.Equals(rev.Reviewer, StringComparison.OrdinalIgnoreCase));
            if (reviewsByUser != null)
            {
                reviews = reviewsByUser.ToList();
            }

            return reviews.ToArray();
        }

        private Result serializeToDisk<T>(T obj, string filePath)
        {
            Result result = new Result() { IsSuccessful = true };

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fileStream, obj);
                }
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.Message = e.Message;
            }

            return result;
        }

        private T deserializeFromDisk<T>(string filePath)
        {
            T obj = default(T);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    obj = (T)serializer.Deserialize(fileStream);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return obj;
        }

        private bool doesRestaurantExist(Restaurant restaurant, List<Restaurant> restaurants)
        {
            Restaurant existingrestaurant = restaurants.Where(res => string.Equals(res.Name, restaurant.Name, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (existingrestaurant != null)
            {
                return true;
            }
            return false;
        }

        private bool isRestaurantIDValid(string restaurantID, IRestaurant[] restaurants)
        {
            if (string.IsNullOrWhiteSpace(restaurantID))
            {
                return false;
            }
            var validRestaurants = restaurants.Where(res => restaurantID.Equals(res.RestaurantID)).SingleOrDefault();
            if (validRestaurants != null)
            {
                return true;
            }
            return false;
        }
    }
}
