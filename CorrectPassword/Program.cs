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
            User user = newUser.GetParametrUser();
            DateTime nowTime = DateTime.Now;
            DateTime oldDate = new DateTime(2016,9,10);
            UserPasswordsDefault userDefault = newUser.GetParametrDefaultUser();

            // проверим локальное время если не правильно выходим
            TimeSpan tsCorrect = oldDate - nowTime;
            int diffTimeCorrect = tsCorrect.Days;         

            if (diffTimeCorrect > 0)
            {
                Console.WriteLine("не правильное время на пк заменить батарейку");
                return;
            }         

            if (user == null)
            { 
               if (userDefault == null)
                {
                    Console.WriteLine("база не доступна выходим или не заполнен дефолтный профиль");
                    return;
                }
                         
              string newPassword = PasswordGenerator.getPassword(userDefault.passwordLength, userDefault.passwordСomplexity);
                
               if (newUser.AddPasswordsUser(userDefault,newPassword))
                {
                   if ( GetSetPcUserAttributes.addUser(newPassword, userDefault, "Администраторы") )
                    {
                        Console.WriteLine("добавился новый пользователь имя его и пароль в БД ");
                        newUser.setStatus(true);
                        return;
                    }
                   else
                    {
                        Console.WriteLine("не удалось добавить пользователя ");
                        newUser.setStatus(false);
                        return;
                    }

                }
               else
                {
                    Console.WriteLine("не удалось сохранить пароль в базу ничего не изменилось ");
                    return;
                }              
            }          

            // пришло время менять пароль? если нет выходим
            TimeSpan ts = nowTime - user.stampDateTimeLoadPc;
            int diffTime = ts.Days;

            if (diffTime >= user.passwordLifeTime)

            {
                Console.WriteLine("Рано менять пароль выходим");
                return;
            }

            string newPasswordLocalUser = PasswordGenerator.getPassword(user.passwordLength, user.passwordСomplexity);          
        
            if (newUser.AddPasswordsUser(user, newPasswordLocalUser) )
            {
               if ( GetSetPcUserAttributes.setUserPassword(user.loginUser, newPasswordLocalUser))
                {
                    Console.WriteLine("пароль сменился на новый у локального Администратора ");
                    newUser.setStatus(true);
                }
               else
                {
                    Console.WriteLine("не получилось сохранить пароль, заведем заново пользователя ");
                   if ( GetSetPcUserAttributes.addUser(newPasswordLocalUser, userDefault, "Администраторы"))
                    {
                        Console.WriteLine("новый пользователь добавлен пароль и логин в БД ");
                        newUser.setStatus(true);
                    }
                   else
                    {
                        Console.WriteLine("не удалось сменить пароль или добавить пользователя пометим как status false");
                        newUser.setStatus(false);
                    }
                }               
            }
            else
            {
                Console.WriteLine("не удалось сохранить в БД пароль локального пользователя т.к БД не доступна, пароль остался старый");
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
