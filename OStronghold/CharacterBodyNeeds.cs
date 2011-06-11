using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class CharacterBodyNeeds
    {
        #region Members

        private struct eatingStruct
        {
            public Consts.hungerState _hungerState; //current hunger state
            public Consts.hungerTimer _hungryTimer; //hungry timer
            public Gametime lastAteTime;
        }//everything related to eating
        private eatingStruct _eating;        

        public Consts.hungerState HungerState
        {
            get { return _eating._hungerState; }
            set 
            { 
                _eating._hungerState = value; //sets value
                switch (_eating._hungerState) //sets hungry timer according to the hungerstate
                {
                    case Consts.hungerState.Hungry:                        
                        OnHungry(System.EventArgs.Empty); //fires a hungry event
                        _eating._hungryTimer = Consts.hungerTimer.Hungry;
                        break;
                    case Consts.hungerState.Normal:
                        _eating._hungryTimer = Consts.hungerTimer.Normal;
                        break;
                    case Consts.hungerState.Full:
                        _eating._hungryTimer = Consts.hungerTimer.Full;
                        break;
                }
            }
        }
        public Consts.hungerTimer HungryTimer
        {
            get { return _eating._hungryTimer; }            
        }//readonly
        public Gametime LastAteTime
        {
            get { return _eating.lastAteTime; }
            set { _eating.lastAteTime.CopyGameTime(value); }
        }

        #endregion

        #region Constructor

        public CharacterBodyNeeds()
        {
            _eating.lastAteTime = new Gametime();
        }

        #endregion

        #region Methods
        
        #endregion

        #region Events

        public delegate void HungryHandler(object sender, EventArgs e);
        public event HungryHandler _hungryEvent;

        protected virtual void OnHungry(System.EventArgs e)
        {
            if (_hungryEvent != null)
            {
                _hungryEvent(this, e);
            }
        }

        #endregion

    }//this class relates to all the body needs a character require (hunger, sleep, thirst, etc.)
}
