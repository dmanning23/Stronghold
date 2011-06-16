using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OStronghold
{
    class Program
    {
        public static Gametime _gametime = new Gametime(0, 0, 0);
        public static StrongholdClass _aStronghold = new StrongholdClass();
        public static Consts _consts = new Consts();

        static void Main(string[] args)
        {                       
            _aStronghold.populate(1);            
            while (true)
            {
                TimeSpan _timespan = new TimeSpan(DateTime.Now.Ticks - _gametime.LastGameTick);
                if (_timespan.TotalSeconds >= Consts.gametickperSecond)
                {
                    _gametime.LastGameTick = DateTime.Now.Ticks;
                    _gametime.incXMinutes(Consts.gametickIncreaseMinutes);
                }
                

                //Console.Clear();
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Game time: " + _gametime.ToString());
                Console.WriteLine();                
                _aStronghold.printPopulation();

                //Thread responsible for find Idle Commoners and making them do something.
                //Thread activateIdleCommonersThread = new Thread(new ThreadStart(_aStronghold.activateIdleCommoners));
                //activateIdleCommonersThread.Start();

                Thread.Sleep(1000*Consts.gametickperSecond);

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
