using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebApi.Entities;

namespace InitialProject.Model
{
    public class Accomodation
    {
        [Key]
        public int AccId { get; set; }

        public string Name { get; set; }

        public AccomodationType AccomodationType { get; set; }

        public int MaxGuests { get; set; }

        public int MinReservationDays { get; set; }

        public int DaysBeforeCanceling { get; set; }

      //  public bool IsAvailable { get; set; }

        public List<AccomodationImage> Images { get; set; }

      
        public string Class { get; set; }


        public List<AccomodationReservation> AccomodationReservations { get; set; }

        public override string ToString()
        {
            return "Name" + Name + "AccomodationType" + AccomodationType + "MaxGuests" + MaxGuests + "MinReservationDays" + MinReservationDays + "DaysBeforeCacneling" + DaysBeforeCanceling + "Images" + Images + "Guests" + Guests + "AccReservations" + AccomodationReservations;
        }

        public Accomodation() 
        {
            Images = new List<AccomodationImage>(); 
             
            AccomodationReservations = new List<AccomodationReservation>(); 
        }

        public Accomodation(int accId, string name, Location location, AccomodationType type, int maxGuests, int minReservationDays, int daysBeforeCanceling)
        {
            AccId = accId;
            Name = name;
            AccomodationType = type;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            DaysBeforeCanceling = daysBeforeCanceling;
            Images = new List<AccomodationImage>();

            Guests = new List<Guest>();
            AccomodationReservations = new List<AccomodationReservation>();

            

            AccomodationReservations = new List<AccomodationReservation>();
        }

        public Accomodation(string name, Location location, AccomodationType type, int maxGuests, int minReservationDays, int daysBeforeCanceling)
        {

            Name = name;
            AccomodationType = type;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            DaysBeforeCanceling = daysBeforeCanceling;
            Images = new List<AccomodationImage>(); 
            
            AccomodationReservations = new List<AccomodationReservation>();
       }
    } 
}
