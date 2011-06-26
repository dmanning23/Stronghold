using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class CharacterBodyNeeds
    {
        #region Members

        #region eating related
        private struct eatingStruct
        {
            public Consts.hungerState _hungerState; //current hunger state
            public Consts.hungerTimer _hungryTimer; //hungry timer
            public Gametime _lastAteTime;
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
            get { return _eating._lastAteTime; }
            set { _eating._lastAteTime.CopyGameTime(value); }
        }
        #endregion

        #region sleeping related
        private struct sleepingStruct
        {
            public Consts.sleepState _sleepState;
            public Gametime _lastSleptTime;
        }//everything related to sleeping
        private sleepingStruct _sleeping;

        public Consts.sleepState SleepState
        {
            get { return _sleeping._sleepState; }
            set { _sleeping._sleepState = value; }
        }
        public Gametime LastSleptTime
        {
            get { return _sleeping._lastSleptTime; }
            set { _sleeping._lastSleptTime = value; }
        }

        #endregion

        #endregion

        #region Constructor

        public CharacterBodyNeeds()
        {
            _eating._lastAteTime = new Gametime();
            _eating._lastAteTime.CopyGameTime(Program._gametime);
            _sleeping._lastSleptTime = new Gametime();
            _sleeping._lastSleptTime.CopyGameTime(Program._gametime);
        }

        #endregion

        #region Methods

        public string getCharacterBodyNeedsString()
        {
            string result = "";
            result += "Hunger state: " + _eating._hungerState + "\n";
            result += "Hunger timer: " + _eating._hungryTimer + "\n";
            result += "Last ate: " + _eating._lastAteTime + "\n";
            
            return result;
        }

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
