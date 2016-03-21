using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Entity
{
    public class Contact : BaseEntity
    {
        private string firstName;
        private string lastName;
        private string email;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PhotoFilePath { get; set; }
        public DateTime? BirthDay { get; set; }

        
        public int UserId { get; set; }

        public virtual ICollection<ContactGroup> ContactGroups  { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public ICollection<Note> Notes { get; set; }
        public virtual User User { get; set; }
    }
}