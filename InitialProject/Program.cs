using InitialProject.Controller;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject
{
    internal class Program
    {
        [STAThreadAttribute]

        public static void Main()
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

        private static void WriteMenuOptions()
        {
            Console.WriteLine("1. Owner's view");

            Console.WriteLine("2. option B");

            Console.WriteLine("2. Guide's view");
            Console.WriteLine("3. Guest's view");
            Console.WriteLine("4. Tourist's view");

            Console.WriteLine("x. exit");
            Console.Write("Your option: ");
        }

        private static void ProcessChosenOption(string chosenOption) 
        {
            switch(chosenOption)
            {
                case "1":
                    Console.WriteLine("Owner's view");
                    OwnerController controllerMenu = new OwnerController();
                    controllerMenu.Menu();
                    break; 
                case "2": 
                    Console.WriteLine("Chosen option: 2");
                    GuideController guideMenu = new GuideController();
                    guideMenu.Menu();
                    break; 

                    Console.WriteLine("Guide's view"); 
                    break;
                case "3":
                    Console.WriteLine("Chosen option: 3");
                    AccomodationController accomodationController = new AccomodationController();
                    accomodationController.GetMenu();
                    break;
                case "4":
                    Console.WriteLine("Chosen option: 4");
                    Console.WriteLine("Chosen option: Prikaz i pretraga tura");
                    TourController TourController = new TourController();
                    TourController.GetMenu();
                    break;

                case "x":
                    break;
                default:
                    Console.WriteLine("Option does not exist"); 
                    break;
            }

        }
    }
}
