using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.NotesVM
{
    public class NotesCreateVM
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastEdit { get; set; }
        public int ContactId { get; set; }
    }
}