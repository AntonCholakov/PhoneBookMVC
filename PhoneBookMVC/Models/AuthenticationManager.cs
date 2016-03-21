using PhoneBookMVC.Entity;
using PhoneBookMVC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Models
{
    public class AuthenticationManager
    {
        private static AuthenticationService AuthenticationServiceInstance
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session[typeof(AuthenticationService).Name] == null)
                {
                    HttpContext.Current.Session[typeof(AuthenticationService).Name] = new AuthenticationService();
                }

                return (AuthenticationService)HttpContext.Current.Session[typeof(AuthenticationService).Name];
            }
        }

        public static User LoggedUser
        {
            get
            {
                return AuthenticationManager.AuthenticationServiceInstance.LoggedUser;
            }
        }

        public static void AuthenticateUser(string username, string password)
        {
            AuthenticationServiceInstance.AuthenticateUser(username, password);
        }

        public static void Logout()
        {
            HttpContext.Current.Session[typeof(AuthenticationService).Name] = null;
            // force auth to null loggeduser
            AuthenticationServiceInstance.AuthenticateUser(null, null);
        }
    }
}