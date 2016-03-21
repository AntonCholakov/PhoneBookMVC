using PagedList;
using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.ContactsVM
{
    public class ContactsIndexVM
    {
        public string SortOrder { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GroupId { get; set; }
        public Dictionary<string, object> Props { get; set; }
        public int? Page { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<ContactGroup> ContactGroups { get; set; }
        public IPagedList<Contact> PagedContacts { get; set; }
    }
}