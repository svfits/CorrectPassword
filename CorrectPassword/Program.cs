using CorrectPassword.Repository;
using CorrectPassword.UserPasswordsSettings;
using System;
using System.Collections;
using System.Collections.Generic;
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
            User user = newUser.GetParametrUser(GetSetPcUserAttributes.namePc());
            DateTime nowTime = DateTime.Now;
            DateTime oldDate = new DateTime(2016,9,10);

            TimeSpan tsCorrect = oldDate - nowTime;
            int diffTimeCorrect = tsCorrect.Days;
            if (diffTimeCorrect > 0)
            {
                Console.WriteLine("не правильное время на пк заменить батарейку ");
            }

            if (user == null)
            {
                UserPasswordsDefault userDefault = newUser.GetParametrDefaultUser();

                if (userDefault == null )
                {
                    Console.WriteLine("база не доступна выходим ");
                    return;
                }
                string newPassword = PasswordGenerator.getPassword(userDefault.passwordLength, userDefault.passwordСomplexity);
                
               if ( newUser.SetPasswordsUser(userDefault.defaultLoginUser,newPassword))
                {
                    GetSetPcUserAttributes.addUser(newPassword, userDefault.defaultLoginUser, "Администраторы");
                    Console.WriteLine("сохранился пароль везде у нового пользователя ");
                    return;
                }
               else
                {
                    Console.WriteLine("не удалось сохранить пароль в базу ");
                    return;
                }              
            }

            string newPasswordLocalUser = PasswordGenerator.getPassword(user.passwordLength, user.passwordСomplexity);
            
            TimeSpan ts = nowTime - user.stampDateTimeLoadPc;
            int diffTime = ts.Days;

            if (newUser.SetPasswordsUser(user.loginUser, newPasswordLocalUser) && diffTime > user.passwordLifeTime )
            {
                GetSetPcUserAttributes.setUserPassword(user.loginUser, newPasswordLocalUser);
                Console.WriteLine("сохранился пароль везде у нового пользователя ");
            }
            else
            {
                Console.WriteLine("не удалось сохранить пароль в базу старого пользователя");
                return;
            }

            //GetSetPcUserAttributes.setUserPassword(user.,PasswordGenerator.getPassword(12,1));
            //newUser.SetPasswordsUser()

            foreach (var dd in GetSetPcUserAttributes.GetLocalAdmin())
            {
                Console.WriteLine(dd.ToString());
            }        

            Console.ReadKey();
        }

       
    }
}
