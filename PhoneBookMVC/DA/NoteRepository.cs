using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.DA
{
    public class NoteRepository : BaseRepository<Note>
    {
        public NoteRepository(AppContext context) : base(context) { }

        public List<Note> GetByContactId(int id)
        {
            List<Note> notes = GetAll().ToList();
            return (from n in notes
                    where n.ContactId == id
                    select n).ToList();
        }
    }
}