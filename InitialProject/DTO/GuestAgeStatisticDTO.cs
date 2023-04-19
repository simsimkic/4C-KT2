using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class GuestAgeStatisticDTO
    {   

        public int TourId { get; set; }
        public string Name { get; set; }

        public int Under18 { get; set; }

        public int Beetween18and50 { get; set; }

        public int Above50 { get; set; }

        public double WithCoupons { get; set; }

        public double WithoutCoupons { get; set; }
        public GuestAgeStatisticDTO() { }

        public GuestAgeStatisticDTO(int tourId, string name, int under18, int beetween18and50, int above50, double with, double without)
        {
            TourId = tourId;
            Name = name;
            Under18 = under18;
            Beetween18and50 = beetween18and50;
            Above50 = above50;
            WithCoupons = with;
            WithoutCoupons = without;
        }
    }
}
