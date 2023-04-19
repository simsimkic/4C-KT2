
﻿using InitialProject.Repository;
﻿using InitialProject.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;


namespace InitialProject.Service
{
    public class UserService
    {
        
        UserRepository userRepository = new UserRepository();
        public UserService() { }    

        

        
        public void UpdateStatus(bool titleFlag)
        {
            userRepository.UpdateStatus(titleFlag);
        }
        public bool AddUser(User user, int touristAge = 0)
        {
            return userRepository.AddUser(user, touristAge);
        }

        public User Login(string username, string password)
        {
            return userRepository.Login(username, password);
        }

        public User GetByUsername(string username)
        {
            return userRepository.GetByUsername(username);

        }
    }
}
