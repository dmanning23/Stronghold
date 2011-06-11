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
        public static StrongholdClass _aStronghold = new StrongholdClass();
        public static Consts _consts = new Consts();

        static void Main(string[] args)
        {            
            Gametime test = new Gametime(0, 7);
            

            _aStronghold.populate(10);

            while (true)
            {
                _gametime = test;

                //Console.Clear();                
                Console.WriteLine("Game time: Day " + _gametime.Day + " Hour " + _gametime.Hour);
                Console.WriteLine();
                _aStronghold.printPopulation();

                //Thread responsible for find Idle Commoners and making them do something.
                //Thread activateIdleCommonersThread = new Thread(new ThreadStart(_aStronghold.activateIdleCommoners));
                //activateIdleCommonersThread.Start();

                Thread.Sleep(1000);
                _gametime.incOneHour();

                //random population generation
                /*if (_aStronghold._stats.currentPopulation <= 20)
                {
                    if (Consts.rand.Next(1, 100) > 50)
                    {
                        _aStronghold.populate(1);
                    }
                }*/

                //update game time according to ticks

            }

        }
    }
}
