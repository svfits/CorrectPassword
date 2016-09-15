using CorrectPassword.UserPasswordsSettings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CorrectPassword
{
    public static class GetSetPcUserAttributes
    {
        /// <summary>
        ////имя машины
        /// </summary>
        /// <returns></returns>
        public static string namePc()
        {
            return Environment.MachineName;
        }

        /// <summary>
        /// локальный ip адрес выбираем только которые начинаются 10
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    string ss = ip.ToString();
                    string text = ss.Substring(0, 2);
                    if (text == "10")
                    {
                        return ss;
                    }
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        /// <summary>
        /// список администраторов на пк 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalAdmin()
        {
            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
            DirectoryEntry admGroup = localMachine.Children.Find("Администраторы", "group");
            object members = admGroup.Invoke("members", null);
            List<string> lstUsers = new List<string>();

            foreach (object groupMember in (IEnumerable)members)
            {
                DirectoryEntry member = new DirectoryEntry(groupMember);
                lstUsers.Add(member.Name);
            }

            return lstUsers;
        }

        public static Boolean addUser(string password, UserPasswordsDefault defaultLoginUser, string groups)
        {
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Machine);
                
                UserPrincipal user = new UserPrincipal(context);
                user.SetPassword(password);
                user.DisplayName = defaultLoginUser.defaultLoginUser;
                user.Name = defaultLoginUser.defaultLoginUser;
                user.Description = defaultLoginUser.description;
                user.UserCannotChangePassword = true;
                user.PasswordNeverExpires = true;

                user.Enabled = true;
                
                user.Save();

                PrincipalContext context2 = new PrincipalContext(ContextType.Machine);
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context2, "Операторы настройки сети");

                group.Members.Add(user);
                group.Save();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating account: {0}", ex.Message);
                return false;
            }

        }

        public static Boolean setUserPassword(string user, string password)
        {
            try
            {
                DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
                DirectoryEntry _user = localMachine.Children.Find(user, "user");               
                _user.Password = password;                
                _user.CommitChanges();
                 
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating account: {0}", ex.Message);
                return false;
            }          
        }

    }

}

