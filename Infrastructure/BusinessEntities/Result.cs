using System.Runtime.Serialization;

namespace Infrastructure.BusinessEntities
{
    [DataContract]
    public class Result
    {
        private bool _isSuccessful;
        [DataMember]
        public bool IsSuccessful
        {
            get { return _isSuccessful; }
            set { _isSuccessful = value; }
        }

        private string _message;
        [DataMember]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
