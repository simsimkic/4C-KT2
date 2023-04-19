using InitialProject.Commands;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WebApi.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace InitialProject.ViewModel
{
    public class TourRatingViewModel :  INotifyPropertyChanged
    {
        private string _guideKnowledge;
        private string _guideLanguage;
        private string _tourAmusement;
        private string _comment;
        private string _photoURL;
        private string _tourName;
        private List<TourImages> _tourImages;

        public ICommand RateTourCommand { get; set; }

        public ICommand AddPhotoCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public TourRatingViewModel()
        {
            RateTourCommand = new DelegateCommand(RateTour, CanRateTour);
            AddPhotoCommand = new DelegateCommand(AddPhoto);
            TourImages = new List<TourImages>();
        }

        public string GuideKnowledge
        {
            get { return _guideKnowledge; }
            set { _guideKnowledge = value; RaisePropertyChanged(nameof(GuideKnowledge)); }
        }

        public string GuideLanguage
        {
            get { return _guideLanguage; }
            set
            {
                _guideLanguage = value;
                RaisePropertyChanged(nameof(GuideLanguage));
            }
        }

        public string TourAmusement
        {
            get { return _tourAmusement; }
            set
            {
                _tourAmusement = value;
                RaisePropertyChanged(nameof(TourAmusement));
            }
        }
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                RaisePropertyChanged(nameof(Comment));
            }
        }

        public string PhotoURL
        {
            get { return _photoURL; }
            set
            {
                _photoURL = value;
                RaisePropertyChanged(nameof(PhotoURL));
            }
        }

        public List<TourImages> TourImages
        {
            get { return _tourImages; }
            set
            {
                _tourImages = value;
                RaisePropertyChanged(nameof(TourImages));
            }
        }

        public string TourName
        {
            get { return _tourName; }
            set
            {
                _tourName = value;
                RaisePropertyChanged(nameof(TourName));
            }
        }

        public bool CanRateTour()
        {
            if (GuideKnowledge != null && GuideLanguage != null && TourAmusement != null && !string.IsNullOrEmpty(Comment) && TourImages.Count >= 1)
            {
                return true;
            }
            return false;
        }


        public void RateTour()
        {
            TourRatingService tourRatingService = new TourRatingService();
            TourService tourService = new TourService();
            Tour ratedTour = tourService.GetByName(TourName);
            if (ratedTour == null)
            {
                MessageBox.Show("No such tour!");
                return;
            }
            if (CanTouristRate())
            {
                int guideKnowledge = GetIntFromString(GuideKnowledge);
                int guideLanguage = GetIntFromString(GuideLanguage);
                int tourAmusement = GetIntFromString(TourAmusement);

                TourRating tourRating = new TourRating(guideKnowledge, guideLanguage, tourAmusement, Comment, TourImages);
                tourRatingService.Add(tourRating, UserSession.LoggedInUser.Id, ratedTour.TourId);
                TourImages.Clear();
                MessageBox.Show("You successfully rated a tour!");
            } else
            {
                TourImages.Clear();
                MessageBox.Show("You can't rate this tour.");
            }
        }

        public void AddPhoto()
        {
            TourImages tourImage = new TourImages(TourName, PhotoURL);
            TourImages.Add(tourImage);
            PhotoURL = null;
            MessageBox.Show("You added an image!");
        }

        public bool CanTouristRate()
        {
            TourService tourService = new TourService();
            Tour tour = tourService.GetByName(TourName);
            if (tour == null)
            {
                MessageBox.Show("No such tour!");
            }
            TouristsService touristsService = new TouristsService();
            return touristsService.CanTouristRate(UserSession.LoggedInUser.Id, tour);
        }

        public int GetIntFromString(string str)
        {
            int value;
            if (int.TryParse(Regex.Match(str, @"\d+").Value, out value))
            {
                return value;
            }

            return -1;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Action CloseAction { get; set; }
    }
}
