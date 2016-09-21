﻿using CorrectPassword.Log;
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
            string newPasswordLocal = PasswordGenerator.getPassword(user.passwordLength, user.passwordСomplexity);

            //проверим локальное время, если не правильно выходим
            TimeSpan tsCorrect = oldDate - nowTime;
            int diffTimeCorrect = tsCorrect.Days;

            if (diffTimeCorrect > 0)
            {
                Console.WriteLine("не правильное время на пк ");
                LogLocal.addLocalLog("не правильное время на пк ", EventLogEntryType.Warning);
                return;
            }

            // пришло время менять пароль? если нет выходим
            TimeSpan ts = nowTime - user.stampDateTimeLoadPc;
            int diffTime = ts.Days;

            if (user.passwordLifeTime >= diffTime)
            {
                Console.WriteLine("Рано менять пароль выходим");
                LogLocal.addLocalLog("Рано менять пароль, выходим", EventLogEntryType.Information);
                return;
            }

            // если ли есть пользователь локально сменим пароль, если ли нет добавим
            if ( GetSetPcUserAttributes.GetLocalAdmin(user.loginUser, "Администраторы") )
            {
               if( newUser.AddPasswordsUser(userDefault, newPasswordDefault) && GetSetPcUserAttributes.setUserPassword(user.loginUser, newPasswordLocal))
                {
                    Console.WriteLine("пароль сменился на новый у локального Администратора ");
                    newUser.setStatus(true);
                    LogLocal.addLocalLog("пароль сменился на новый у локального администратора, сохранено в БД", EventLogEntryType.Information);
                    return;
                }
               else
                {
                    Console.WriteLine("не удалось сменить пароль или БД не доступна");
                    newUser.setStatus(true);
                    LogLocal.addLocalLog("не удалось сменить пароль или БД не доступна", EventLogEntryType.Information);
                    return;
                }
            }
            else
            {              
                if (newUser.AddPasswordsUser(userDefault,newPasswordDefault) && GetSetPcUserAttributes.addUser(newPasswordDefault, userDefault, "Администраторы"))
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

           
            

            //if (user == null)
            //{ 
            //   if (userDefault == null)
            //    {
            //        Console.WriteLine("база не доступна выходим или не заполнен дефолтный профиль");
            //        LogLocal.addLocalLog("база не доступна выходим или не заполнен дефолтный профиль", EventLogEntryType.Warning);
            //        return;
            //    }

            //  string newPassword = PasswordGenerator.getPassword(userDefault.passwordLength, userDefault.passwordСomplexity);

            //   if (newUser.AddPasswordsUser(userDefault,newPassword))
            //    {
            //       if ( GetSetPcUserAttributes.addUser(newPassword, userDefault, "Администраторы") &&  )
            //        {
            //          
            //          
            //         
            //        }
            //       else
            //        {
            //          
            //            newUser.setStatus(false);
            //          
            //        }

            //    }
            //   else
            //    {
            //        Console.WriteLine("не удалось сохранить пароль в базу, ничего не изменилось ");
            //        LogLocal.addLocalLog("не удалось сохранить пароль в базу, ничего не изменилось ", EventLogEntryType.Warning);
            //        return;
            //    }              
            //}          

           

            //string newPasswordLocalUser = PasswordGenerator.getPassword(user.passwordLength, user.passwordСomplexity);          

            //if (newUser.AddPasswordsUser(user, newPasswordLocalUser) )
            //{
            //   if ( GetSetPcUserAttributes.setUserPassword(user.loginUser, newPasswordLocalUser))
            //    {
            //        Console.WriteLine("пароль сменился на новый у локального Администратора ");
            //        newUser.setStatus(true);
            //        LogLocal.addLocalLog("пароль сменился на новый у локального администратора, сохранено в БД", EventLogEntryType.Information);
            //    }
            //   else
            //    {
            //        Console.WriteLine("не получилось сохранить пароль, заведем заново пользователя ");
            //       if ( GetSetPcUserAttributes.addUser(newPasswordLocalUser, userDefault, "Администраторы"))
            //        {
            //            Console.WriteLine("новый пользователь добавлен пароль и логин в БД ");
            //            newUser.setStatus(true);
            //            LogLocal.addLocalLog("новый пользователь добавлен пароль и логин в БД ", EventLogEntryType.Information);
            //        }
            //       else
            //        {
            //            Console.WriteLine("не удалось сменить пароль или добавить пользователя пометим как status false");
            //            newUser.setStatus(false);
            //            LogLocal.addLocalLog("не удалось сменить пароль или добавить пользователя пометим как status false в БД", EventLogEntryType.Error);
            //        }
            //    }               
            //}
            //else
            //{
            //    Console.WriteLine("не удалось сохранить в БД пароль локального пользователя т.к БД не доступна, пароль остался старый");
            //    LogLocal.addLocalLog("не удалось сохранить в БД пароль локального пользователя т.к БД не доступна, пароль остался старый", EventLogEntryType.Warning);
            //    return;
            //}

            ////GetSetPcUserAttributes.setUserPassword(user.,PasswordGenerator.getPassword(12,1));
            ////newUser.SetPasswordsUser()

            //foreach (var dd in GetSetPcUserAttributes.GetLocalAdmin())
            //{
            //    Console.WriteLine(dd.ToString());
            //}        

            Console.WriteLine("Конец  ");
            Console.ReadKey();
        }

       
    }
}
