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
        ///  сохранить пароль в базе
        /// </summary>
        /// <param name="defaultLoginUser"></param>
        /// <param name="newPassword"></param>
        /// <param name="getLocalIPAddress"></param>
        /// <param name="_namePc"></param>
        /// <returns></returns>
       public bool SetPasswordsUser(string defaultLoginUser, string newPassword)
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
                        conn.password = newPassword;
                        conn.stampDateTimeLoadPc = DateTime.Now;                        
                        conn.ipPC = _ip;

                        db.Entry(conn).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Users.Add(new User
                        {
                            ipPC = _ip,
                            namePc = _namePc,
                            password = newPassword,
                            passwordСomplexity = 1,
                            loginUser = defaultLoginUser,
                            stampDateTimeLoadPc = DateTime.Now,
                            correctDateTime = DateTime.Now                            
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
