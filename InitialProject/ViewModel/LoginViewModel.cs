using InitialProject.Commands;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using InitialProject.View;

namespace InitialProject.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _message;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(Login, CanLogin);
            CancelCommand = new DelegateCommand(Cancel);
            RegisterCommand = new DelegateCommand(Register);
        }

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

        public string Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged(nameof(Message)); }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
        public void ShowUserWindow(User user)
        {
            if (user.UserType == UserType.Owner)
            {
                var ownerWindow = new OwnerWindow();
                ownerWindow.ShowDialog();
            }
            else if (user.UserType == UserType.Guest)
            {
                var guestWindow = new GuestWindow();
                guestWindow.ShowDialog();
            }
            else if (user.UserType == UserType.Guide)
            {
                var guideWindow = new GuideWindow();
                guideWindow.ShowDialog();
            }
            else if (user.UserType == UserType.Tourist)
            {
                var touristWindow = new TouristWindow();
                touristWindow.ShowDialog();
            }
        }
        private void Login()
        {
            using (var db = new DataContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == Username && u.Password == Password);

                if (user != null)
                {
                    UserSession.LoggedInUser = user;
                    ShowUserWindow(user);
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                    CloseWindow();
                }
            }
        }

        private void Cancel()
        {
            CloseAction();
        }

        private void Register()
        {
            var registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    if (window == Application.Current.MainWindow)
                    {
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        window.Close();
                    }
                }
            }
        }
        public Action CloseAction { get; set; }

    }

}
