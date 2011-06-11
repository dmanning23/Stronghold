using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class Consts
    {
        #region Members

        public enum characterHungerFlowActions { Undefined=1, Idle, GettingHungry, LookingForFood, Eating, FinishedEating, Count};
        //Undefined - default when born
        //Idle - not doing anything                       
        //GettingHungry - going from full -> hungry
        //LookingForFood - searching food
        //Eating - eating food               
        //FinishedEating - done eating and back to idle

        public int[] characterHungerFlowActionDuration = new int[(int)characterHungerFlowActions.Count];

        public enum gender { Male, Female };

        public enum hungerState { Hungry, Normal, Full };
        //Hungry - needs to eat immediately (eating is highest priority)
        //Normal - can perform other actions (eating is medium priority)
        //Full - don't need to eat (eating is lowest priority or no priority)

        public enum hungerTimer { Hungry = 0, Normal = 4, Full = 8 }; //hours           

        public static Random rand = new Random((int)DateTime.Now.Ticks);

        #endregion

        #region Constructor

        public Consts()
        {
            //configures duration for hunger flow actions
            characterHungerFlowActionDuration[(int)characterHungerFlowActions.Undefined] = 0;
            characterHungerFlowActionDuration[(int)characterHungerFlowActions.Idle] = 0;
            characterHungerFlowActionDuration[(int)characterHungerFlowActions.GettingHungry] = 1;
            characterHungerFlowActionDuration[(int)characterHungerFlowActions.LookingForFood] = 1;
            characterHungerFlowActionDuration[(int)characterHungerFlowActions.Eating] = 3;
            characterHungerFlowActionDuration[(int)characterHungerFlowActions.FinishedEating] = 1;
        }

        #endregion
    }
}
