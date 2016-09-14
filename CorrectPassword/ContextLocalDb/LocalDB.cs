using CorrectPassword.ContextLocalDb;
using CorrectPassword.UserPasswordsSettings;
using System.Collections.Generic;
using System.Linq;
using System;
using CorrectPassword.Repository;

namespace CorrectPassword.Repository
{
    public class LocalDb : IRepository
    {
        /// <summary>
        /// если параметры для уже созданого пользователя
        /// </summary>
        /// <param name="namePC"></param>
        /// <returns></returns>
    public  User GetParametrUser(string namePC)
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    return db.Users.Where(c => c.namePc == namePC).FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// параметры дефолтного пользователя
        /// </summary>
        /// <returns></returns>
      public UserPasswordsDefault GetParametrDefaultUser()
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                   
                    return db.UserPasswordsDefault.FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        ///  сохранить пароль в базе нового пользователя
        /// </summary>
        /// <param name="defaultLoginUser"></param>
        /// <param name="newPassword"></param>
        /// <param name="getLocalIPAddress"></param>
        /// <param name="_namePc"></param>
        /// <returns></returns>
       public bool SetPasswordsUser(UserPasswordsDefault defaultLoginUser, string newPassword)
        {
            string _namePc = GetSetPcUserAttributes.namePc();
            string _ip = GetSetPcUserAttributes.GetLocalIPAddress();
            try
            {
                using (UserContext db = new UserContext())
                {
                    var conn = db.Users
                        .AsEnumerable()
                        .Where(c => c.namePc == _namePc)
                        .FirstOrDefault()
                        ;

                    if (conn == null)
                    {
                        db.Users.Add(new User
                        {
                            ipPC = _ip,
                            namePc = _namePc,
                            password = newPassword,
                            passwordСomplexity = 1,
                            loginUser = defaultLoginUser.defaultLoginUser,
                            stampDateTimeLoadPc = DateTime.Now,                           
                            passwordLength = defaultLoginUser.passwordLength,
                            passwordLifeTime = defaultLoginUser.passwordLifeTime
                        });
                    }                 

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        /// <summary>
        /// сохранить пароль старого пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPasswordLocalUser"></param>
        /// <returns></returns>
        internal bool SetPasswordsUser(User user, string newPasswordLocalUser)
        {
            string _namePc = GetSetPcUserAttributes.namePc();
            string _ip = GetSetPcUserAttributes.GetLocalIPAddress();
            try
            {
                using (UserContext db = new UserContext())
                {
                    var conn = db.Users
                        .AsEnumerable()
                        .Where(c => c.namePc == _namePc)
                        .FirstOrDefault()
                        ;

                    if (conn != null)
                    {                       
                        conn.stampDateTimeLoadPc = DateTime.Now;
                        conn.ipPC = _ip;
                        conn.password = newPasswordLocalUser;
                        conn.passwordСomplexity = user.passwordСomplexity;
                        conn.loginUser = user.loginUser;
                        conn.stampDateTimeLoadPc = DateTime.Now;                       
                        conn.passwordLength = user.passwordLength;
                        conn.passwordLifeTime = user.passwordLifeTime;

                        db.Entry(conn).State = System.Data.Entity.EntityState.Modified;
                    }                  

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// проверка пароля в базе, ну если очень хочется
        /// </summary>
        /// <param name="namePC"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public  Boolean ValidPasswords(string namePC, string password)
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    var conn = db.Users.Where(c => c.namePc == namePC && c.password == password).ToList();

                    if (conn == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }                
            }
            catch
            {
                return false;
            }
        }     
    }
}
