using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace InitialProject.Service
{
    public class AccomodationService
    {
        public AccomodationService()
        {

        }

        AccomodationRepository accomodationRepository = new AccomodationRepository();
        LocationRepository locationRepository = new LocationRepository();

        public void RegisterAccomodation(Accomodation accomodation, Location location, List<AccomodationImage> accomodationImages)
        {

            using (var context = new DataContext())

            AccomodationRepository accomodationRepository = new AccomodationRepository();
            return accomodationRepository.GetAllAccomodations();
        }

        public List<Accomodation> GetAccomodationsByLocation(int locationId)
        {
            AccomodationRepository accomodationRepository = new AccomodationRepository();
            List<Accomodation> accomodationsByLocation = accomodationRepository.GetAccomodationsByLocation(locationId);
            return accomodationsByLocation;
        }

        public Accomodation GetAccomodationById(int accId)
        {
            AccomodationRepository accomodationRepository = new AccomodationRepository();
            return accomodationRepository.GetAccomodationById(accId);
        }

        public List<Accomodation> GetByType(string type)
        {
            AccomodationRepository accomodationRepository = new AccomodationRepository();
            List<Accomodation> allAccomodations = accomodationRepository.GetAllAccomodations();
            List<Accomodation> accomodationsByType = new List<Accomodation>();
            foreach (Accomodation accomodation in allAccomodations)

            {
                Location existingLocation = locationRepository.GetByCityAndCountry(location.City, location.Country);

                if (existingLocation != null)
                {
                    location = existingLocation;
                    location.Accomodations.Add(accomodation);
                    context.Locations.Update(location);
                }
                else
                {
                    location.Accomodations.Add(accomodation);
                    context.Locations.Add(location);
                }

                foreach (var image in accomodationImages)
                {
                    context.AccomodationImages.Add(image);
                    accomodation.Images.Add(image);
                }
                context.SaveChanges();

            }

        }

        public void UpdateClassBy(bool superOwner)
        {
            string accomodationClass = "B";

            if(superOwner)
            {
                accomodationClass = "A";
            }


            this.accomodationRepository.UpdateClassBy(accomodationClass);
        }

        public void UpdateAvailability(int accId,DateTime start,DateTime end,int numberOfGuests)
        {   using (var db = new DataContext())
            {
                var accomodation = GetAccomodationById(accId);
                if (accomodation == null)
                {
                    Console.WriteLine("Accomodation not found.");
                    return;
                }
                if (accomodation.MaxGuests < numberOfGuests)
                {
                    Console.WriteLine("Not enough spots available for the given number of guests");
                    return;
                }

                accomodation.MaxGuests = numberOfGuests;
                db.SaveChanges();
            }
        }


        public List<Accomodation> FindAvailableAccomodations(DateTime startDate, DateTime endDate, int numberOfGuests)
        {
            AccomodationService accomodationService = new AccomodationService();
            List<Accomodation> availableAccomodations = new List<Accomodation>();
            foreach (Accomodation accomodation in accomodationService.GetAllAccomodations())
            {
                if ((endDate - startDate).Days + 1 > accomodation.MinReservationDays)
                {
                    if(GetFreeSpotsNumber(accomodation.AccId) >= numberOfGuests) //&& accomodation.IsAvailable
                    {
                        availableAccomodations.Add(accomodation);
                    }
                }
            }
            return availableAccomodations;
        }

        public List<Guest> GetGuests(int accId)
        {
            GuestsRepository guestsRepository = new GuestsRepository();
            return guestsRepository.GetGuests(accId);
        }

        public int GetFreeSpotsNumber(int accId)
        {
            AccomodationRepository accomodationRepository = new AccomodationRepository();
            Accomodation accomodation = GetAccomodationById(accId);
            List<Guest> accomodationsGuests = new List<Guest>();
            accomodationsGuests = GetGuests(accId);
            int freeSpotsNumber = accomodation.MaxGuests - accomodationRepository.GetNumberOfGuestsInAccomodation(accId);
            return freeSpotsNumber;
        }

        public void BookAcc(int accId,int guestsId,int guestsNumber,DateTime start,DateTime end)
        {
            AccomodationReservationRepository accomodationReservationRepository = new AccomodationReservationRepository();
            accomodationReservationRepository.BookAcc(accId, guestsId, guestsNumber,start,end);
        }

        
        public void CancelReservation(int accId)//accRes po ID, ne uklapa se uvek
        {
            AccomodationRepository accomodationRepository = new AccomodationRepository();
            AccomodationReservationRepository accomodationReservationRepository = new AccomodationReservationRepository();

            Accomodation accomodation = accomodationRepository.GetAccomodationById(accId);
            AccomodationReservation accomodationReservation = accomodationReservationRepository.GetAccomodationReservationById(accId);

            using(var db = new DataContext()) {

                if (accomodation.DaysBeforeCanceling > (accomodationReservation.CheckInDate - DateTime.Now).TotalDays)
                {
                    Console.WriteLine("it is impossible to cancel the reservation because the owner has given several days before cancellation");
                    return;
                }
                else if (accomodationReservation.CheckInDate - DateTime.Now < new TimeSpan(24, 0, 0))
                {
                    Console.WriteLine("it is impossible to cancel because there are less than 24 hours until the start of the reservation");
                    return;
                }

                else
                {


                    db.AccomodationReservations.Remove(accomodationReservation);
                    Console.WriteLine("Succesfully removed accomodation reservation!");
                    db.SaveChanges();
                    return;
            }
            }

        }
        


    }
}
