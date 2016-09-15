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
        User GetParametrUser();    

        /// <summary>
        /// получение дефолтного пользователя
        /// </summary>
        /// <returns></returns>
        UserPasswordsDefault GetParametrDefaultUser();

        /// <summary>
        /// установить пароль для пользователя
        /// </summary>
        /// <returns></returns>
        bool AddPasswordsUser(UserPasswordsDefault defaultLoginUser, string newPassword);
        /// <summary>
        ////установить статус нового пользователя
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        Boolean setStatus(Boolean status);

    }
}
