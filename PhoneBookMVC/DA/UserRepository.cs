using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.DA
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(AppContext context) : base(context) { }

        public User GetByUsernameAndPassword(string username, string password)
        {
            List<User> users = GetAll().ToList();
            return (from u in users
                    where u.Username == username && u.Hash == password
                    select u).SingleOrDefault();
        }
        public User GetByUsername(string username)
        {
            List<User> users = GetAll().ToList();
            return (from u in users
                    where u.Username == username
                    select u).SingleOrDefault();
        }

    }
}