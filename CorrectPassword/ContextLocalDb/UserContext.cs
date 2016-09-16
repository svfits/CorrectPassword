using CorrectPassword.UserPasswordsSettings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace CorrectPassword.ContextLocalDb
{
  public   class UserContext : DbContext
    {
        public UserContext() : base("data source = localhost; Initial Catalog = UserAdminLocal; Integrated Security = True; TrustServerCertificate=False") { }

        // base("data source = (localdb)\\MSSQLLocalDB; Initial Catalog = AthletesAccounting; Integrated Security = True; TrustServerCertificate=False") { }


        public DbSet<User> Users { get; set; }

        public DbSet<UserPasswordsDefault> UserPasswordsDefault { get; set; }
    }
}
