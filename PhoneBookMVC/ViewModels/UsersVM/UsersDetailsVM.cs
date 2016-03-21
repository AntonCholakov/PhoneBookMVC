using PhoneBookMVC.Entity;
using PhoneBookMVC.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.UsersVM
{
    public class UsersDetailsVM
    {
        private int id;
        private string username;
        private string firstName;
        private string lastName;
        private User.UserRoleEnum role;
        private string email;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [Required]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [Required]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [Required]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [Required]
        [EmailCustom]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [Required]
        public string Password { get; set; }

        public User.UserRoleEnum Role
        {
            get { return role; }
            set { role = value; }
        }
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}