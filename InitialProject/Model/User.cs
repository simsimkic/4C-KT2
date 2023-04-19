using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType UserType { get; set; }

        public List<AccomodationReservation> AccomodationReservations { get; set; }

        public User() {
            AccomodationReservations = new List<AccomodationReservation>();
        }


        public User(string username, string password, UserType userType)
        {
            Username = username;
            Password = password;
            UserType = userType;
        }
    }
}
