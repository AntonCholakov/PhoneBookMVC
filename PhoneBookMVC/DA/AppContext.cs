using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.DA
{
    public class AppContext : DbContext
    {

        public AppContext() : base("PhoneBookDB") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<ContactGroup> ContactGroups { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Contact>()
                .HasMany(p => p.ContactGroups)
                .WithMany(t => t.Contacts)
                .Map(mc =>
                {
                    mc.ToTable("ContactJoinContactGroup");
                    mc.MapLeftKey("ContactId");
                    mc.MapRightKey("ContactGroupId");
                });
        }
        //public void AddToClass(Contact @class)
        //{
        //    base.AddObject("Class", @class);
        //}
    }
    
}