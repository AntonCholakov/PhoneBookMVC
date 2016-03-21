using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.PhonesVM
{
    public class PhonesCreateVM
    {
        private int id;
        private Phone.PhoneTypeEnum type;
        private string number;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        [Required]
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        [Required]
        public Phone.PhoneTypeEnum Type
        {
            get { return type; }
            set { type = value; }
        }

        // Foreign key for Contact
        [Required]
        public int ContactId { get; set; }
    }
}