using Infrastructure;
using Infrastructure.BusinessEntities;
using Infrastructure.Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessLayer.Tests
{
    [TestClass()]
    public class DataAccessLayerTests : TestBase
    {
        [TestMethod()]
        public void AddReviewTest()
        {
            IReview review = _container.Resolve<IReview>();
            review.RestaurantID = "cb3174cb-d626-499f-b14c-9a87af082c37";
            review.Reviewer = "countcyrillus";
            review.Rating = "4";
            review.Comment = "This is a test";

            Result result = _dataAccessLayer.AddReview(review);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNull(result.Message);

            review.RestaurantID = "146bb41b-a648-41bd-8999-b0b94ed2bfc";
            result = _dataAccessLayer.AddReview(review);
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNotNull(result.Message);
        }

        [TestMethod()]
        public void AddRestaurantTest()
        {
            IRestaurant existingRestaurant = _container.Resolve<IRestaurant>();
            existingRestaurant.Name = "Test Restaurant 1";
            Result result = _dataAccessLayer.AddRestaurant(existingRestaurant);
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNotNull(result.Message);

            IRestaurant newRestaurant = _container.Resolve<IRestaurant>();
            newRestaurant.Name = "Test Restaurant 3";
            newRestaurant.City = "Harrisburg";
            result = _dataAccessLayer.AddRestaurant(newRestaurant);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNull(result.Message);
        }

        [TestMethod()]
        public void DeleteReviewTest()
        {
            Result result = _dataAccessLayer.DeleteReview("ff96e931-a4df-465a-b0fb-433fd61a02c0");
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNull(result.Message);
        }

        [TestMethod()]
        public void GetRestaurantsByCityTest()
        {
            IRestaurant[] restaurants = _dataAccessLayer.GetRestaurantsByCity(string.Empty);
            Assert.AreEqual(2, restaurants.Length);

            restaurants = _dataAccessLayer.GetRestaurantsByCity("Harrisburg");
            Assert.AreEqual(1, restaurants.Length);
        }

        [TestMethod()]
        public void GetReviewsByUserTest()
        {
            IReview[] reviews = _dataAccessLayer.GetReviewsByUser("countcyrillus");
            Assert.AreEqual(2, reviews.Length);

            reviews = _dataAccessLayer.GetReviewsByUser("nonExistantUser");
            Assert.AreEqual(0, reviews.Length);
        }
    }
}