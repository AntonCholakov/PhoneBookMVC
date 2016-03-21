using System;
using PhoneBookMVC.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.NotesVM
{
    public class NotesIndexVM
    {
        public List<Note> Notes { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}