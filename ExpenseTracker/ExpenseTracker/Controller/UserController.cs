﻿using ExpenseTracker.Business_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Controller
{
    internal class UserController
    {
        private UsersBL user;

        public UserController()
        {
            this.user = new UsersBL();
        }
        public bool AuthenticateUser(string email, string password)
        {
            return user.AuthenticationService(email, password);
        }

        public string getNameByEmail(string email)
        {
            string str =  user.getUserByEmailService(email);
            if (str == null)
            {
                return "ERROR: Name not found!";
            }
            else return str;
        }
    }
}