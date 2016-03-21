namespace PhoneBookMVC.Migrations
{
    using PhoneBookMVC.Entity;
    using PhoneBookMVC.Hasher;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhoneBookMVC.DA.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PhoneBookMVC.DA.AppContext context)
        {
            User user = new User();
            user.Role = User.UserRoleEnum.Admin;
            user.Username = "admin";
            user.FirstName = "Admin";
            user.LastName = "Adminov";
            var passPhrase = PasswordHasher.Hash("admin");
            user.Hash = passPhrase.Hash;
            user.Salt = passPhrase.Salt;
            user.Email = "admin@programista.pro";
            context.Users.AddOrUpdate(user);
        }
    }
}
