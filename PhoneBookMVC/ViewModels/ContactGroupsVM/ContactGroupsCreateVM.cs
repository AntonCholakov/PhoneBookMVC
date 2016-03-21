using System;
using PhoneBookMVC.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.ContactGroupsVM
{
    public class ContactGroupsCreateVM
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}