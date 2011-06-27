using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OStronghold
{
    public class Consts
    {
        #region Members

        #region action related
        public enum characterGeneralActions
        {
            Undefined = 1,            
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

        public static int gametickperSecond = 1; //how many real time seconds are there in a game tick
        public static int gametickIncreaseMinutes = 60; //how many game minutes are increased in every game tick

        #region inventory related

        public static int startCharInventoryMaxCapacity = 1000;

        #endregion

        #region building related

        

        #endregion

        #region job related

        public enum JobStatus { Available, Taken , Closed};

        #endregion

        #region ID numbers reserved (1-99)
        #endregion
        #region ID types for buildings (100 -> 500)

        public static int ACCOMONDATION = 100;

        public static int STRONGHOLD_YARD = 5000;        


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

        public static string HUT_NAME = "Hut";
        public static OStronghold.Generic.Status HUT_HP;
        public static int HUT_COSTTOBUILD = 25;
        public static int HUT_MAXLEVEL = 5;

        #endregion

        #region streamwriter

        public static StreamWriter debugSW;

        #endregion
        #endregion

        #region Constructor

        public Consts()
        {
            actionsData = new actionStruct[(int)Consts.characterGeneralActions.Count];
            actionsData[(int)Consts.characterGeneralActions.Eating]._actionDuration = 60;
            actionsData[(int)Consts.characterGeneralActions.Eating]._actionPriority = 5;
            actionsData[(int)Consts.characterGeneralActions.LookingForPlaceToLive]._actionPriority = 10;
            actionsData[(int)Consts.characterGeneralActions.Sleeping]._actionDuration = 480;
            actionsData[(int)Consts.characterGeneralActions.Sleeping]._actionPriority = 20;            
            actionsData[(int)Consts.characterGeneralActions.Working]._actionPriority = 50;
            actionsData[(int)Consts.characterGeneralActions.Idle]._actionDuration = 0;
            actionsData[(int)Consts.characterGeneralActions.Idle]._actionPriority = 99;

            HUT_HP = new Generic.Status(100);

            debugSW = new StreamWriter(@"..\\..\\logs\\debug.log");
        }

        #endregion

        #region Methods

        public static void printMessage(string message)
        {
            Console.WriteLine(message);            
        }

        public static void writeToDebugLog(string message)
        {            
            debugSW.WriteLine(message);
            debugSW.Flush();
        }

        #endregion
    }
}
