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
        string _namePc = GetSetPcUserAttributes.namePc();
        string _ip = GetSetPcUserAttributes.GetLocalIPAddress();
        /// <summary>
        /// если параметры для уже созданого пользователя
        /// </summary>
        /// <param name="namePC"></param>
        /// <returns></returns>
        public  User GetParametrUser()
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    return db.Users.Where(c => c.namePc == _namePc && c.status == true).FirstOrDefault();
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
       public bool AddPasswordsUser(UserPasswordsDefault defaultLoginUser, string newPassword)
        {           
            try
            {
                using (UserContext db = new UserContext())
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
                            passwordLifeTime = defaultLoginUser.passwordLifeTime,
                            status = false
                        });
                                
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
        public bool AddPasswordsUser(User user, string newPasswordLocalUser)
        {         
            try
            {
                using (UserContext db = new UserContext())
                {                   
                    db.Users.Add(new User {
                        stampDateTimeLoadPc = DateTime.Now,
                        ipPC = _ip,
                        password = newPasswordLocalUser,
                        passwordСomplexity = user.passwordСomplexity,
                        passwordLifeTime = user.passwordLifeTime,
                        passwordLength = user.passwordLength,
                        loginUser = user.loginUser
                    });                 

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
        /// установить статус в БД что все БД = локально
        /// </summary>
        /// <param name="namePC"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public  Boolean setStatus(Boolean status)
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    var conn = db.Users.Where(c => c.namePc == _namePc).Last();

                    if (conn == null)
                    {
                        return false;
                    }
                    else
                    {
                        conn.status = status;
                        db.Entry(conn).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
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

