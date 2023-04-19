using InitialProject.DTO;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace InitialProject.Repository
{
    public class TourRepository
    {

        TourReservationRepository tourReservationRepository = new TourReservationRepository();

        public TourRepository() { }

        public void Add(Tour tourToAdd)
        {
            using (var db = new DataContext())
            {
                db.Tours.Add(tourToAdd);
                db.SaveChanges();
            }
        }

        public List<Tour> GetAll()
        {
            using (var db = new DataContext())
            {

                return db.Tours.ToList();
            }
        }

        public List<Tour> GetByLocation(int locationId)
        {
            List<Tour> toursByLocation = new List<Tour>();
            using (var db = new DataContext())
            {
                var location = db.Locations.Include(t => t.Tours).SingleOrDefault(t => t.LocationId == locationId);
                if (location != null)
                {
                    toursByLocation.AddRange(location.Tours);
                }
            }
            return toursByLocation;
        }

        public int GetTouristsNumber(int tourId)
        {
            using (var db = new DataContext())
            {
                var tour = db.Tours.Include(t => t.TourReservations).SingleOrDefault(t => t.TourId == tourId);

                int tourists = 0;
                List<TourReservation> tourReservations = tour.TourReservations.ToList();
                foreach (TourReservation tourReservation in tourReservations)
                {
                    tourists += tourReservation.TouristsNumber;
                }
                return tourists;
            }
        }

        public void BookTour(int tourId, int touristId, int tourists)
        {
            TourReservation tourReservation = new TourReservation(tourists);
            using (var db = new DataContext())
            {
                Tourist tourist = db.Tourists.Find(touristId);
                Tour tour = db.Tours.Find(tourId);
                if (tourist != null && tour != null)
                {
                    tourist.TourReservations.Add(tourReservation);
                    tour.TourReservations.Add(tourReservation);
                    tour.Tourists.Add(tourist);
                    db.SaveChanges();
                }

            }
            Console.WriteLine("Uspesno ste rezervisali turu.");
        }

        public Tour GetById(int id)
        {
            using (var db = new DataContext())
            {
                return db.Tours.FirstOrDefault(t => t.TourId == id);
            }
        }

        /*public Tour GetByName(string tourName)
        {
            using (var db = new DataContext())
            {
                return db.Tours.FirstOrDefault(t => t.Name == tourName);
            }
        }*/

        public Tour GetByName(string name)
        {
            using (var db = new DataContext())
            {
                return db.Tours.Include(t => t.StartingDates).Where(t => t.Name == name).FirstOrDefault();
            }
        }

        public void Update(int id, Tour updatedTour)
        {
            using (var context = new DataContext())
            {
                var tourToUpdate = context.Tours.FirstOrDefault(t => t.TourId == id);
                if (tourToUpdate != null)
                {
                    tourToUpdate.Name = updatedTour.Name;
                    tourToUpdate.Description = updatedTour.Description;
                    tourToUpdate.Language = updatedTour.Language;
                    tourToUpdate.MaxGuests = updatedTour.MaxGuests;
                    tourToUpdate.Duration = updatedTour.Duration;
                    tourToUpdate.Checkpoints = updatedTour.Checkpoints;
                    tourToUpdate.Images = updatedTour.Images;
                    tourToUpdate.Tourists = updatedTour.Tourists;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var db = new DataContext())
            {
                var tourToDelete = db.Tours.FirstOrDefault(t => t.TourId == id);

                if (tourToDelete != null)
                {
                    db.Tours.Remove(tourToDelete);
                    db.SaveChanges();
                }
            }
        }

        public List<Tour> GetByStartDate(DateTime startingDate)
        {
            List<Tour> todaysTour = new List<Tour>();
            using (var db = new DataContext())
            {
                List<Tour> tours = db.Tours.Include(t => t.Checkpoints).Include(t => t.Tourists).Include(t => t.Images).Include(t => t.StartingDates).ToList();
                foreach (var tour in tours)
                {
                    todaysTour = db.Tours.Include(t => t.Checkpoints).Include(t => t.Tourists).Include(t => t.Images).Include(t => t.StartingDates).
                        Where(t => t.StartingDates.Any(d => d.Date.Date == startingDate.Date)).ToList();
                }

            }
            return todaysTour;
        }

        public List<Tour> GetList()
        {
            List<Tour> toursWithCheckpoints = new List<Tour>();
            using (var db = new DataContext())
            {
                toursWithCheckpoints = db.Tours.Include(t => t.Checkpoints).Include(t => t.Tourists).Include(t => t.Images).ToList();
            }
            return toursWithCheckpoints;
        }

        public List<Tourist> GetTourists(Tour tour)
        {
            using (var db = new DataContext())
            {
                {

                    var tourToReturn = db.Tours.Include(t => t.Tourists).FirstOrDefault(t => t.TourId == tour.TourId);


                    if (tour == null)
                    {
                        return new List<Tourist>();
                    }


                    return tour.Tourists;
                }
            }
        }

        public Tour GetByTourReservation(int tourReservationId)
        {
            using (var db = new DataContext())
            {
                List<Tour> tours = db.Tours.Include(t => t.TourReservations).ToList();

                foreach (Tour tour in tours)
                {
                    foreach (TourReservation tourReservation in tour.TourReservations)
                    {
                        if (tourReservation.TourReservationId == tourReservationId)
                        {
                            return tour;
                        }
                    }
                }
                return null;
            }
        }
    }
}