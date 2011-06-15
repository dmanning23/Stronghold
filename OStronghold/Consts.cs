using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class Consts
    {
        #region Members

        public enum characterGeneralActions
        {
            Undefined = 1,
            Idle,
            Eat
        };//generic character actions

        public enum characterSleepFlowActions { Undefined, Awake, Asleep};

        public enum gender { Male, Female };

        public enum hungerState { Hungry, Normal, Full ,JustAte};        
        //Hungry - needs to eat immediately (eating is highest priority)
        //Normal - can perform other actions (eating is medium priority)
        //Full - don't need to eat (eating is lowest priority or no priority)
        //JustAte - just ate

        public enum hungerTimer { Hungry = 0, Normal = 240, Full = 420 }; //minutes

        public static Random rand = new Random((int)DateTime.Now.Ticks);

        public static int gametickperSecond = 2; //how many real time seconds are there in a game tick
        public static int gametickIncreaseMinutes = 60; //how many game minutes are increased in every game tick

        #endregion

        #region Constructor

        public Consts()
        {
        }

        #endregion
    }
}
