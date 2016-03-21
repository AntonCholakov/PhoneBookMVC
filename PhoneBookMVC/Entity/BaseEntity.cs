using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Entity
{
    public class BaseEntity
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
    }
}