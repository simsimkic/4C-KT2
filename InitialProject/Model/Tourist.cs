
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Tourist : User
    {


        public bool IsPresent { get; set; }

        public int Age { get; set; }

        public List<TourReservation> TourReservations { get; set; }
        public List<Coupon> Coupons { get; set; }

        public List<TourRating> TourRatings { get; set; }

        public List<TouristNotifications> TouristNotifications { get; set; }

        public Tourist(string username, string password, UserType userType) : base(username, password, userType)
        {
            TourReservations = new List<TourReservation>();
            Coupons = new List<Coupon>();
            TourRatings = new List<TourRating>();
            TouristNotifications = new List<TouristNotifications>();
        }

        public Tourist(string username, string password, bool isPresent, UserType userType = UserType.Tourist) : base(username, password, userType)
        {
            IsPresent = isPresent;
            TourReservations = new List<TourReservation>();
            Coupons = new List<Coupon>();
            TourRatings = new List<TourRating>();
            TouristNotifications = new List<TouristNotifications>();

        }

        public Tourist(string username, string password, int age, UserType userType = UserType.Tourist) : base(username, password, userType)
        {
            Age = age;
            TourReservations = new List<TourReservation>();
            Coupons = new List<Coupon>();
            TourRatings = new List<TourRating>();
            TouristNotifications = new List<TouristNotifications>();

        }
        public override string ToString()
        {
            return $"Id: {Id}\n, Username: {Username}\n, Password: {Password}\n, UserType: {UserType}\n, IsPresent: {IsPresent}\n";
        }
    }
}
