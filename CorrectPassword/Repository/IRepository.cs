using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrectPassword.UserPasswordsSettings;

namespace CorrectPassword.Repository
{
    interface IRepository
    {
        /// <summary>
        /// параметры пользователя
        /// </summary>
        /// <returns>возвращает список параметров пользователя</returns>
        User GetParametrUser(string namePC);    

        /// <summary>
        /// получение дефолтного пользователя
        /// </summary>
        /// <returns></returns>
        UserPasswordsDefault GetParametrDefaultUser();

        /// <summary>
        /// установить пароль для пользователя
        /// </summary>
        /// <returns></returns>
        bool SetPasswordsUser(UserPasswordsDefault defaultLoginUser, string newPassword);

    }
}
