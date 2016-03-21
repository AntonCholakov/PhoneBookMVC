using PhoneBookMVC.DA;
using PhoneBookMVC.Entity;
using PhoneBookMVC.Hasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Service
{
    public class AuthenticationService
    {
        public User LoggedUser { get; private set; }

        public bool AuthenticateUser(string username, string password)
        {
            LoggedUser = null;
            AppContext ctx = new AppContext();
            UserRepository userRepo = new UserRepository(ctx);
            User user = userRepo.GetByUsername(username);
            if (user != null)
            {
                if (PasswordHasher.Equals(password, user.Salt, user.Hash))
                {
                    LoggedUser = user;
                    return true;
                }   
            }

            return false;
        }
    }
}