using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class UserRepository
    {

        public UserRepository()
        {

        }

        public bool AddUser(User user, int touristAge = 0)
        {
            using (var db = new DataContext())
            {
                if (user.UserType == UserType.Owner)
                {
                    Owner owner = new Owner(user.Username, user.Password, user.UserType);
                    db.Users.Add(owner);
                } else if (user.UserType == UserType.Guest)
                {
                    Guest guest = new Guest(user.Username, user.Password, user.UserType);
                    db.Users.Add(guest);
                } else if (user.UserType == UserType.Guide)
                {
                    Guide guide = new Guide(user.Username, user.Password, user.UserType);
                    db.Users.Add(guide);
                }
                else if (user.UserType == UserType.Tourist)
                {
                    Tourist tourist = new Tourist(user.Username, user.Password, touristAge, user.UserType);
                    db.Users.Add(tourist);
                }
                db.SaveChanges();
                return true;
            }
        }

        public static List<User> GetAllUsers()
        {
            using (var db = new DataContext())
            {
                return db.Users.ToList();
            }
        }

        public User GetByUsername(string username)
        {
            using (var db = new DataContext())
            {
                foreach (User user in db.Users)
                {
                    if (user.Username.Equals(username))
                    {
                        return user;
                    }
                }
            }
            return null;
        }

        public User Login(string username, string password)
        {
            using (var db = new DataContext())
            {
                foreach (User user in db.Users)
                {
                    if (user.Username.Equals(username) && user.Password.Equals(password))
                    {
                        return user;
                    }
                }
            }
            return null;
        }

        public void UpdateUser(User updatedUser)
        {
            using (var db = new DataContext())
            {
                var user = db.Users.FirstOrDefault(t => t.UserId == updatedUser.UserId);
                if (user != null)
                {
                    user.Username = updatedUser.Username;
                    user.Password = updatedUser.Password;
                    user.UserType = updatedUser.UserType;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteUser(int userId)
        {
            using (var db = new DataContext())
            {
                var user = db.Users.FirstOrDefault(t => t.UserId == userId);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateStatus(bool titleFlag) // nije radjeno za ulogovanog korisnika, naknadno ce biti ubaceno
        {

            using var db = new DataContext();
            foreach (Owner owner in db.Users)
            {
                if (owner.Username.Equals("owner"))
                {
                    owner.SuperOwner = titleFlag;
                    db.SaveChanges();
                }
            }
        }

        

    }
}
