using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OStronghold
{
    class Program
    {
        public static Consts _consts = new Consts();
        public static Gametime _gametime = new Gametime(0, 0, 0);
        public static Stronghold _aStronghold = new Stronghold();        

        static void Main(string[] args)
        {                       
            _aStronghold.populate(5);            
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
                Console.WriteLine("Stronghold GP: " + _aStronghold.Treasury.Gold);
                Console.WriteLine("Jobs available: " + _aStronghold.getAllAvailableJobs().Count);
                _aStronghold.printJobs();
                Console.WriteLine();                
                _aStronghold.printPopulation();

                if (_gametime.Hour == 0)
                {
                    int end = Consts.rand.Next(1, 5);
                    for (int i = 0; i < end; i++)
                    {
                        int jobId = _aStronghold._allJobs.Count + 1;
                        Generic.Job job = new Generic.Job(jobId, 9999, -1, "Farmer#" + jobId, Program._gametime, Program._gametime + Consts.rand.Next(0, 3600), new Gametime(0, Consts.rand.Next(0, 8)), new Gametime(0, Consts.rand.Next(12, 23)), Consts.rand.Next(1, 15), Consts.JobStatus.Available);
                        _aStronghold._allJobs.AddLast(job);
                    }
                }

                //_aStronghold.printStrongholdLeader();                
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
