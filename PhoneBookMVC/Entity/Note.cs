using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Entity
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastEdit { get; set; }

        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}