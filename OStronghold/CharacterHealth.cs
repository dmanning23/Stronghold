using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class CharacterHealth
    {
        #region Members

        public struct status
        {
            private int current; //current value
            private int max; //max value
            private int regeneration; //how much is regenerated over 1 game tick            

            public int Current
            {
                get { return current; }
                set 
                {                     
                    current = value;
                    if (current > max)
                    {
                        current = max;
                    }
                    if (current < 0)
                    {
                        current = 0;
                    }
                }
            }

            public int Max
            {
                get { return max; }
                set { max = value; }
            }

            public int Regeneration
            {
                get { return regeneration; }
                set { regeneration = value; }
            }
        }

        public status hp;
        public status mp;
        public status stamina;
        public int staminaUsedThisTick;

        #endregion

        #region Constructor

        public CharacterHealth()
        {
            hp.Current = 0;
            hp.Max = 0;
            hp.Regeneration = 0;
            mp.Current = 0;
            mp.Max = 0;
            mp.Regeneration = 0;
            stamina.Current = 0;
            stamina.Max = 0;
            stamina.Regeneration = 0;
            staminaUsedThisTick = 0;
        }

        #endregion

        #region Methods

        public void defineHP(int value, int regenValue)
        {
            hp.Max = value;
            hp.Current = value;
            hp.Regeneration = regenValue;
        }

        public void defineMP(int value, int regenValue)
        {
            mp.Max = value;
            mp.Current = value;
            mp.Regeneration = regenValue;
        }

        public void defineStamina(int value, int regenValue)
        {
            stamina.Max = value;
            stamina.Current = value;
            stamina.Regeneration = regenValue;
        }        

        #endregion
    }//character health related
}
