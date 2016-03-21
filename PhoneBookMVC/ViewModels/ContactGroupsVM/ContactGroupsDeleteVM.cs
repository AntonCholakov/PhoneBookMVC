using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.ContactGroupsVM
{
    public class ContactGroupsDeleteVM
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}