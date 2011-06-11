using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class Gametime
    {
        #region Members

        private int _day;
        private int _hour;

        public int Day
        {
            get { return _day; }
            set { _day = value; }
        }
        public int Hour
        {
            get { return _hour; }
            set 
            {                
                _hour = value;
                while (_hour >= 24)
                {                                       
                    _day++;
                    _hour -= 24;
                }
                OnHourPassed(System.EventArgs.Empty);
            }

        }

        #endregion

        #region Constructor

        public Gametime()
        {
            _anHourPassedEvent += this.OnHourPassedEventHandler;
        }

        public Gametime(int dayvalue, int hourvalue)
        {
            Day = dayvalue;
            Hour = hourvalue;
            _anHourPassedEvent += this.OnHourPassedEventHandler;
        }

        public Gametime(int hourvalue)
        {
            Hour = hourvalue;//automatically updates the day value in the hour encapsulation
            _anHourPassedEvent += this.OnHourPassedEventHandler;
        }

        #endregion

        #region Methods

        public void CopyGameTime(Gametime target)
        {
            _day = target.Day;
            _hour = target.Hour;
        }

        public int getTotalHours()
        {
            return ((_day * 24) + _hour);
        }

        #endregion

        #region Operators overload

        public override string ToString()
        {
            return ("Day " + _day + " Hour " + _hour);
        }

        public static Gametime operator +(Gametime t1, Gametime t2)
        {
            return (new Gametime(t1.Day + t2.Day, t1.Hour + t2.Hour));
        }

        public static Gametime operator +(Gametime t1, int hours)
        {
            return (new Gametime(t1.Day, t1.Hour + hours));
        }

        public static Gametime operator -(Gametime t1, Gametime t2)
        {
            int t1Hours = t1.getTotalHours();
            int t2Hours = t2.getTotalHours();

            if (t1Hours >= t2Hours)
            {
                return new Gametime(t1Hours - t2Hours);
            }
            else return new Gametime(t2Hours - t1Hours); 

        }

        public static bool operator ==(Gametime t1, Gametime t2)
        {
            return (t1.Day == t2.Day && t1.Hour == t2.Hour);
        }

        public static bool operator !=(Gametime t1, Gametime t2)
        {
            return !(t1 == t2);
        }

        public static bool operator >=(Gametime t1, Gametime t2)
        {
            return (t1.getTotalHours() >= t2.getTotalHours());
        }

        public static bool operator <(Gametime t1, Gametime t2)
        {
            return !(t1 >= t2);
        }

        public static bool operator <=(Gametime t1, Gametime t2)
        {
            return (t1.getTotalHours() <= t2.getTotalHours());
        }

        public static bool operator >(Gametime t1, Gametime t2)
        {
            return !(t1 <= t2);
        }


        public override bool Equals(object obj)
        {
            if (obj is Gametime)
            {
                return (this == (Gametime)obj);
            }
            return false;
        }             

        #endregion

        #region Events

        public delegate void hourPassedHandler(object sender, EventArgs e);
        public event hourPassedHandler _anHourPassedEvent;

        protected virtual void OnHourPassed(System.EventArgs e)
        {
            if (_anHourPassedEvent != null)
            {
                _anHourPassedEvent(this, e);
            }
        }

        public void OnHourPassedEventHandler(object sender, EventArgs e)
        {
            for (int x = 0; x < Program._aStronghold._stats.currentPopulation; x++)
            {
                CharacterClass person = ((CharacterClass)Program._aStronghold._commoners[x]);

                if (person._currentActionFinishTime > Program._gametime)
                {
                    //wait
                }//person is doing something - wait until action is over
                else
                {
                    switch (person._hungerflowaction)
                    {
                        //character done being idle - need to check when he/she is going to become hungry
                        case Consts.characterHungerFlowActions.Idle:                            
                            if (Program._gametime >= (person._bodyneeds.LastAteTime + (int)person._bodyneeds.HungryTimer))
                            {
                                person._hungerflowaction = Consts.characterHungerFlowActions.GettingHungry;
                                person._bodyneeds.HungerState = Consts.hungerState.Normal;
                                person._currentActionFinishTime = Program._gametime + Program._consts.characterHungerFlowActionDuration[(int)Consts.characterHungerFlowActions.GettingHungry];
                            }//person is getting hungry since he passed the hungry timer
                            break;
                        //character done getting hungry - need to check if hungry timer passed
                        case Consts.characterHungerFlowActions.GettingHungry:
                            if (Program._gametime > (person._bodyneeds.LastAteTime + (int)person._bodyneeds.HungryTimer))
                            {
                                person._bodyneeds.HungerState = Consts.hungerState.Hungry;
                            }
                            break;
                        //character is done looking for food - character will start eating if there are enough food, if not he/she will have to wait til next hour
                        case Consts.characterHungerFlowActions.LookingForFood:
                            if (Program._aStronghold._buildings.foodStorage > 0)
                            {
                                person.eatAction();
                            }
                            break;
                        //character is done eating - hunger state changed and person will be finishing eating
                        case Consts.characterHungerFlowActions.Eating:
                            switch (person._bodyneeds.HungerState)
                            {
                                case Consts.hungerState.Hungry:
                                    person._bodyneeds.HungerState = Consts.hungerState.Full;
                                    break;
                                case Consts.hungerState.Normal:
                                    person._bodyneeds.HungerState = Consts.hungerState.Full;
                                    break;
                            }
                            person._hungerflowaction = Consts.characterHungerFlowActions.FinishedEating;
                            person._currentActionFinishTime = Program._gametime + +Program._consts.characterHungerFlowActionDuration[(int)Consts.characterHungerFlowActions.FinishedEating];
                            break;
                        //character is done finishing eating - update last ate time and hunger state
                        case Consts.characterHungerFlowActions.FinishedEating:
                            person._bodyneeds.LastAteTime.CopyGameTime(Program._gametime);
                            Program._aStronghold._buildings.foodStorage++;
                            person._hungerflowaction = Consts.characterHungerFlowActions.Idle;
                            break;  
                    }
                }//action is finished - do results of the action
            }
        }//every hour passed must perform actions       
        #endregion
    }//game time
}
