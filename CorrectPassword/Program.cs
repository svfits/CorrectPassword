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

            GetPcAttributes.SetPasswordSetUser("","","");

            foreach (var dd in GetPcAttributes.GetLocalAdmin())
            {
                Console.WriteLine(GetPcAttributes.GetLocalIPAddress() + "  " + GetPcAttributes._namePc() + "  " + dd.ToString());
            }                 
                        
            Console.ReadKey();
        }

       
    }
}
