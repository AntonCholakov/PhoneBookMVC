using PhoneBookMVC.Entity;
using PhoneBookMVC.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace PhoneBookMVC.ViewModels.ContactsVM
{
    public class ContactsCreateVM
    {
        private int id;
        private string firstName;
        private string lastName;        
        private string email;

        public int ID
        {
            get { return id; }
            set { id = value; }
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
        //[RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$")]
        [EmailCustom]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PhotoFilePath { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "yyyy/MM/dd", ConvertEmptyStringToNull = true)]
        public DateTime? BirthDate { get; set; }

        // Foreign key for User
        public int UserId { get; set; }
    }
}