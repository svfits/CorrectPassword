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
        /// <summary>
        //// Генератор паролей
        /// </summary>
        /// <param name="lengthPassword">длина пароля</param>
        /// <param name="complexityPassword">сложность пароля</param>
        /// <returns></returns>
        public string getPassword(int lengthPassword, int complexityPassword)
        {
            string password = Membership.GeneratePassword(lengthPassword,complexityPassword);
            return password;
        }
    }
}
