using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.DA
{
    public class ContactRepository : BaseRepository<Contact>
    {
        public ContactRepository(AppContext context) : base(context) { }

        public List<Contact> GetByUserId(int id)
        {
            List<Contact> contacts = GetAll().ToList();
            return (from c in contacts
                    where c.UserId == id
                    select c).ToList();
        }

        public override void Delete(int id)
        {
            Contact contact = base.GetById(id);
            contact.ContactGroups.Clear();
            base.Update(contact);
            base.Delete(id);
            base.Save();
        }
    }
}