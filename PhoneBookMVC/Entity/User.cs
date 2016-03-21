using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Entity
{
    public class User : BaseEntity
    {
        public enum UserRoleEnum { Admin, User }
        private string firstName;
        private string hash;
        private string lastName;
        private UserRoleEnum role;
        private string salt;
        private string username;
        private string email;
                
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<ContactGroup> ContactGroups { get; set; }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string Hash
        {
            get { return hash; }
            set { hash = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public UserRoleEnum Role
        {
            get { return role; }
            set { role = value; }
        }

        public string Salt
        {
            get { return salt; }
            set { salt = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}