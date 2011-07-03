using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using OStronghold.CharacterFolder;
using OStronghold.GenericFolder;
using OStronghold.StrongholdFolder;

namespace OStronghold
{
    public class Consts
    {
        #region Members

        #region action related
        public enum characterGeneralActions
        {
            Undefined = 1, 
            BuyingFood,
            Eating,
            Idle,           
            LookingForPlaceToLive,
            LookingForWork,
            Sleeping,
            Working,
            Count
        };//generic character actions

        public struct actionStruct
        {
            public int _actionDuration; //in minutes
            public int _actionPriority; //the lower the number and higher the priority
        }
        public static actionStruct[] actionsData;
        #endregion

        #region sleep related
        public enum sleepState { MustSleep, Awake };
        //MustSleep - must sleep - stops everything
        //Awake - normal awake
        #endregion

        public enum gender { Male, Female };
        public enum healthState { Dead, Alive };

        #region food related
        public enum hungerState { Hungry, Normal, Full ,JustAte};        
        //Hungry - needs to eat immediately (eating is highest priority)
        //Normal - can perform other actions (eating is medium priority)
        //Full - don't need to eat (eating is lowest priority or no priority)
        //JustAte - just ate

        public enum hungerTimer { Hungry = 0, Normal = 240, Full = 420 }; //minutes

        #endregion

        public enum sleepTimer { Awake = 1080 };

        public static Random rand = new Random((int)DateTime.Now.Ticks);

        #region time related

        public static int gametickperSecond = 1; //how many real time seconds are there in a game tick
        public static int gametickIncreaseMinutes = 60; //how many game minutes are increased in every game tick
        public static int oneHour = 60;
        public static int halfDay = 720;
        public static int oneDay = 1440;
        public static int oneMonth = 43200;

        #endregion

        #region inventory related

        public static int startCharInventoryMaxCapacity = 1000;

        #endregion

        #region building related

        public enum buildingState { Planned, UnderConstruction, Built, Destroyed };

        #endregion

        #region job related

        public enum JobStatus { Available, Taken , Closed};
        public static int noworker = -1;

        public static string farmerName = "Farmer";
        public static int farmerJobDuration = oneMonth;
        public static int farmerPayroll = 3;
        public static int numberOfFarmersPerFarm = 5;
        public static Gametime farmerBeginTime;
        public static Gametime farmerEndTime;
        public static int numberOfFoodProducedPerFarmer = 6;

        public static string granaryKeeperName = "Granary Keeper";
        public static int granaryKeeperJobDuration = oneMonth;
        public static int granaryKeeperPayroll = 5;
        public static int numberOfGranaryKeepersPerGranary = 1;
        public static Gametime granaryKeeperBeginTime;
        public static Gametime granaryKeeperEndTime;
        public static int granaryMaxInventory = 1000;

        #endregion

        #region ID numbers reserved (1-99)
        #endregion
        #region ID types for buildings (100 -> 500)

        public static int hut = 100;
        public static int farm = 101;
        public static int granary = 102;

        public static int stronghold_treasury = 498;
        public static int stronghold_yard = 499;
        public static int stronghold = 500;        


        #endregion
        #region ID numbers for inventory (1000 -> 5000)

        public static int GOLD_ID = 1000;
        public static string GOLD_NAME = "Gold";
        public static double GOLD_WEIGHT = 0.1;

        public static int FOOD_ID = 1001;
        public static string FOOD_NAME = "Food";
        public static double FOOD_WEIGHT = 0.1;

        #endregion

        #region Building information

        #region hut
        public static string hut_name = "Hut";
        public static Status hut_hp;
        public static int hut_costtobuild = 25;
        public static int hut_maxlevel = 5;
        public static int hut_buildtime = halfDay;
        public static int hut_maxtenants = 10;
        #endregion

        #region farm
        public static string farm_name = "Farm";
        public static Status farm_hp;        
        public static int farm_costtobuild = 20;
        public static int farm_maxlevel = 3;
        public static int farm_buildtime = halfDay;
        #endregion

        #region granary
        public static string granary_name = "Granary";
        public static Status granary_hp;
        public static int granary_costtobuild = 50;
        public static int granary_maxlevel = 2;
        public static int granary_buildtime = oneDay;
        #endregion

        #endregion

        #region Economy prices (in gold)

        public static int FOOD_PRICE = 1;

        #endregion

        #region Events related

        public enum eventType { Character, Building , Stronghold};

        public static GenericEvent globalEvent;
        public static GenericEventHandlerClass globalEventHandler;
        public static int EVENT_DEBUG_MIN = 1;
        public static int EVENT_DEBUG_MINPLUS = 2; 
        public static int EVENT_DEBUG_NORMAL = 3;
        public static int EVENT_DEBUG_NORMALPLUS = 4; 
        public static int EVENT_DEBUG_MAX = 5; 

        public static int EVENT_DEBUG_LEVEL = 3; //1=min, 5=max
        public static int DEBUG_LOG = 0;

        #endregion

        #region streamwriter

        public static StreamWriter debugSW;
        public static StreamWriter characterEventSW;
        public static StreamWriter buildingEventSW;
        public static StreamWriter strongholdEventSW;

        #endregion
        #endregion

        #region Constructor

        public Consts()
        {
            actionsData = new actionStruct[(int)Consts.characterGeneralActions.Count];
            actionsData[(int)Consts.characterGeneralActions.BuyingFood]._actionDuration = 60;
            actionsData[(int)Consts.characterGeneralActions.BuyingFood]._actionPriority = 4;
            actionsData[(int)Consts.characterGeneralActions.Eating]._actionDuration = 60;
            actionsData[(int)Consts.characterGeneralActions.Eating]._actionPriority = 5;
            actionsData[(int)Consts.characterGeneralActions.LookingForPlaceToLive]._actionPriority = 10;
            actionsData[(int)Consts.characterGeneralActions.Sleeping]._actionDuration = 480;
            actionsData[(int)Consts.characterGeneralActions.Sleeping]._actionPriority = 20;            
            actionsData[(int)Consts.characterGeneralActions.Working]._actionPriority = 50;
            actionsData[(int)Consts.characterGeneralActions.Idle]._actionDuration = 0;
            actionsData[(int)Consts.characterGeneralActions.Idle]._actionPriority = 99;

            hut_hp = new Status(100);
            farm_hp = new Status(50);
            granary_hp = new Status(200);

            debugSW = new StreamWriter(@"..\\..\\logs\\debug.log");
            characterEventSW = new StreamWriter(@"..\\..\\logs\\characterEvents.log");
            buildingEventSW = new StreamWriter(@"..\\..\\logs\\buildingEvents.log");
            strongholdEventSW = new StreamWriter(@"..\\..\\logs\\strongholdEvents.log");

            farmerBeginTime = new Gametime(0, 5, 0);
            farmerEndTime = new Gametime(0, 15, 0);

            granaryKeeperBeginTime = new Gametime(0, 8, 0);
            granaryKeeperEndTime = new Gametime(0, 20, 0);

            //create global events
            globalEvent = new GenericEvent();
            globalEventHandler = new GenericEventHandlerClass(globalEvent);
        }

        #endregion

        #region Methods

        public static void printMessage(string message)
        {
            Console.WriteLine(message);            
        }

        public static void writeToDebugLog(string message)
        {
            if (Consts.DEBUG_LOG == 0)
            {
                debugSW.WriteLine(message);
                debugSW.Flush();
            }
        }

        public static void writeToCharacterEventLog(string message)
        {
            characterEventSW.WriteLine("[" + Program._gametime + "]: " + message);
            characterEventSW.Flush();
        }

        public static void writeToBuildingEventLog(string message)
        {
            buildingEventSW.WriteLine("[" + Program._gametime + "]: " + message);
            buildingEventSW.Flush();
        }

        public static void writeToStrongholdEventLog(string message)
        {
            strongholdEventSW.WriteLine("[" + Program._gametime + "]: " + message);
            strongholdEventSW.Flush();
        }

        #endregion
    }
}
