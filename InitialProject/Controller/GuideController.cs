using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using WebApi.Entities;

namespace InitialProject.Controller
{
    public class GuideController
    {

        public GuideController() { }

        public static readonly TourService tourService = new TourService();


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
            Console.WriteLine("1.kreiraj turu i ubaci u bazu");
            Console.WriteLine("2. prati turu");
            Console.WriteLine("3. testiraj");
            Console.WriteLine("x. exit");
            Console.Write("Your option: ");
        }

        public static void ProcessChosenOption(string chosenOption)
        {
            switch (chosenOption)
            {
                case "1":
                    Tour tour = CreateTour();
                    Location location = CreateLocation();
                    List<TourImages> image = CreateTourImages();
                    List<Checkpoint> checkpoint = CreateCheckpoints();
                    List<Dates> dates = CreateDates();
                    tourService.MakeTour(tour, location, image, checkpoint, dates);

                    break;
                case "2":
                    Console.WriteLine("Izabrali ste pracenje ture");
                    tourService.TourTracking();
                    break;
                case "3": 
                    K2_F2_Guide k2_F2_Guide = new K2_F2_Guide();
                    k2_F2_Guide.FinishedTours();
                    break;
                case "x":
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }

        }

        public static List<Dates> CreateDates()
        {
            
            List<Dates> StartingDates = new List<Dates>();

            Console.WriteLine("koliko tura ima datuma");
            int n = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Dates date = new Dates();
                Console.WriteLine("Unesite datum:");
                date.Date = DateTime.Parse(Console.ReadLine());
                StartingDates.Add(date);
            }

            return StartingDates;
        }

        public static Tour CreateTour()
        {
            int id;
            string name;
            string Description;
            string language;
            int maxGuests;
            DateTime starTime;
            DateTime endTime;
            int duration;

            Console.Clear();

            //Console.WriteLine("TourId:");
            //id = Int32.Parse(Console.ReadLine());

            Console.WriteLine("TourName:");
            name = Console.ReadLine();

            Console.WriteLine("TourDecription:");
            Description = Console.ReadLine();

            Console.WriteLine("TourLanguage");
            language = Console.ReadLine();

            Console.WriteLine("MaxGuests:");
            maxGuests = Int32.Parse(Console.ReadLine());

           // Console.WriteLine("StartTime(yyyy-mm-dd hh:mm:ss):");
            //starTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("TourDuration:");
            duration = Int32.Parse(Console.ReadLine());

            Tour newTour = new Tour(name, Description, language,maxGuests,duration);

            return newTour;
        }
        public static Location CreateLocation()
        {

            string City;
            string Country;

            Console.Clear();
            // Console.WriteLine("LocationId:");
            // id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("City:");
            City = Console.ReadLine();
            Console.WriteLine("Country:");
            Country = Console.ReadLine();

            Location newLocation = new Location(City, Country);

            return newLocation;

        }
        public static List<TourImages> CreateTourImages()
        {
            string Name;

            string URL;

            List<TourImages> tourImages = new List<TourImages>();

            Console.Clear();
            Console.WriteLine("Koliko vasa tura ima slika(URL):");
            int numOfImages = Int32.Parse(Console.ReadLine());


            for (int i = 0; i < numOfImages; i++)
            {


                Console.WriteLine("Image Name:");
                Name = Console.ReadLine();
                Console.WriteLine("URL:");
                URL = Console.ReadLine();

                TourImages tourImage = new TourImages(Name, URL);
                tourImages.Add(tourImage);

            }

            return tourImages;
        }

        public static List<Checkpoint> CreateCheckpoints()
        {
            string Name;
            CheckpointType Type;
            Checkpoint checkpoint;
            List<Checkpoint> checkpoints = new List<Checkpoint>();

            Console.Clear();
            int numOfCheckpoints;
            do
            {
                Console.WriteLine("Koliko vasa tura ima kljucnih tacaka(>=2):");
                numOfCheckpoints = Int32.Parse(Console.ReadLine());

            } while (numOfCheckpoints < 2);

            for (int i = 0; i < numOfCheckpoints; i++)
            {

                Console.WriteLine("Checkpoint Name:");
                Name = Console.ReadLine();

                Console.WriteLine("Checkpoint Type(Start, Middle, End):");
                Type = (CheckpointType)Enum.Parse(typeof(CheckpointType), Console.ReadLine());

                checkpoint = new Checkpoint(Name, Type);
                checkpoints.Add(checkpoint);
            }



            return checkpoints;
        }

        public static Boolean IsValueRight(string checkpointType)
        {
            if (Enum.IsDefined(typeof(CheckpointType), checkpointType))
            {
                return true;

            }
            return false;

        }
    }
}
