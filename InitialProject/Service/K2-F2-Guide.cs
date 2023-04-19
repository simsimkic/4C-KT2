using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebApi.Entities;

namespace InitialProject.Service
{
    public class K2_F2_Guide
    {   
        TourRepository tourRepository = new TourRepository();
        DatesRepository datesRepository = new DatesRepository();


        public TourDateDTO ShowMostVisitedTour(List<Dates> dates)
        {
            int mostVisitedDateId = FindMostVisitedDateId(dates);
            List<Tour> tours = tourRepository.GetAll();
            Tour tour = FindMostVisitedTourName(tours, mostVisitedDateId);

            if (tour != null)
            {
                return MakeMostVisitedDTO(mostVisitedDateId, tour);

            } else
            {
                return null;
            }
        }
        
        public TourDateDTO MakeMostVisitedDTO(int mostVisitedDateId, Tour tour)
        {
            Dates date = datesRepository.GetById(mostVisitedDateId);

            TourDateDTO tourDateDTO = new TourDateDTO(tour.TourId, tour.Name, date.Date, date.Id, tour.Description);

            return tourDateDTO;
        }
        public TourDateDTO ShowMostVisitedByYear(int year)
        {
            List<Dates> dates = datesRepository.GetByYear(year);
            TourDateDTO tourToReturn = ShowMostVisitedTour(dates);
            return tourToReturn;
        }

        public void TouristsStat(string dateName)
        {

        }
        public int FindMostVisitedDateId(List<Dates> dates)
        {
            int max = 0;
            int maxDateId = dates[0].Id;
            foreach (var date in dates)
            {
                int TouristCounter = 0;
                TouristCount(date, ref TouristCounter);
                FindMaxID(TouristCounter, ref max, ref maxDateId, date);
                
            }
            return maxDateId;
        }

        public void TouristCount(Dates date, ref int TouristCounter)
        {
            foreach (var tourist in date.Tourists)
            {
                TouristCounter++;
            }
        }

        public void FindMaxID(int TouristCounter, ref int max, ref int maxDateId, Dates date)
        {
            if (TouristCounter > max)
            {
                max = TouristCounter;
                maxDateId = date.Id;
            }
        }

        public Tour FindMostVisitedTourName(List<Tour> tours, int dateId)
        {
            return IterateTours(tours, dateId);
        }

        public Tour IterateTours(List<Tour> tours, int dateId)
        {
            foreach (var tour in tours)
            {
                var result = IterateDates(dateId, tour);

                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public Tour IterateDates(int dateId, Tour tour)
        {
            foreach (var date in tour.StartingDates)
            {
                var result = FindByID(date, dateId, tour);

                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
        public Tour FindByID(Dates date, int dateId, Tour tour)
        {
            if (date.Id == dateId)
            {
                return tour;
            }
            return null;
        }

        public List<TourDateDTO> FinishedTours()
        {
            
            List<Tour> Tours = tourRepository.GetAll();
            List<TourDateDTO> toursToReturn = new List<TourDateDTO>();
            DateTime currentTime = DateTime.Now;
            foreach (var tour in Tours)
            {
                if (tour.GuideId == UserSession.LoggedInUser.Id)
                {
                    List<TourDateDTO> dto = dDTO(tour);

                    foreach (var date in dto)
                    {


                        toursToReturn.Add(date);

                    }
                }
            }
            return toursToReturn;
        }

        public List<TourDateDTO> dDTO (Tour tour)
        {   
            List<TourDateDTO> toursToReturn =  new List<TourDateDTO> ();
            
            DateTime currentTime = DateTime.Now;
            foreach (var date in tour.StartingDates)
            {   
                DateTime finishDate = date.Date.AddHours(tour.Duration);
                if (DateTime.Compare(finishDate, currentTime) < 0)
                {
                    TourDateDTO tourDate = new TourDateDTO(tour.TourId, tour.Name, date.Date, date.Id, tour.Description);
                    toursToReturn.Add(tourDate);
                }
                
            }
            return toursToReturn;
        }

        public GuestAgeStatisticDTO GuestAgeStatisticDTO(Dates date, int tourId)
        {

            Tour tour = tourRepository.GetById(tourId);
            
            int percentUnder18 = CountUnder18(date);
            int percent18and50 = CountBeetween18and50(date);
            int percentAbove50 = CountAbove50(date);
            double with = WithCouponsPercent(date, tourId);
            double without = WithoutCouponsPercent(date, tourId);
            
            

            GuestAgeStatisticDTO guestStatisticDTO = new GuestAgeStatisticDTO(tourId, tour.Name, percentUnder18, percent18and50, percentAbove50, with, without);
        
            return guestStatisticDTO;
        
        } 

        public int CountUnder18(Dates date)
        {
            int under18 = 0;
            int allTourists = 0;

            foreach(var tourist in date.Tourists)
            {
                CheckAgeUnder18(ref allTourists, tourist, ref under18);
            }
            if(under18 <= 0) {return 0;}

            return under18;

        }
        public void CheckAgeUnder18(ref int allTourists, Tourist tourist, ref int count)
        {
            allTourists++;
            if (tourist.Age < 18)
            {
                count++;
            }
        }

        public int CountBeetween18and50(Dates date)
        {
            int under50 = 0;
            int allTourists = 0;

            foreach (var tourist in date.Tourists)
            {
                CheckAgeBeetween(ref allTourists, tourist, ref under50);
            }

            if (under50 <= 0){return 0;}

            return under50;

        }

        public void CheckAgeBeetween(ref int allTourists, Tourist tourist, ref int count)
        {
            allTourists++;
            if (tourist.Age < 50 && tourist.Age >18)
            {
                count++;
            }
        }

        public int CountAbove50(Dates date)
        {
            int above50 = 0;
            int allTourists = 0;

            foreach (var tourist in date.Tourists)
            {
               CheckAgeAbove50(ref allTourists, tourist, ref above50);
            }

            if (above50 <= 0){ return 0;}

            return above50;

        }

        public void CheckAgeAbove50(ref int allTourists, Tourist tourist, ref int count)
        {
            allTourists++;
            if (tourist.Age > 50)
            {
                count++;
            }
        }

        public double WithCouponsPercent(Dates dates, int tourId)
        {
            double numOfCoupons = 0;
            double numOfTourists = 0;
            foreach (var tourist in dates.Tourists)
            {   
                numOfTourists++;
                foreach(var coupons in tourist.Coupons)
                {
                    if(coupons.TourId == tourId)
                    {
                        numOfCoupons++;
                    }
                }
            }
            if(numOfTourists <= 0) { return 0; }
            
            return (numOfCoupons/numOfTourists)*100;
        }

        public double WithoutCouponsPercent(Dates dates, int tourId)
        {   
            
            double numOfCoupons = 0;
            double numOfTourists = 0;
            foreach (var tourist in dates.Tourists)
            {
                int flag = 0;
                numOfTourists++;
                
                foreach (var coupons in tourist.Coupons)
                {
                    if (coupons.TourId == tourId)
                    {
                        flag = 1;
                        break;
                    }
                    
                }
                if (flag == 0) { numOfCoupons++; }
            }
            if (numOfTourists <= 0) { return 0; }
            
            return (numOfCoupons / numOfTourists) * 100;
            ;
        }
    }
}
