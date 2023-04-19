using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using InitialProject.Service;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace InitialProject.Repository
{
    public class AccomodationReservationRepository
    {
        public AccomodationReservationRepository() { }



        public List<AccomodationReservation> GetAllExpiredBy(int day, int month, int year)
        {
            List<AccomodationReservation> expiredReservations = new List<AccomodationReservation>();

            using(var db = new DataContext())
            {
                expiredReservations = db.AccomodationReservations.Include(ar => ar.Accomodations) 
                                                                 .Include(ar => ar.User)
                                        .Where(ar => ((ar.CheckOutDate.Day >= (day - 5)) && (ar.CheckOutDate.Day <= day)) 
                                                   && (ar.CheckOutDate.Month == month) 
                                                   && (ar.CheckOutDate.Year == year)
                                              ).ToList();
            } 
            return expiredReservations;
        }

        public List<AccomodationReservation> GetAllBetween(DateTime startingDate, DateTime endingDate)
        {

            List<AccomodationReservation> accommodationReservations = new List<AccomodationReservation>();

            using (var dbContext = new DataContext())
            {
                accommodationReservations = dbContext.AccomodationReservations
                                            .Where(ar => ((startingDate >= ar.CheckInDate && startingDate <= ar.CheckOutDate) || (endingDate >= ar.CheckInDate && endingDate <= ar.CheckOutDate)) ||
                                                         (ar.CheckInDate >= startingDate && ar.CheckInDate <= endingDate) || (ar.CheckOutDate >= startingDate && (ar.CheckOutDate <= endingDate))
                                                   )
                                           .Include(r => r.Accomodations)    
                                           .Include(r => r.User)
                                           .ToList();
            }
            return accommodationReservations;
        }

        public void UpdateScheduledDatesBy(int id, DateTime newBegginingDate, DateTime newEndingDate)
        {

            AccomodationReservation accommodationReservation = new();

            var db = new DataContext();
            accommodationReservation = db.AccomodationReservations.Find(id);

            accommodationReservation.CheckInDate = newBegginingDate;
            accommodationReservation.CheckOutDate = newEndingDate;
            db.SaveChanges();
        }

        public List<AccomodationReservation> GetAllAccomodationReservation()
        {
            using(var db = new DataContext())
            {
                return db.AccomodationReservations.ToList();
            }
        }

        public AccomodationReservation GetAccomodationReservationById(int accId)
        {
            using (var db = new DataContext())
            {
                List<AccomodationReservation> allAccomodationReservation = GetAllAccomodationReservation();
                foreach (AccomodationReservation accomodationReservation in allAccomodationReservation)
                {
                    if (accomodationReservation.Id == accId)
                    {
                        return accomodationReservation;
                    }
                }
            }
            return null;
        }


        public void BookAcc(int accId,int guestId,int guestsNumber, DateTime start,DateTime end)
        {
            AccomodationReservation accomodationReservation = new AccomodationReservation();
            AccomodationRepository accomodationRepository = new AccomodationRepository();
            AccomodationService accomodationService = new AccomodationService();

            using(var db = new DataContext())
            {
                Guest guest = db.Guests.Find(guestId);
                Accomodation accomodation = db.Accomodations.Find(accId);
                if(guest != null && accomodation != null)
                {
                    guest.AccomodationReservations.Add(accomodationReservation);
                    accomodation.AccomodationReservations.Add(accomodationReservation);
                    accomodation.Guests.Add(guest);
                    db.SaveChanges();
                }
            }
            Console.WriteLine("Succesfully reserved accomodation");
        } 


    }
}
