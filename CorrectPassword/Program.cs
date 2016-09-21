using CorrectPassword.Log;
using CorrectPassword.Repository;
using CorrectPassword.UserPasswordsSettings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CorrectPassword
{
    class Program
    {
        static void Main(string[] args)
        {

            LocalDb newUser = new LocalDb();
            User user = newUser.GetParametrUser();
            DateTime nowTime = DateTime.Now;
            DateTime oldDate = new DateTime(2016,9,10);
            UserPasswordsDefault userDefault = newUser.GetParametrDefaultUser();          

            if (user == null && userDefault == null)
            {
                Console.WriteLine("база не доступна выходим или не заполнен дефолтный профиль");
                LogLocal.addLocalLog("база не доступна выходим или не заполнен дефолтный профиль", EventLogEntryType.Warning);
                return;
            }

            string newPasswordDefault = PasswordGenerator.getPassword(userDefault.passwordLength, userDefault.passwordСomplexity);
            string newPasswordLocal;

            if (user != null)
            {
                newPasswordLocal = PasswordGenerator.getPassword(user.passwordLength, user.passwordСomplexity);

                // пришло время менять пароль? если нет выходим
                TimeSpan ts = nowTime - user.stampDateTimeLoadPc;
                int diffTime = ts.Days;

                if (user.passwordLifeTime >= diffTime && GetSetPcUserAttributes.GetLocalAdmin(user.loginUser, "Администраторы"))
                {
                    Console.WriteLine("Рано менять пароль выходим");
                    LogLocal.addLocalLog("Рано менять пароль, выходим", EventLogEntryType.Information);
                    return;
                }
            }
            else
            {
                newPasswordLocal = newPasswordDefault;
            }
            

            //проверим локальное время, если не правильно выходим
            TimeSpan tsCorrect = oldDate - nowTime;
            int diffTimeCorrect = tsCorrect.Days;

            if (diffTimeCorrect > 0)
            {
                Console.WriteLine("не правильное время на пк ");
                LogLocal.addLocalLog("не правильное время на пк ", EventLogEntryType.Warning);
                return;
            }

            // если ли есть пользователь локально сменим пароль, если ли нет добавим
            if (user == null || !GetSetPcUserAttributes.GetLocalAdmin(userDefault.defaultLoginUser, "Администраторы"))
            {
                if (newUser.AddPasswordsUser(userDefault, newPasswordDefault) && GetSetPcUserAttributes.addUser(newPasswordDefault, userDefault, "Администраторы"))
                {
                    Console.WriteLine("добавился новый пользователь, имя его и пароль в БД ");
                    LogLocal.addLocalLog("добавился новый администратор, имя его и пароль в БД ", EventLogEntryType.Information);
                    newUser.setStatus(true);
                    return;
                }
                else
                {
                    Console.WriteLine("не удалось добавить пользователя ");
                    LogLocal.addLocalLog("не удалось добавить пользователя ", EventLogEntryType.Warning);
                    newUser.setStatus(false);
                    return;
                }
            }

           if (user != null && GetSetPcUserAttributes.GetLocalAdmin(user.loginUser, "Администраторы"))
            {
                if (newUser.AddPasswordsUser(userDefault, newPasswordDefault) && GetSetPcUserAttributes.setUserPassword(user.loginUser, newPasswordLocal))
                {
                    Console.WriteLine("пароль сменился на новый у локального Администратора ");
                    newUser.setStatus(true);
                    LogLocal.addLocalLog("пароль сменился на новый у локального администратора, сохранено в БД", EventLogEntryType.Information);
                    return;
                }
                else
                {
                    Console.WriteLine("не удалось сменить пароль или БД не доступна");
                    newUser.setStatus(false);
                    LogLocal.addLocalLog("не удалось сменить пароль или БД не доступна", EventLogEntryType.Information);
                    return;
                }
            }

            Console.WriteLine("Конец  ");
            Console.ReadKey();
        }

       
    }
}
