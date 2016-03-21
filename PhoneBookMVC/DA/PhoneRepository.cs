using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.DA
{
    public class PhoneRepository : BaseRepository<Phone>
    {
        public PhoneRepository(AppContext context) : base(context) { }

        public List<Phone> GetByContactId(int id)
        {
            List<Phone> phones = GetAll().ToList();
            return (from p in phones
                    where p.ContactId == id
                    select p).ToList();
        }
    }
}