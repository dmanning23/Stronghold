
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
        public static Thread startGameTimeThread = new Thread(new ThreadStart(startGameTime));

        public static void startGUI()
        {


            while (true)
            {
                Console.Clear();
                Consts.printMessage("------------------------------------------------");                
                Consts.printMessage("                 Debug Menu");
                Consts.printMessage("------------------------------------------------");                
                Consts.printMessage("<show|suspend|resume|stop> game time");
                Consts.printMessage("<add|show> person <number>");
                Consts.printMessage("<add|show> hut");
                Consts.printMessage("quit");
                Consts.printMessage("");
                Console.Write("Enter input: ");
                string input = Console.ReadLine();
                string[] words = input.Split(' ');
                int numberParam;

                switch (words[0])
                {
                    case "add":
                        if (words[1] == "person")
                        {
                            if (words.Length == 2) numberParam = 1;
                            else numberParam = Int32.Parse(words[2]);
                            _aStronghold.populate(numberParam);
                            Consts.printMessage("Person added.");
                        }
                        else if (words[1] == "hut")
                        {
                            Generic.BuildingForLiving hut = new Generic.BuildingForLiving(Consts.HUT_ID, Consts.HUT_NAME, Consts.HUT_HP, Consts.HUT_COSTTOBUILD, new Generic.Status(1, Consts.HUT_MAXLEVEL), Program._gametime, Program._gametime, new Generic.Status(0, 10));
                            _aStronghold._buildingsList.AddLast(hut);
                            Consts.printMessage("Hut added.");
                        }
                        break;
                    case "resume":
                        if (words[1] == "game" && words[2] == "time")
                        {
                            startGameTimeThread.Resume();
                            Consts.printMessage("Game time resumed.");
                        }
                        break;
                    case "show":
                        if (words[1] == "game" && words[2] == "time")
                        {                            
                            Consts.printMessage(_gametime.ToString());
                        }
                        else if (words[1] == "person")
                        {
                            numberParam = Int32.Parse(words[2]);
                            _aStronghold.printPerson(numberParam);                            
                        }
                        break;
                    case "stop":
                        if (words[1] == "game" && words[2] == "time")
                        {
                            startGameTimeThread.Abort();
                            Consts.printMessage("Game time stopped.");
                        }
                        break;
                    case "suspend":
                        if (words[1] == "game" && words[2] == "time")
                        {
                            startGameTimeThread.Suspend();
                            Consts.printMessage("Game time suspended.");
                        }
                        break;
                    case "quit":
                        break;
                        
                }

                Thread.Sleep(500);
            }
        
        }

        public static void startGameTime()
        {
            while (true)
            {
                TimeSpan _timespan = new TimeSpan(DateTime.Now.Ticks - _gametime.LastGameTick);
                if (_timespan.TotalSeconds >= Consts.gametickperSecond)
                {
                    _gametime.LastGameTick = DateTime.Now.Ticks;
                    _gametime.incXMinutes(Consts.gametickIncreaseMinutes);
                }


                //Thread responsible for find Idle Commoners and making them do something.
                
                
                Thread.Sleep(1000*Consts.gametickperSecond);               

                //update game time according to ticks
            }
        }

        static void Main(string[] args)
        {
            startGameTimeThread.Start();
            startGUI();            
        }
    }
}
