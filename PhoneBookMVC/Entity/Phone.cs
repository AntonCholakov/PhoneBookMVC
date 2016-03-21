using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Entity
{
    public class Phone : BaseEntity
    {
        public enum PhoneTypeEnum { Mobile, Home }
        private PhoneTypeEnum type;
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        public PhoneTypeEnum Type
        {
            get { return type; }
            set { type = value; }
        }

        // Foreign key for Contact
        public int ContactId { get; set; }
    }
}