using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.ContactGroupsVM
{
    public class ContactGroupsIndexVM
    {
        public Contact Contact { get; set; }
        public int ContactID { get; set; }
        public List<ContactGroup> ContactGroups { get; set; }
    }
}