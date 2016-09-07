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
        public UserContext() : base("CorrectPassword.Properties.Settings.Setting") { }

        public DbSet<User> Users { get; set; }

        public DbSet<UserPasswordsDefault> UserPasswordsDefault { get; set; }
    }
}
