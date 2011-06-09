using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OStronghold
{
    class Program
    {
        static void Main(string[] args)
        {
            StrongholdClass aStronghold = new StrongholdClass();
            Random rand = new Random((int)DateTime.Now.Ticks);
            Random num = new Random(5);
            int x;

            while (true)
            {                
                Console.WriteLine("Number of idle citizens: " + aStronghold._stats.numberOfIdleCitizens);
                x = rand.Next(1, 1000);
                if (x > 500)
                {
                    aStronghold.populate(num.Next(1, 5));
                }
                Thread.Sleep(1000);
            }            
        }
    }
}
