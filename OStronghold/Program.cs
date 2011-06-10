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

            aStronghold.populate(5);

            while (true)
            {
                Console.Clear();
                aStronghold.printPopulation();
                Thread.Sleep(1000);

                //Thread responsible for find Idle Citizens and making them do something.
                Thread activateIdleCitizensThread = new Thread(new ThreadStart(aStronghold.activateIdleCommoners));
                activateIdleCitizensThread.Start();
            }

        }
    }
}
