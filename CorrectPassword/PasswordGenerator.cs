using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CorrectPassword
{
  public  class PasswordGenerator
    {
        public string getPassword()
        {
            string password = Membership.GeneratePassword(6,1);
            return password;
        }
    }
}
