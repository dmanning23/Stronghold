using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OStronghold
{
    class Program
    {
        public static Gametime _gametime = new Gametime();

        static void Main(string[] args)
        {
            StrongholdClass aStronghold = new StrongholdClass();
            Gametime test = new Gametime(0, 8);
            

            aStronghold.populate(10);

            while (true)
            {
                Console.Clear();                
                Console.WriteLine("Game time: Day " + _gametime.Day + " Hour " + _gametime.Hour);
                Console.WriteLine();
                aStronghold.printPopulation();
                Thread.Sleep(1000);
                _gametime.Hour++;

                //Thread responsible for find Idle Commoners and making them do something.
                Thread activateIdleCommonerssThread = new Thread(new ThreadStart(aStronghold.activateIdleCommoners));
                activateIdleCommonerssThread.Start();

                //update game time according to ticks

            }

        }
    }
}
