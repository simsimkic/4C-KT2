
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebApi.Entities;

namespace InitialProject.Controller
{
    public class OwnerController
    {
        public OwnerController() { }

        public static readonly AccomodationService accomodationService = new AccomodationService(); 
        public static readonly AccomodationReservationService accomodationReservationService = new AccomodationReservationService();
        public static readonly GuestRatingService guestRatingService = new GuestRatingService(); 
        public static readonly GuestRatingRepository guestRatingRepository = new GuestRatingRepository();

        public void Menu()
        {
            string chosenOption;

            do
            {
                WriteMenuOptions();
                chosenOption = Console.ReadLine();
                Console.Clear();
                ProcessChosenOption(chosenOption);

            } while (!chosenOption.Equals("x"));


        }

        public void WriteMenuOptions()
        {
            Console.WriteLine("1. registruj smestaj i ubaci u bazu");
            Console.WriteLine("2. ispisi sve istekle rezervacije");
            Console.WriteLine("3. ispisi sve ocenjene rezervacije");
            Console.WriteLine("4. ispisi sve neocenjene istekle rezervacije");
            Console.WriteLine("5. obavestenja");
            Console.WriteLine("6. oceni gosta");
            Console.WriteLine("x. exit");
            Console.Write("Your option: ");
        }

        public static void ProcessChosenOption(string chosenOption)
        {
            switch (chosenOption)
            {
                case "1":
                    Accomodation accomodation = CreateAccomodation();
                    Location location = CreateLocation();
                    List<AccomodationImage> accomodationImages = CreateAccomodationImages();
                    accomodationService.RegisterAccomodation(accomodation, location, accomodationImages);
                    break;
                case "2":
                    List<AccomodationReservation> expiredReservations = accomodationReservationService.GetAllExpired();
                    foreach (var reservation in expiredReservations)
                    {
                        Console.WriteLine(reservation.ToString());
                    }
                    break;
                case "3":
                    List<AccomodationReservation> gradedReservations = guestRatingService.GetGradedReservations();
                    foreach (var reservation in gradedReservations)
                    {
                        Console.WriteLine(reservation.ToString());
                    }
                    break;
                case "4":
                    List<AccomodationReservation> nonGradedExpiredReservations = guestRatingService.GetNotGradedExpiredReservations();
                    foreach (var reservation in nonGradedExpiredReservations)
                    {
                        Console.WriteLine(reservation.ToString());
                    }
                    break;
                case "5":
                    /*List<AccomodationReservation> accomodationReservations = new List<AccomodationReservation>();
                    accomodationReservations = guestRatingService.GetNotGradedExpiredReservations();
                    if (accomodationReservations.Count == 0)
                    {
                        Console.WriteLine("No new notifications!");
                        return;
                    }

                    DateTime todaysDate = DateTime.UtcNow.Date;



                    foreach (var reservation in accomodationReservations)
                    {
                        int daysLeft = 5 - (todaysDate.Day - reservation.CheckOutDate.Day);

                        if (daysLeft == 1)
                        {
                            Console.WriteLine("Reservation " + reservation.AccomodationReservationId + " has expired: \n   " + daysLeft.ToString() + " day left to rate guest: " + reservation.User.Username);
                        }


                        Console.WriteLine("Reservation " + reservation.AccomodationReservationId + " has expired: \n   " + daysLeft.ToString() + " days left to rate guest: " + reservation.User.Username);
                    }
                    break;

                   /* List<AccomodationReservation> accomodationReservations = new List<AccomodationReservation>();
                    accomodationReservations = guestRatingService.GetNotGradedExpiredReservations(); 
                    if (accomodationReservations.Count == 0)
                    {
                        Console.WriteLine("No new notifications!");
                        return;
                    }

                    DateTime todaysDate = DateTime.UtcNow.Date;

                    

                    foreach (var reservation in accomodationReservations)
                    {
                        int daysLeft = 5 - (todaysDate.Day - reservation.CheckOutDate.Day);
                        
                        if (daysLeft == 1)
                        {
                            Console.WriteLine("Reservation " + reservation.AccomodationReservationId + " has expired: \n   " + daysLeft.ToString() + " day left to rate guest: " + reservation.User.Username);
                        } 


                       Console.WriteLine("Reservation " + reservation.AccomodationReservationId + " has expired: \n   " + daysLeft.ToString() + " days left to rate guest: " + reservation.User.Username);
                    }
                    break; */
                case "6":
                   /* List<AccomodationReservation> reservations = new List<AccomodationReservation>();
                    reservations = guestRatingService.GetNotGradedExpiredReservations();

                    int n = 0;
                    foreach(var reservation in reservations)
                    {
                        Console.WriteLine(n + "." + reservation.ToString());
                        n++;
                    }

                    Console.WriteLine("Enter Reservation ID: ");
     

                    int option = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Cleanlines");
                    int cleanliness = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("RuleCompl");
                    int rulec = Int32.Parse(Console.ReadLine());

                    GuestRating guestRating = new GuestRating(cleanliness, rulec, reservations[option]);
                    
                    guestRatingRepository.Save(guestRating);


                    break;*/
                case "x":
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }

        }


        public static Location CreateLocation()
        {

            string City;
            string Country;

            Console.Clear();
            Console.WriteLine("City:");
            City = Console.ReadLine();
            Console.WriteLine("Country:");
            Country = Console.ReadLine();

            Location newLocation = new Location(City, Country);

            return newLocation;
        }

        public static List<AccomodationImage> CreateAccomodationImages()
        {
   
            string Name;

            string URL;

            List<AccomodationImage> accomodationImages = new List<AccomodationImage>();

            Console.Clear();
            Console.WriteLine("Koliko vas smestaj ima slika(URL):");
            int numOfImages = Int32.Parse(Console.ReadLine());


            for (int i = 0; i < numOfImages; i++)
            {

                Console.Clear();
                Console.WriteLine("Image Name:");
                Name = Console.ReadLine();
                Console.WriteLine("URL:");
                URL = Console.ReadLine();

                AccomodationImage accomodationImage = new AccomodationImage(Name, URL);
                accomodationImages.Add(accomodationImage);

            }

            return accomodationImages;
        }

        public static Accomodation CreateAccomodation()
        {
            string name;
            Location location = new Location();
            AccomodationType accType;
            int maxGuests;
            int minReservationDays;
            int daysBeforeCanceling;


            Console.WriteLine("Name:");
            name = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Enter AccomodationType value (Apartman, House, or Cabin):");
            string input = Console.ReadLine();
      
            bool isValid = Enum.TryParse(input, out accType);

            if (!isValid)
            {
                Console.WriteLine("Invalid AccomodationType value entered.");
            }
            else
            {
                string enumString = Enum.GetName(typeof(AccomodationType), accType);
                Console.WriteLine("You entered: " + enumString);
            }
            Console.Clear();

            Console.WriteLine("Maximum number of Guests:");
            maxGuests = Int32.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Minimum number of days for reservation:");
            minReservationDays = Int32.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Number of days before cancellation: ");
            daysBeforeCanceling = Int32.Parse(Console.ReadLine());
            Console.Clear();




            Accomodation newAccomodation = new Accomodation(name, location, accType, maxGuests, minReservationDays, daysBeforeCanceling);

            return newAccomodation;
        }
    }
}
