﻿using ExpenseTracker.Business_Logic;
using ExpenseTracker.Exceptions;
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
        public UsersBL AuthenticateUser(string email, string password)
        {
            return user.AuthenticationService(email, password);
        }

        public string getNameByEmail(string email)
        {
            string str =  user.getUserByEmailService(email);
            if (str == null)
            {
                throw new MissingDataException("The name of the user with this email: " + email + "was not found!");
            }
            else return str;
        }

        public int getIdByEmail(string email)
        {
            int id = user.getUserIdByEmailService(email);
            if(id == -1)
            {
                throw new MissingDataException("User with email: " + email + "not found!");
            }
            else return id;
        }

        public string addNewUser(string email, string password, string name) {
            bool result = user.addNewUser(email, password, name);
            if (result)
            {
                return "Success creating new account! \n Go back to the login window to proceed.";
            }
            else return "Error creating account! \n That email is already beeing used.";
        }
    }
}
