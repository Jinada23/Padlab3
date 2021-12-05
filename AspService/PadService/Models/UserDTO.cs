using System;

namespace PadService.Models
{
    public class UserDTO : MongoDocument
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string position { get; set; }
        public string phoneNumber { get; set; }
        public string department { get; set; }
        public DateTime created { get; set; }
        public DateTime dateOfBirth { get; set; }
        public Guid mentorId { get; set; }

    }
}
