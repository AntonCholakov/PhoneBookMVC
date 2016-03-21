using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Entity
{
    public class Query : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}