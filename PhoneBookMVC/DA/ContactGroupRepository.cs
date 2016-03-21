using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.DA
{
    public class ContactGroupRepository : BaseRepository<ContactGroup>
    {
        public ContactGroupRepository(AppContext context) : base(context) { }

        public List<ContactGroup> GetByUserId(int id)
        {
            List<ContactGroup> groups = GetAll().ToList();
            return (from g in groups
                    where g.UserId == id
                    select g).ToList();
        }
        public override void Delete(int id)
        {
            ContactGroup group = base.GetById(id);
            group.Contacts.Clear();
            base.Update(group);
            base.Delete(id);
            base.Save();
        }
    }
}