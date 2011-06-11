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
            private int current;
            private int max;

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
        }

        public status hp;
        public status mp;
        public status stamina;        

        #endregion

        #region Constructor

        public CharacterHealth()
        {
            hp.Current = 0;
            hp.Max = 0;
            mp.Current = 0;
            mp.Max = 0;
            stamina.Current = 0;
            stamina.Max = 0;
        }

        #endregion

        #region Methods

        public void defineHP(int value)
        {
            hp.Current = value;
            hp.Max = value;
        }

        public void defineMP(int value)
        {
            mp.Current = value;
            mp.Max = value;
        }

        public void defineStamina(int value)
        {
            stamina.Current = value;
            stamina.Max = value;
        }

        #endregion
    }//character health related
}
