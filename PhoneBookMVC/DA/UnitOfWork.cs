using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.DA
{
    public class UnitOfWork
    {
        private UserRepository userRepo;
        private ContactRepository contactRepo;
        private PhoneRepository phoneRepo;
        private NoteRepository noteRepo;
        private QueryRepository queryRepo;
        private ContactGroupRepository groupRepo;

        private AppContext context;

        public UnitOfWork()
        {
            this.context = new AppContext();
        }

        public UserRepository UserRepository
        {
            get
            {
                if (userRepo == null)
                {
                    userRepo = new UserRepository(context);
                }
                return userRepo;
            }
        }

        public ContactRepository ContactRepository
        {
            get
            {
                if (contactRepo == null)
                {
                    contactRepo = new ContactRepository(context);
                }
                return contactRepo;
            }
        }

        public PhoneRepository PhoneRepository
        {
            get
            {
                if (phoneRepo == null)
                {
                    phoneRepo = new PhoneRepository(context);
                }
                return phoneRepo;
            }
        }

        public NoteRepository NoteRepository
        {
            get
            {
                if (noteRepo == null)
                {
                    noteRepo = new NoteRepository(context);
                }
                return noteRepo;
            }
        }

        public QueryRepository QueryRepository
        {
            get
            {
                if (queryRepo == null)
                {
                    queryRepo = new QueryRepository(context);
                }
                return queryRepo;
            }
        }

        public ContactGroupRepository ContactGroupRepository
        {
            get
            {
                if (groupRepo == null)
                {
                    groupRepo = new ContactGroupRepository(context);
                }
                return groupRepo;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}