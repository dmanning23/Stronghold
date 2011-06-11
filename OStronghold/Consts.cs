using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class Consts
    {
        public enum characterState { Idle, Dead , Undefined, Eating, LookingForFood}; 
        //Idle - not doing anything       
        //Dead - not alive
        //Undefined - default when born
        //Eating - eating food
        //LookingForFood - searching food

        public enum gender { Male, Female };

        public enum hungerState { Hungry, Normal, Full };
        //Hungry - needs to eat immediately (eating is highest priority)
        //Normal - can perform other actions (eating is medium priority)
        //Full - don't need to eat (eating is lowest priority or no priority)

        public enum hungerTimer { Hungry = 0, Normal = 4, Full = 8 }; //hours           

        public static Random rand = new Random((int)DateTime.Now.Ticks);
    }
}
