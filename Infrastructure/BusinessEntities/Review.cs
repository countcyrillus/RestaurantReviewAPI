using Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Infrastructure.BusinessEntities
{
    [Table("Reviews")]
    [DataContract]
    public class Review : IReview
    {
        private string _restaurantID;
        [DataMember]
        public string RestaurantID
        {
            get { return _restaurantID; }
            set { _restaurantID = value; }
        }

        private string _reviewId;
        [Key]
        [DataMember]
        public string ReviewID
        {
            get { return _reviewId; }
            set { _reviewId = value; }
        }

        private string _reviewer;
        [DataMember]
        public string Reviewer
        {
            get { return _reviewer; }
            set { _reviewer = value; }
        }

        private string _reviewedOn;
        [DataMember]
        public string ReviewedOn
        {
            get { return _reviewedOn; }
            set { _reviewedOn = value; }
        }

        private string _comment;
        [DataMember]
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        private string _rating;
        [DataMember]
        public string Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }

        [ForeignKey("RestaurantID")]
        public Restaurant Restaurant { get; set; }
    }
}
