using Infrastructure.BusinessEntities;
using System.Data.Entity;

namespace EFDataAccessLayer
{
    public class RestaurantReviewContext : DbContext
    {
        public RestaurantReviewContext() : base("RestaurantDBConnectionString")
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
