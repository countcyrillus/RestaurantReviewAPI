namespace Infrastructure.Interfaces
{
    public interface IReview
    {
        string RestaurantID { get; set; }

        string ReviewID { get; set; }

        string Reviewer { get; set; }

        string ReviewedOn { get; set; }

        string Comment { get; set; }

        string Rating { get; set; }
    }
}
