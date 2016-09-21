using CorrectPassword.Log;
using CorrectPassword.UserPasswordsSettings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

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
            return "";
        }

        /// <summary>
        /// список администраторов на пк 
        /// </summary>
        /// <returns></returns>
        public static Boolean GetLocalAdmin(string userName, string group)
        {
            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
            DirectoryEntry admGroup = localMachine.Children.Find("Администраторы", "group");
            object members = admGroup.Invoke("members", null);

            //List<string> lstUsers = new List<string>();

            foreach (object groupMember in (IEnumerable)members)
            {
                DirectoryEntry member = new DirectoryEntry(groupMember);
                //lstUsers.Add(member.Name);
                if(member.Name == userName)
                {
                    return true;
                }
            }

            return false;
        }

        public static Boolean addGroup(UserPasswordsDefault defaultLoginUser, string groups)
        {         
            DirectoryEntry userGroup = null;
            string userName = defaultLoginUser.defaultLoginUser;
            string groupName = groups;

            try
            {
                string groupPath = String.Format(CultureInfo.CurrentUICulture, "WinNT://{0}/{1},group", Environment.MachineName, groupName);
                userGroup = new DirectoryEntry(groupPath);

                if ((null == userGroup) || (true == String.IsNullOrEmpty(userGroup.SchemaClassName)) || (0 != String.Compare(userGroup.SchemaClassName, "group", true, CultureInfo.CurrentUICulture)))
                {
                    return false;
                }

                String userPath = String.Format(CultureInfo.CurrentUICulture, "WinNT://{0},user", userName);
                userGroup.Invoke("Add", new object[] { userPath });
                userGroup.CommitChanges();
                return true;               
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error creating account: {0}", ex.Message);
                LogLocal.addLocalLog(ex.Message, EventLogEntryType.Error);
                return false;
            }         

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

                GetSetPcUserAttributes.addGroup(defaultLoginUser,groups);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating account: {0}", ex.Message);
                LogLocal.addLocalLog(ex.Message, EventLogEntryType.Error);
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
                LogLocal.addLocalLog(ex.Message, EventLogEntryType.Error);
                return false;
            }          
        }

    }

}

