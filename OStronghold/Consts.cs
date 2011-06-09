using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class Consts
    {
        public enum characterState { Idle, Busy, Dead , Undefined}; 
        //Idle - not doing anything
        //Busy - in a middle of an action
        //Dead - not alive
        //Undefined - default when born

        public enum gender { Male, Female };

        public static Random rand = new Random((int)DateTime.Now.Ticks);
    }
}
