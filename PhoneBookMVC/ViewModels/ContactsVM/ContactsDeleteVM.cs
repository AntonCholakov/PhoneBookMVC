using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.ContactsVM
{
    public class ContactsDeleteVM
    {
        public int ID { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }

        public string PhotoFilePath { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "yyyy/MM/dd", ConvertEmptyStringToNull = true)]
        public DateTime? BirthDate { get; set; }

        // Foreign key for User
        public int UserId { get; set; }
    }
}