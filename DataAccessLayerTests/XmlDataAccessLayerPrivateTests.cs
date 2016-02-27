using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessLayer.Tests;
using Infrastructure.BusinessEntities;
using System.Configuration;
using System.Collections.Generic;
using Infrastructure.Interfaces;

namespace XmlDataAccessLayer.Tests
{
    [TestClass()]
    public class XmlDataAccessLayerPrivateTests : TestBase
    {
        [TestMethod()]
        public void serializeToDiskTest()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            Restaurant restaurant = new Restaurant();
            restaurant.RestaurantID = Guid.NewGuid().ToString();
            restaurant.Name = "Private Test Restaurant 1";
            restaurants.Add(restaurant);
            restaurant = null;
            restaurant = new Restaurant();
            restaurant.RestaurantID = Guid.NewGuid().ToString();
            restaurant.Name = "Private Restaurant 2";
            restaurants.Add(restaurant);

            string filePath = ConfigurationManager.AppSettings["RestaurantsXMLFilePath"];
            PrivateObject objToBeTested = new PrivateObject(typeof(DataAccessLayer));
            Result result = (Result)objToBeTested.Invoke("serializeToDisk", new Type[] { typeof(List<Restaurant>), typeof(string) }, new object[] { restaurants, filePath }, new Type[] { typeof(List<Restaurant>) });
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNull(result.Message);
        }

        [TestMethod()]
        public void deserializeFromDiskTest()
        {
            string filePath = ConfigurationManager.AppSettings["RestaurantsXMLFilePath"];
            PrivateObject objToBeTested = new PrivateObject(typeof(DataAccessLayer));
            List<Restaurant> restaurants = (List<Restaurant>)objToBeTested.Invoke("deserializeFromDisk", new Type[] { typeof(string) }, new object[] { filePath }, new Type[] { typeof(List<Restaurant>) });
            Assert.AreEqual("Restaurant 1", restaurants[0].Name);
            Assert.AreEqual("Restaurant 2", restaurants[1].Name);
        }

        [TestMethod()]
        public void doesRestaurantExistTest()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            Restaurant restaurant = new Restaurant();
            restaurant.Name = "Restaurant 1";
            restaurants.Add(restaurant);
            IRestaurant existingRestaurant = new Restaurant();
            existingRestaurant.Name = "Restaurant 1";
            IRestaurant newRestaurant = new Restaurant();
            newRestaurant.Name = "Restaurant 3";
            PrivateObject objToBeTested = new PrivateObject(typeof(DataAccessLayer));
            bool result = (bool)objToBeTested.Invoke("doesRestaurantExist", existingRestaurant, restaurants);
            Assert.IsTrue(result);

            result = (bool)objToBeTested.Invoke("doesRestaurantExist", newRestaurant, restaurants);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void isRestaurantIDValidTest()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            Restaurant restaurant = new Restaurant();
            restaurant.RestaurantID = "12345";
            restaurant.Name = "Restaurant 1";
            restaurants.Add(restaurant);

            restaurant = new Restaurant();
            restaurant.RestaurantID = "23456";
            restaurant.Name = "Restaurant 2";
            restaurants.Add(restaurant);

            PrivateObject objToBeTested = new PrivateObject(typeof(DataAccessLayer));
            bool result = (bool)objToBeTested.Invoke("isRestaurantIDValid", "12345", restaurants.ToArray());
            Assert.IsTrue(result);

            result = (bool)objToBeTested.Invoke("isRestaurantIDValid", "456789", restaurants.ToArray());
            Assert.IsFalse(result);
        }
    }
}
