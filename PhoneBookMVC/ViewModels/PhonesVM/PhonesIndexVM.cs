using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.PhonesVM
{
    public class PhonesIndexVM
    {
        public int ID { get; set; }
        public int ContactId { get; set; }
        public List<Phone> Phones { get; set; }
    }
}