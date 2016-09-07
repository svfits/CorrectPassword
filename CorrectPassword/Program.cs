using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrectPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            int i=0;
            PasswordGenerator pg = new PasswordGenerator();
            Dictionary<string, int> openWith = new Dictionary<string, int>();
            do
            {
                string pg1 = pg.getPassword();
                //Console.WriteLine(pg1);

                if (! openWith.Keys.Contains(pg1))
                {
                    openWith.Add(pg1, 0);
                }

                openWith[pg1]++;                
            }
            while (i++ < 20000000);
            int z = 0;
         //  Console.WriteLine(openWith.Select(a => a.Value > 1).ToList());
            foreach (var ff in openWith)
            {
                if (ff.Value > 1)
                {
                    Console.WriteLine("password : {0}, count : {1}", ff.Key, ff.Value);
                    z++;
                }
            }

            Console.WriteLine("finish  " + z);
            Console.ReadKey();
        }
    }
}
