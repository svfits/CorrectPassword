using CorrectPassword.ContextLocalDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrectPassword.Repository
{
    public class LocalDB : IRepository
    {
        public List<User> GetParametrUser(string namePC)
        {
            using (UserContext db = new UserContext())
            {
                return db.Users.Where(c => c.namePc == namePC).ToList();
            }
        }

        void IRepository.SetPasswordsUser(User jjj)
        {
            using (UserContext db = new UserContext())
            {
                var conn = db.Users
                    .AsEnumerable()
                    .Where(c => c.namePc == jjj.namePc).ToList()
                    .FirstOrDefault()
                    ;
                if (conn == null)
                {
                    conn.password = jjj.password;
                    conn.ipPC = jjj.ipPC;

                    db.Entry(conn).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Users.Add(new User 
                        {
                        ipPC = jjj.ipPC,
                        namePc = jjj.namePc,
                        password = jjj.password,
                        passwordСomplexity = 1,
                        loginUser = jjj.loginUser                        
                    });
                }

                db.SaveChanges();                
            }
        }

        bool IRepository.ValidPasswords(string namePC, string password)
        {
            using (UserContext db = new UserContext())
            {
                var conn =  db.Users.Where(c => c.namePc == namePC && c.password == password).ToList();

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
    }
}
