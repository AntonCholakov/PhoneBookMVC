using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.QueryVM
{
    public class QueryCreateVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}