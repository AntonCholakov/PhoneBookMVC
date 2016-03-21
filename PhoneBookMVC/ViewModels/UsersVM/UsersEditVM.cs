using PhoneBookMVC.Entity;
using PhoneBookMVC.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.UsersVM
{
    public class UsersEditVM
    {
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailCustom]
        public string Email { get; set; }

        public User.UserRoleEnum Role { get; set; }

        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}