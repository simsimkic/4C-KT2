using System.IO.Packaging;
﻿using InitialProject.Controller;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebApi.Entities;

namespace InitialProject.Service
{
    public class TourService
    {

        public LocationRepository locationRepository = new LocationRepository();
        public TourRepository tourRepository = new TourRepository();

        public TourService() {

        }

        public List<Tour> GetAll()
        {
            return tourRepository.GetAll();
        }

        public Tour GetById(int tourId)
        {
            return tourRepository.GetById(tourId);
        }

        public Tour GetByName(string tourName)
        {
            return tourRepository.GetByName(tourName);
        }

        public List<Tour> GetByLocation(int locationId)
        {
            return tourRepository.GetByLocation(locationId);
        }

        public List<Tour> GetAllByDuration(int duration)
        {
            List<Tour> allTours = tourRepository.GetAll();
            List<Tour> toursByDuration = new List<Tour>();

            foreach (Tour tour in allTours)
            {
                if (tour.Duration == duration)
                {
                    toursByDuration.Add(tour);
                }
            }

            return toursByDuration;
        }

        public List<Tour> GetAllByLanguage(string language)
        {
            List<Tour> allTours = tourRepository.GetAll();
            List<Tour> toursByLanguage = new List<Tour>();

            foreach (Tour tour in allTours)
            {
                if (tour.Language.Equals(language))
                {
                    toursByLanguage.Add(tour);
                }
            }

            return toursByLanguage;
        }
        public List<Tour> GetAllByTouristsNumber(int tourists)
        {
            List<Tour> allTours = tourRepository.GetAll();
            List<Tour> toursByTouristsNumber = new List<Tour>();

            foreach (Tour tour in allTours)
            {
                if (tour.MaxGuests >= tourists)
                {
                    toursByTouristsNumber.Add(tour);
                }
            }

            return toursByTouristsNumber;
        }

        public Location GetTourLocation(int tourId)
        {
            List<Location> allLocations = locationRepository.GetAllLocations();
            List<Tour> toursByLocation = new List<Tour>();

            foreach (Location location in allLocations)
            {
                toursByLocation = GetByLocation(location.LocationId);
                foreach (Tour tour in toursByLocation)
                {
                    if (tour.TourId == tourId)
                    {
                        return location;
                    }
                }
            }
            return null;

        }

        public List<Tourist> GetTourists(int tourId)
        {
            TouristsRepository touristsRepository = new TouristsRepository();
            return touristsRepository.GetTourists(tourId);
        }

        public int GetFreeSpotsNumber(int tourId)
        {
            Tour tour = GetById(tourId);
            List<Tourist> tourTourists = GetTourists(tourId);
            int freeSpotsNumber = tour.MaxGuests - tourRepository.GetTouristsNumber(tourId);
            return freeSpotsNumber;
        }

        public void BookTour(int tourId, int touristId, int tourists)
        { 
            tourRepository.BookTour(tourId, touristId, tourists);
        }

        public void MakeTour(Tour tour, Location location, List<TourImages> tourImages, List<Checkpoint> checkpoints, List<Dates> dates)
        {

            using (var context = new DataContext())
            {

                Location existingLocation = locationRepository.GetByCityAndCountry(location.City, location.Country);



                if (existingLocation != null)
                {
                    location = existingLocation;
                    location.Tours.Add(tour);
                    context.Locations.Update(location);
                }
                else
                {
                    location.Tours.Add(tour);
                    context.Locations.Add(location);
                }

                foreach (var image in tourImages)
                {
                    context.TourImages.Add(image);
                    tour.Images.Add(image);
                }

                foreach (var checkpoint in checkpoints)
                {
                    context.Checkpoints.Add(checkpoint);
                    tour.Checkpoints.Add(checkpoint);
                }

                foreach(var date in dates)
                {
                    context.Dates.Add(date);
                    tour.StartingDates.Add(date);
                }
                

                context.SaveChanges();
            }
        }

        public Tour GetByTourReservation(int tourReservationId)
        {
            return tourRepository.GetByTourReservation(tourReservationId);
        }

        public bool IsTourActive(Tour tour)
        {
            CheckpointService checkpointService = new CheckpointService();
            List<Checkpoint> checkpoints = checkpointService.GetTourCheckpoints(tour.TourId);
            int falseCheckpoints = CountFalseCheckpoints(tour);
            int markedCheckpoints = CountMarkedCheckpoints(tour);
            if (checkpoints.Count == falseCheckpoints || checkpoints.Count == markedCheckpoints)
            {
                return false;
            }
            return true;
        }


        public void TourTracking()
        {

            using (var context = new DataContext())
            {
                TourRepository tourRepository = new TourRepository();
                DateTime todayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0 , 0 , 0);
                Console.WriteLine("Danasnji datum: " + todayDate);
                List<Tour> todaysTours = tourRepository.GetByStartDate(todayDate);
                UnmarkCheckpoints(todaysTours);
                string trackingTourName = FindTodaysToursName();

                Tour trackingTour = new Tour();

                trackingTour = startTrackingTour(trackingTourName, todaysTours);
                if (trackingTour == null)
                {
                    Console.WriteLine("Tura sa tim imenom ne postoji ili nije na danasnjem programu");
                    return;
                }
                context.SaveChanges();
                ShowCheckpoints(trackingTour);

                Menu(trackingTour);


                context.SaveChanges();
            }

        }

        private static void WriteMenuOptions()
        {
            Console.WriteLine("1.Oznaci Cekpoint\n");
            Console.WriteLine("2.Oznaci prisutne turiste\n");
            Console.WriteLine("3.Zavrsi turu\n");
            Console.WriteLine("x. exit");
            Console.Write("Your option: ");
        }

        private bool ProcessChosenOption(string chosenOption, Tour trackingTour)
        {
            Checkpoint currentCheckpoint = new Checkpoint();
            currentCheckpoint = FindStartCheckpoint(trackingTour);
            bool end = false;

            switch (chosenOption)
            {
                case "1":
                    Console.WriteLine("Izabrali ste oznacavanje Cekpointa");
                    currentCheckpoint = MarkingCheckpoints(trackingTour);
                    if (currentCheckpoint.Type == CheckpointType.End)
                    {
                        Console.WriteLine("Krajnji cekpoint je oznacen, tura je gotova");
                        end = true;
                    }
                    break;
                case "2":
                    Console.WriteLine("Izabrali ste da oznacite prisutne");
                    MarkingPresentTourists(currentCheckpoint, trackingTour);
                    break;
                case "3":
                    Console.WriteLine("Izabrali ste da zavrsite turu");
                    end = EndTour(trackingTour);

                    break;
                case "x":
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }

            return end;
        }

        public string FindTodaysToursName()
        {
            // Console.Clear();
            Console.WriteLine("Unesite ime ture koju zelite da zapocnete: ");
            string trackingTourName = Console.ReadLine();

            return trackingTourName;
        }

        public Tour startTrackingTour(string trackingTourName, List<Tour> todaysTours)
        {
            Tour trackingTour = new Tour();
            trackingTour.Name = "ne postoji";
            using (var context = new DataContext())
            {
                foreach (var tour in todaysTours)
                {
                    if (tour.Name == trackingTourName)
                    {
                        trackingTour = tour;
                        break;
                    }
                }

                if (trackingTour.Name == "ne postoji") return null;

                // Pronađi prvi checkpoint sa statusom false i ažuriraj ga
                foreach (var checkpoint in trackingTour.Checkpoints)
                {
                    if (checkpoint.Type == 0)
                    {
                        checkpoint.Status = true;
                        context.Checkpoints.Update(checkpoint);
                        context.SaveChanges();
                    }
                }

            }

            Console.WriteLine($"Tura {trackingTour.Name} je zapoceta");
            Console.WriteLine("-------------------------");

            return trackingTour;
        }

        public void ShowCheckpoints(Tour trackingTour)
        {
            foreach (var checkpoint in trackingTour.Checkpoints)
            {

                Console.WriteLine("CheckpointId: " + checkpoint.CheckpointId);
                Console.WriteLine("CheckpointName: " + checkpoint.Name);
                Console.WriteLine("CheckpointType: " + checkpoint.Type);
                Console.WriteLine("CheckpointStatus: " + checkpoint.Status + "\n");
                Console.WriteLine("-------------------------");
            }
        }

        public void Menu(Tour trackingTour)
        {
            bool end = false;
            string chosenOption;
            Console.WriteLine("Meni za dalje instrukcije: ");
            Console.WriteLine("-------------------------");
            do
            {
                if (end == true) break;
                WriteMenuOptions();
                chosenOption = Console.ReadLine();
                Console.WriteLine("-------------------------");
                Console.Clear();
                end = ProcessChosenOption(chosenOption, trackingTour);

            } while (!chosenOption.Equals("x") || end != true);


        }

        public void ShowCheckpointsList(List<Checkpoint> checkpoints)
        {
            foreach (var checkpoint in checkpoints)
            {

                Console.WriteLine("CheckpointId: " + checkpoint.CheckpointId);
                Console.WriteLine("CheckpointName: " + checkpoint.Name);
                Console.WriteLine("CheckpointType: " + checkpoint.Type);
                Console.WriteLine("CheckpointStatus: " + checkpoint.Status + "\n");
                Console.WriteLine("-------------------------");
            }
        }

        public Checkpoint MarkingCheckpoints(Tour trackingTour)
        {

            List<Checkpoint> falseCheckpoints = ListFalseCheckpoints(trackingTour);


            if (falseCheckpoints.Count == 0)
            {
                Console.WriteLine("Svi Cekpointi u turi su obidjeni\ntura je gotova");
                return null;
            }

            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Lista neoznacenih cekpointa:\n\n");
            Console.WriteLine("----------------------------");
            ShowCheckpointsList(falseCheckpoints);

            Console.WriteLine("Unesite ime cekpointa koji zelite da oznacite:\n\n");
            string name = Console.ReadLine();

            MarkCheckpoint(FindCheckpointForMarking(falseCheckpoints, name), trackingTour);

            return FindCheckpointForMarking(falseCheckpoints, name);
        }
        public List<Checkpoint> ListFalseCheckpoints(Tour trackingTour)
        {
            List<Checkpoint> falseCheckpoints = new List<Checkpoint>();
            foreach (var checkpoint in trackingTour.Checkpoints)
            {
                if (!checkpoint.Status)
                {
                    falseCheckpoints.Add(checkpoint);
                }
            }
            return falseCheckpoints;
        }

        public List<Checkpoint> ListMarkedCheckpoints(Tour trackingTour)
        {
            List<Checkpoint> markedCheckpoints = new List<Checkpoint>();
            foreach (var checkpoint in trackingTour.Checkpoints)
            {
                if (checkpoint.Status)
                {
                    markedCheckpoints.Add(checkpoint);
                }
            }
            return markedCheckpoints;
        }

        public int CountFalseCheckpoints(Tour tour)
        {
            CheckpointRepository checkpointRepository = new CheckpointRepository();

            List<Checkpoint> checkpoints = checkpointRepository.GetTourCheckpoints(tour.TourId);

            int falseCheckpoints = 0;

            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (!checkpoint.Status)
                {
                    falseCheckpoints++;
                }
            }
            return falseCheckpoints;
        }

        public int CountMarkedCheckpoints(Tour tour)
        {
            CheckpointRepository checkpointRepository = new CheckpointRepository();

            List<Checkpoint> checkpoints = checkpointRepository.GetTourCheckpoints(tour.TourId);

            int markedCheckpoints = 0;

            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.Status)
                {
                    markedCheckpoints++;
                }
            }
            return markedCheckpoints;
        }

        public void MarkCheckpoint(Checkpoint checkpoint, Tour trackingTour)
        {
            using (var db = new DataContext())
            {
                foreach (var checkPoint in trackingTour.Checkpoints)
                {
                    if (checkPoint.CheckpointId == checkpoint.CheckpointId)
                    {
                        checkpoint.Status = true;
                        db.Checkpoints.Update(checkpoint);
                        db.SaveChanges();
                    }
                }

            }

        }

        public Checkpoint FindCheckpointForMarking(List<Checkpoint> checkpoints, string name)
        {
            foreach (var checkpoint in checkpoints)
            {
                if (checkpoint.Name == name)
                {
                    return checkpoint;
                }
            }
            return null;
        }

        public Checkpoint FindStartCheckpoint(Tour tour)
        {
            foreach (var checkpoint in tour.Checkpoints)
            {
                if (checkpoint.Type == CheckpointType.Start)
                {
                    return checkpoint;
                }
            }
            return null;
        }
        public void MarkingPresentTourists(Checkpoint currentCheckpoint, Tour trackingTour)
        {
            Console.WriteLine("TEST"); //ne ucitava turiste ili ne prepoznaje

            trackingTour.Tourists = tourRepository.GetTourists(trackingTour);

            foreach (var tourist in trackingTour.Tourists)
            {
                Console.WriteLine("***********************************");
                Console.WriteLine("Ime turiste: " + tourist.Username);
                Console.WriteLine("da li je turista prisutan: " + (tourist.IsPresent == false ? "nije prisutan" : "jeste prisutan" + "\n\n\n"));
            
            }

            string choice;
            do
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Za prestanak unosa prisutnih turista unesite x");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Unesite ime turiste kojeg zelite da oznacite ");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("vasa opcija: ");

                choice = Console.ReadLine();
                MarkTourist(choice, trackingTour,currentCheckpoint);

            } while(!choice.Equals("x"));

        }

        public void MarkTourist (string name, Tour trackingTour, Checkpoint currentCheckpoint)
        {
            using (var db = new DataContext())
            {
                foreach(var tourist in trackingTour.Tourists)
                {
                    if(tourist.Username == name)
                    {
                        if (tourist.IsPresent)
                        {
                            Console.WriteLine($"Turista {tourist.Username} je prisutan");
                            
                        }
                        else
                        {
                            tourist.IsPresent = true;

                            currentCheckpoint.Tourists.Add(tourist);
                            db.Tourists.Update(tourist);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public bool EndTour(Tour trackingTour)
        {

            using (var db = new DataContext())
            {
                foreach (var checkpoint in trackingTour.Checkpoints)
                {
                    if (checkpoint.Type == CheckpointType.End)
                    {
                        checkpoint.Status = true;
                        db.Checkpoints.Update(checkpoint);
                        db.SaveChanges();
                        return checkpoint.Status;
                    }
                }
            }
            return true;
        }

        public void UnmarkCheckpoints(List<Tour> tours)
        {
            using (var db = new DataContext())
            {
                foreach (var tour in tours)
                {
                    foreach (var checkpoint in tour.Checkpoints)
                    {
                        checkpoint.Status = false;
                        db.Checkpoints.Update(checkpoint);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
