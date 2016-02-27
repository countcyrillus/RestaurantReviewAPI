using Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Infrastructure.BusinessEntities
{
    [Table("Restaurants")]
    [DataContract]
    public class Restaurant : IRestaurant
    {
        private string _restaurantId;
        [Key]
        [DataMember]
        public string RestaurantID
        {
            get { return _restaurantId; }
            set { _restaurantId = value; }
        }

        private string _name;
        [MaxLength(100)]
        [Index(IsUnique = true)]
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _addressLine1;
        [DataMember]
        public string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; }
        }

        private string _addressLine2;
        [DataMember]
        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; }
        }

        private string _city;
        [DataMember]
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _state;
        [DataMember]
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _zipCode;
        [DataMember]
        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }

        private string _phoneNumber;
        [DataMember]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }
    }
}
