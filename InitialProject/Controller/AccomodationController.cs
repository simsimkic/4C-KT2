using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApi.Entities;

namespace InitialProject.Controller
{
    public class AccomodationController
    {
        public AccomodationService AccomodationService;
        AccomodationRepository accomodationRepository = new AccomodationRepository();
        AccomodationService accomodationService1 = new AccomodationService();

        public AccomodationController() { }

        public void GetMenu()
        {
            string chosenOption;
            do
            {
                //Kako da imam vise od jednog objekta u bazi npr za Reservate ili Rating (posle 1 ubaguje baza, UNIQUE CONSTARINT nesto tako)
                //spajanje da ne moram i accID i ID da spajam da bude na dva mesta isti (zbor brisanja)
                //

                Console.WriteLine("1. Show all accomodations");
                Console.WriteLine("2. Show accomodations by name [FULL PREWIEV]");
                Console.WriteLine("3. Show accomodations by location");
                Console.WriteLine("4. Show accomodations by type");
                Console.WriteLine("5. Show accomodations by number of guests");
                Console.WriteLine("6. Show accomodations by min days of reservation");
                Console.WriteLine("7. View available accomodations");
                Console.WriteLine("8. Reservate accomodation");
                Console.WriteLine("9. Cancel reservation");
                Console.WriteLine("10. Rating accomodation and owner");
                Console.WriteLine("11. Recommendation for renovation");
                Console.WriteLine("x - exit");
                chosenOption = Console.ReadLine();
                Console.Clear();

                switch (chosenOption)
                {
                    case "1":
                        Console.Clear();
                        GetAllAccomodations();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Name: ");
                        string name = Console.ReadLine();
                        GetByName(name);
                        break;
                    case "3":
                        Console.Clear();
                        string city, country;
                        Console.WriteLine("City: ");
                        city = Console.ReadLine();
                        Console.WriteLine("Country: ");
                        country = Console.ReadLine();
                        GetByLocation(city, country);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Type: ");
                        string type = Console.ReadLine();
                        GetByType(type);
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Number of guests: ");
                        int guestsNumber = int.Parse(Console.ReadLine());
                        GetByGuestsNumber(guestsNumber);
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Number of days reservation: ");
                        int reservationDays = int.Parse(Console.ReadLine());
                        GetByReservationDays(reservationDays);
                        break;

                    case "7":
                        Console.Clear();
                        Console.WriteLine("StartDate? (yyyy-MM-dd 12:00:00)");
                        DateTime start = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("EndDate? (yyyy-MM-dd 12:00:00)");
                        DateTime end = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Number of guests?");
                        int numberOfGuests = int.Parse(Console.ReadLine());
                        FindAvailableAccomodations(start, end, numberOfGuests);
                        break;

                    case "8":
                        Console.Clear();

                        Console.WriteLine("Id acc?");
                        int idAcc = int.Parse(Console.ReadLine());
                        Console.WriteLine("Guest id?");
                        int idGuest = int.Parse(Console.ReadLine());
                        Console.WriteLine("broj gosti");
                        int brojGosti = int.Parse(Console.ReadLine());

                        AccomodationService accomodationService = new AccomodationService();
                        UserService userService = new UserService();
                        Console.WriteLine("Username: ");
                        string username = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        string password = Console.ReadLine();
                        Console.WriteLine("StartDate? (yyyy-MM-dd 12:00:00)");
                        DateTime startD = DateTime.Parse(Console.ReadLine()); 
                        Console.WriteLine("EndDate? (yyyy-MM-dd 12:00:00)");
                        DateTime endD = DateTime.Parse(Console.ReadLine());
                        if (userService.Login(username, password) != null)
                        {

                            Console.WriteLine("Reserving accomodation:");
                            if(accomodationService.GetFreeSpotsNumber(idAcc) < brojGosti)
                            {
                                Console.WriteLine("Dont have free spots u need.");
                            }
                            Guest guests = (Guest)userService.GetByUsername(username);
                            foreach(Accomodation accomodation in accomodationService.FindAvailableAccomodations(startD, endD, brojGosti))
                            {
                                if(accomodationRepository.GetAccomodationById(idAcc).AccId == accomodation.AccId)
                                {
                                    
                                    MakeReservation(idAcc, idGuest, brojGosti, startD, endD);

                                    Console.WriteLine("Succesfully reserved accomodation!");
                                }
                            }
                            break;
                        }
                        break;

                    case "9":
                        Console.Clear();
                        Console.WriteLine("Id acc?");
                        int Accid = int.Parse(Console.ReadLine());
                        accomodationService1.CancelReservation(Accid);
                        break;

                    case "10":
                        Console.Clear();

                        Console.WriteLine("AccomodationID u want to rate?");
                        int acccId = int.Parse(Console.ReadLine());
                        Console.WriteLine("OwnerID u want to rate?");
                        int ownerId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Rate CLEANLINESS for accomodation? [1-5]");
                        int cleanliess = int.Parse(Console.ReadLine());
                        Console.WriteLine("Rate owner's correctness [1-5]");
                        int ownerFriendliess = int.Parse(Console.ReadLine());

                        Console.WriteLine("ID for comment u wanna to write:");
                        int commentId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Comment?");
                        string commentText = Console.ReadLine();

                        Console.WriteLine("Images ID?");
                        int imageId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Image name?");
                        string imageName = Console.ReadLine();
                        Console.WriteLine("Image URL?");
                        string imageURL = Console.ReadLine();

                        Console.WriteLine("Date from day u gone out from accomodation?");
                        DateTime goneTime = DateTime.Parse(Console.ReadLine());

                        RateAccomodation(acccId, ownerId, cleanliess, ownerFriendliess, commentId,commentText,imageId,imageName,imageURL, goneTime);

                        break;

                    case "11":
                        Console.Clear();

                        Console.WriteLine("accID for RecomMendation for Renovation?");
                        int Idaccc = int.Parse(Console.ReadLine());
                        Console.WriteLine("the level of urgency for renovation [1-5]");
                        int rating = int.Parse(Console.ReadLine());
                        Console.WriteLine("What's specific for renovation?");
                        string recommendation = Console.ReadLine();

                        AddAccomodationReview(Idaccc, rating, recommendation);
                        break;

                }

            } while (!chosenOption.Equals("x"));
        }



        public void GetAllAccomodations()
        {
            AccomodationService accomodationService = new AccomodationService();
            AccomodationImagesService accomodationImagesService = new AccomodationImagesService();
            LocationService locationService = new LocationService();
            List<Accomodation> allAccomodations = accomodationService.GetAllAccomodations();
            List<AccomodationImage> accomodationImages = new List<AccomodationImage>();
            Location accomodationLocation = new Location();

            foreach (Accomodation accomodation in allAccomodations)
            {
                Console.WriteLine(accomodation.ToString());
                accomodationImages = accomodationImagesService.GetAccomodationImages(accomodation.AccId);
                foreach (AccomodationImage accomodationImage in accomodationImages)
                {
                    Console.WriteLine(accomodationImage);
                }
                accomodationLocation = accomodationService.GetAccomodationLocation(accomodation.AccId);
                Console.WriteLine(accomodationLocation);

            }
        }

        public void GetByName(string name)
        {
            AccomodationService accomodationService = new AccomodationService();
            AccomodationImagesService accomodationImagesService = new AccomodationImagesService();
            LocationService locationService = new LocationService();
            List<Accomodation> accomodationsByName = accomodationService.GetByName(name);
            List<AccomodationImage> accomodationImages = new List<AccomodationImage>();
            Location accomodationLocation = new Location();
            foreach (Accomodation accomodation in accomodationsByName)
            {
                Console.WriteLine(accomodation);
                accomodationImages = accomodationImagesService.GetAccomodationImages(accomodation.AccId);
                foreach (AccomodationImage accomodationImage in accomodationImages)
                {
                    Console.WriteLine(accomodationImage);
                }
                accomodationLocation = accomodationService.GetAccomodationLocation(accomodation.AccId);
                Console.WriteLine(accomodationLocation);
            }
        }

        public void GetByType(string type)
        {
            AccomodationService accomodationService = new AccomodationService();
            List<Accomodation> accomodationsByType = accomodationService.GetByType(type);
            foreach (Accomodation accomodation in accomodationsByType)
            {
                Console.WriteLine(accomodation);
            }
        }

        public void GetByLocation(string city, string country)
        {
            AccomodationService accomodationService = new AccomodationService();
            LocationService locationService = new LocationService();
            Location location = locationService.GetByCityAndCountry(city, country);
            List<Accomodation> accomodationsByLocation = accomodationService.GetAccomodationsByLocation(location.LocationId);

            foreach (Accomodation accomodation in accomodationsByLocation)
            {
                Console.WriteLine(accomodation);
            }
        }


        public void GetByGuestsNumber(int guestsNumber)
        {
            AccomodationService accomodationService = new AccomodationService();
            List<Accomodation> accomodationsByGuestsNumber = accomodationService.GetByGuestsNumber(guestsNumber);
            foreach (Accomodation accomodation in accomodationsByGuestsNumber)
            {
                Console.WriteLine(accomodation);
            }
        }

        public void GetByReservationDays(int reservationDays)
        {
            AccomodationService accomodationService = new AccomodationService();
            List<Accomodation> accomodationsByReservationDays = accomodationService.GetByReservationDays(reservationDays);
            foreach (Accomodation accomodation in accomodationsByReservationDays)
            {
                Console.WriteLine(accomodation);
            }
        }

        public void FindAvailableAccomodations(DateTime start, DateTime end, int numberOfGuests)
        {
            AccomodationService accomodationService = new AccomodationService();
            List<Accomodation> accomodationsAvailable = accomodationService.FindAvailableAccomodations(start, end, numberOfGuests);
            foreach (Accomodation accomodation in accomodationsAvailable)
            {
                Console.WriteLine(accomodation);
            }
        }

        
        public void MakeReservation(int accId, int guestsId,int numberGuests, DateTime startD, DateTime endD)
        {
            AccomodationService accomodationService = new AccomodationService();

            Accomodation izabran = accomodationService.GetAccomodationById(accId);
            List<Accomodation> availableAccommodation = accomodationService.FindAvailableAccomodations(startD, endD, numberGuests);
            if (availableAccommodation == null)
            {

                throw new Exception("Nema slobodnih.");
            }

            foreach (Accomodation accomodation in availableAccommodation)
            {
                if (accomodation == izabran)
                {
                    accomodationService.BookAcc(accId, guestsId, numberGuests,startD,endD);
                }
            }
            using (var db = new DataContext())
            {
                var reservation = new AccomodationReservation
                {
                    Id = accId,
                    CheckInDate = startD,
                    CheckOutDate = endD,
                    NumberOfGuests = numberGuests,
                    
                };

                db.AccomodationReservations.Add(reservation);
                db.SaveChanges();
                accomodationService.UpdateAvailability(accId, startD, endD, numberGuests);
                Console.WriteLine("aaa");
            }
        }

        public void RateAccomodation(int accId, int ownerId, int cleanliness, int ownerFriendliness, int commentId,string commentText, int imageId,string imageName, string imageURL, DateTime timeGone)
        {
            AccomodationRatingRepository accomodationRatingRepository = new AccomodationRatingRepository();

            DateTime now = DateTime.Now;

            accomodationRatingRepository.AddAccomodationRating(accId, ownerId,  cleanliness,  ownerFriendliness,  commentId,  commentText,  imageId,  imageName,  imageURL,  timeGone);
            return;  

        }   

        public void AddAccomodationReview(int accId,int rating, string recommendation)
        {
            AccomodationReviewRepository accomodationReviewRepository = new AccomodationReviewRepository();

            accomodationReviewRepository.AddAccomodationReview(accId, rating, recommendation);
            return;
        }
        
    }
}
