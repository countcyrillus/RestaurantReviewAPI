using Infrastructure.Interfaces;
using System;
using Infrastructure.BusinessEntities;
using System.Linq;

namespace EFDataAccessLayer
{
    /// <summary>
    /// This implementation of the DataAccessLayer uses EntityFramework to store
    /// and retrieve data.
    /// </summary>
    public class DataAccessLayer : IDataAccessLayer
    {
        public Result AddRestaurant(IRestaurant restaurant)
        {
            Result result = new Result() { IsSuccessful = true };
            try
            {
                using (var ctx = new RestaurantReviewContext())
                {
                    Restaurant res = restaurant as Restaurant;
                    res.RestaurantID = Guid.NewGuid().ToString();

                    ctx.Restaurants.Add(res);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.Message = e.Message;
            }

            return result;
        }

        public Result AddReview(IReview review)
        {
            Result result = new Result() { IsSuccessful = true };
            try
            {
                using (var ctx = new RestaurantReviewContext())
                {
                    Review rev = review as Review;
                    rev.ReviewID = Guid.NewGuid().ToString();

                    ctx.Reviews.Add(rev);
                    ctx.SaveChanges();
                }
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
            Result result = new Result() { IsSuccessful = true };
            try
            {
                using (var ctx = new RestaurantReviewContext())
                {
                    Review rev = new Review();
                    rev.ReviewID = reviewID;

                    ctx.Reviews.Attach(rev);
                    ctx.Reviews.Remove(rev);
                    ctx.SaveChanges();
                }
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
            IRestaurant[] restaurants = new IRestaurant[0];
            try
            {
                using (var ctx = new RestaurantReviewContext())
                {
                    if (string.IsNullOrWhiteSpace(cityName))
                    {
                        restaurants = ctx.Restaurants.ToArray();
                    }
                    else
                    {
                        var restaurantList = ctx.Restaurants.Where(res => cityName.Equals(res.City, StringComparison.OrdinalIgnoreCase));
                        if (restaurantList != null)
                        {
                            restaurants = restaurantList.ToArray();
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            return restaurants;
        }

        public IReview[] GetReviewsByUser(string userName)
        {
            IReview[] reviews = new IReview[0];
            if (string.IsNullOrWhiteSpace(userName))
            {
                return reviews;
            }
            try
            {
                using (var ctx = new RestaurantReviewContext())
                {
                    var reviewsList = ctx.Reviews.Where(rev => userName.Equals(rev.Reviewer, StringComparison.OrdinalIgnoreCase));
                    if (reviewsList != null)
                    {
                        reviews = reviewsList.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
            }
            return reviews;
        }
    }
}
