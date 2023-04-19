using InitialProject.Commands;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _confirmPassword;
        private UserType _userType;
        private string _message;
        private int _age;
        private bool _isAgeEnabled;
        

        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand RegisterCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public RegisterViewModel()
        {
            UserTypes = new ObservableCollection<UserType>(Enum.GetValues(typeof(UserType)).Cast<UserType>());
            RegisterCommand = new DelegateCommand(Register, CanRegister);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public ObservableCollection<UserType> UserTypes { get; private set; }

        public string Username
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged(nameof(Username)); }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; RaisePropertyChanged(nameof(ConfirmPassword)); }
        }

        public UserType UserType
        {
            get { return _userType; }
            set { _userType = value; RaisePropertyChanged(nameof(UserType)); }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged(nameof(Message)); }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged(nameof(Age));
            }
        }

        public bool IsAgeEnabled
        {
            get { return _isAgeEnabled; }
            set
            {
                _isAgeEnabled = value;
                RaisePropertyChanged(nameof(IsAgeEnabled));
            }
        }

        private bool CanRegister()
        {
            if (!string.IsNullOrEmpty(Username) &&
                !string.IsNullOrEmpty(Password) &&
                !string.IsNullOrEmpty(ConfirmPassword) &&
                Password == ConfirmPassword) {
                if (UserType == UserType.Tourist)
                {
                    IsAgeEnabled = true;
                    if (Age != 0)
                    {
                        return true;
                    }
                    return false;
                } else
                {
                    IsAgeEnabled = false;
                }
                return true;
            }
            return false;
        }

        public void ShowUserWindow()
        {
            if (UserType == UserType.Owner)
            {
                var ownerWindow = new OwnerWindow();
                ownerWindow.ShowDialog();
            }
            else if (UserType == UserType.Guide)
            {
                var guideWindow = new GuideWindow();
                guideWindow.ShowDialog();
            }
            else if (UserType == UserType.Guest)
            {
                var guestWindow = new GuestWindow();
                guestWindow.ShowDialog();
            }
            else if (UserType == UserType.Tourist)
            {
                var touristWindow = new TouristWindow();
                touristWindow.ShowDialog();
            }
        }
        private void Register()
        {          
            User user = new User(Username, Password, UserType);
            UserService userService = new UserService();
            if (UserType == UserType.Tourist) {
                userService.AddUser(user, Age);
            }
            else {
                userService.AddUser(user);
            }

            ShowUserWindow();
        }

        private void Cancel()
        {
            CloseAction();
        }


        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Action CloseAction { get; set; }
    }
   
}
