using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrectPassword.Repository
{
    interface IRepository
    {
        /// <summary>
        /// параметры пользователя
        /// </summary>
        /// <returns>возвращает список параметров пользователя</returns>
        List<User> GetParametrUser(string namePC);
      

        /// <summary>
        /// удачно ли сохранился пароль на сервере
        /// </summary>
        /// <returns></returns>
        Boolean ValidPasswords(string namePC, string password);
        /// <summary>
        /// установить пароль для пользователя
        /// </summary>
        /// <returns></returns>
        void SetPasswordsUser(User usr);

    }
}
