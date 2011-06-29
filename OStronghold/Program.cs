
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OStronghold.StrongholdFolder;
using OStronghold.GenericFolder;
using OStronghold.CharacterFolder;

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
                Consts.printMessage("                 Debug Menu         " + Program._gametime);
                Consts.printMessage("------------------------------------------------");
                Consts.printMessage("add farm");
                Consts.printMessage("add granary");
                Consts.printMessage("add|show hut");
                Consts.printMessage("add|show person <number>");
                Consts.printMessage("dump <person|building|job> <id|all>");
                Consts.printMessage("show|suspend|resume|stop game time");
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
                            Consts.printMessage(numberParam + " Person(s) added.");
                            Consts.writeToDebugLog(numberParam + " Person(s) added.");
                        }
                        else if (words[1] == "hut")
                        {
                            _aStronghold.buildHut();
                            Consts.printMessage("Hut added.");
                            Consts.writeToDebugLog("Hut added.");
                        }
                        else if (words[1] == "farm")
                        {
                            _aStronghold.buildFarm();
                            Consts.printMessage("Farm added.");
                            Consts.writeToDebugLog("Farm added.");
                        }
                        else if (words[1] == "granary")
                        {
                            _aStronghold.buildGranary();
                            Consts.printMessage("Granary added.");
                            Consts.writeToDebugLog("Granary added.");
                        }
                        else Consts.printMessage("Invalid command.");
                        break;
                    case "dump":
                        if (words[1] == "person")
                        {
                            if (_aStronghold._commoners.Count == 0)
                            {
                                Consts.printMessage("No population in Stronghold!");
                            }
                            else
                            {
                                if (words[2] == "all")
                                {
                                    for (int i = 1; i <= _aStronghold._commoners.Count; i++)
                                    {                                        
                                        Consts.writeToDebugLog(((Character)_aStronghold._commoners[i]).getCharacterString());
                                    }
                                }
                                else if (words.Length == 3)
                                {
                                    numberParam = Int32.Parse(words[2]);
                                    if (numberParam > Consts.accomodation + _aStronghold._commoners.Count)
                                    {
                                        Consts.printMessage("Invalid command.");
                                    }
                                    else
                                    {
                                        Consts.writeToDebugLog(((Character)_aStronghold._commoners[numberParam]).getCharacterString());
                                    }
                                }
                                else Consts.printMessage("Invalid command.");
                            }
                        }
                        else if (words[1] == "job")
                        {
                            if (_aStronghold._allJobs.Count == 0)
                            {
                                Consts.printMessage("No jobs in Stronghold!");
                            }
                            else
                            {
                                if (words[2] == "all")
                                {
                                    foreach (Job job in _aStronghold._allJobs)
                                    {
                                        Consts.writeToDebugLog(job.getJobString());
                                    }
                                }
                                else if (words.Length == 3)
                                {
                                    numberParam = Int32.Parse(words[2]);
                                    if (numberParam > _aStronghold._allJobs.Count)
                                    {
                                        Consts.printMessage("Invalid command.");
                                    }
                                    else
                                    {
                                        Consts.writeToDebugLog(_aStronghold.searchJobByID(numberParam).getJobString());
                                    }
                                }
                                else Consts.printMessage("Invalid command.");
                            }
                        }
                        else if (words[1] == "building")
                        {
                            if (_aStronghold._buildingsList.Count == 0)
                            {
                                Consts.printMessage("No buildings in Stronghold!");
                            }
                            else
                            {
                                if (words[2] == "all")
                                {
                                    foreach (Building building in _aStronghold._buildingsList)
                                    {
                                        Consts.writeToDebugLog(building.getBuildingString());
                                    }
                                }
                                else if (words.Length == 3)
                                {
                                    numberParam = Int32.Parse(words[2]);
                                    if (numberParam > _aStronghold._buildingsList.Count)
                                    {
                                        Consts.printMessage("Invalid command.");
                                    }
                                    else
                                    {
                                        Consts.writeToDebugLog(_aStronghold.searchBuildingByID(numberParam).getBuildingString());
                                    }
                                }
                                else Consts.printMessage("Invalid command.");
                            }
                        }
                        else Consts.printMessage("Invalid command.");
                        break;
                    case "resume":
                        if (words[1] == "game" && words[2] == "time")
                        {
                            startGameTimeThread.Resume();
                            Consts.printMessage("Game time resumed.");
                        }
                        else Consts.printMessage("Invalid command.");
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
                        else Consts.printMessage("Invalid command."); 
                        break;
                    case "stop":
                        if (words[1] == "game" && words[2] == "time")
                        {
                            startGameTimeThread.Abort();
                            Consts.printMessage("Game time stopped.");
                        }
                        else Consts.printMessage("Invalid command.");
                        break;
                    case "suspend":
                        if (words[1] == "game" && words[2] == "time")
                        {
                            startGameTimeThread.Suspend();
                            Consts.printMessage("Game time suspended.");
                        }
                        else Consts.printMessage("Invalid command.");
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
