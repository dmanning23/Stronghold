using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class Gametime
    {
        #region Members

        private long _lastgametick;
        private long _elapsedticks;

        private int _minute;
        private int _day;
        private int _hour;

        public long LastGameTick
        {
            get { return _lastgametick; }
            set { _lastgametick = value; }
        }
        public long ElapsedTicks
        {
            get { return _elapsedticks; }
            set { _elapsedticks = value; }
        }

        public int Minute
        {
            get { return _minute; }
            set 
            { 
                _minute = value;
                while (_minute >= 60)
                {
                    incOneHour();
                    _minute -= 60;
                }
            }
        }
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
                    Day++;
                    _hour -= 24;
                }                
            }

        }

        #endregion

        #region Constructor

        public Gametime()
        {
            _lastgametick = DateTime.Now.Ticks;
            _gametickPassedEvent += this.OnGameTickPassedHandler;
            _anHourPassedEvent += this.OnHourPassedEventHandler;
        }        

        public Gametime(int dayvalue, int hourvalue)
        {
            _lastgametick = DateTime.Now.Ticks;
            Minute = 0;
            Day = dayvalue;
            Hour = hourvalue;
            _anHourPassedEvent += this.OnHourPassedEventHandler;
            _gametickPassedEvent += this.OnGameTickPassedHandler;
        }

        public Gametime(int minvalue)
        {
            _lastgametick = DateTime.Now.Ticks;
            Minute = minvalue;//automatically updates the day value in the hour encapsulation
            _anHourPassedEvent += this.OnHourPassedEventHandler;
            _gametickPassedEvent += this.OnGameTickPassedHandler;
        }

        public Gametime(int dayvalue, int hourvalue, int minutevalue)
        {
            _lastgametick = DateTime.Now.Ticks;
            Minute = minutevalue;
            Day = dayvalue;
            Hour = hourvalue;
            _anHourPassedEvent += this.OnHourPassedEventHandler;
            _gametickPassedEvent += this.OnGameTickPassedHandler;
        }

        #endregion

        #region Methods

        public void CopyGameTime(Gametime target)
        {
            _minute = target.Minute;
            _day = target.Day;
            _hour = target.Hour;
        }

        public int getTotalHours()
        {
            return ((_day * 24) + _hour);
        }

        public int getTotalMinutes()
        {
            return (getTotalHours() * 60);
        }

        public void incXMinutes(int minutes)
        {
            Minute += minutes;
        }

        public void incOneHour()
        {
            Hour++;
            OnHourPassed(System.EventArgs.Empty);
        }

        #endregion

        #region Operators overload

        public override string ToString()
        {
            return ("Day " + _day + " Hour " + _hour + " Minutes " + _minute);
        }

        public static Gametime operator +(Gametime t1, Gametime t2)
        {
            return (new Gametime(t1.Day + t2.Day, t1.Hour + t2.Hour, t1.Minute + t2.Minute));
        }

        public static Gametime operator +(Gametime t1, int minutes)
        {
            return (new Gametime(t1.getTotalMinutes() + minutes));
        }

        public static Gametime operator -(Gametime t1, Gametime t2)
        {
            int t1Minutes = t1.getTotalMinutes();
            int t2Minutes = t2.getTotalMinutes();

            if (t1Minutes >= t2Minutes)
            {
                return new Gametime(t1Minutes - t2Minutes);
            }
            else return new Gametime(t2Minutes - t1Minutes); 

        }

        public static bool operator ==(Gametime t1, Gametime t2)
        {
            return (t1.Day == t2.Day && t1.Hour == t2.Hour && t1.Minute == t2.Minute);
        }

        public static bool operator !=(Gametime t1, Gametime t2)
        {
            return !(t1 == t2);
        }

        public static bool operator >=(Gametime t1, Gametime t2)
        {
            return (t1.getTotalMinutes() >= t2.getTotalMinutes());
        }

        public static bool operator <(Gametime t1, Gametime t2)
        {
            return !(t1 >= t2);
        }

        public static bool operator <=(Gametime t1, Gametime t2)
        {
            return (t1.getTotalMinutes() <= t2.getTotalMinutes());
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

        public delegate void gametickPassedHandler(object sender, EventArgs e);
        public event gametickPassedHandler _gametickPassedEvent;

        public delegate void hourPassedHandler(object sender, EventArgs e);
        public event hourPassedHandler _anHourPassedEvent;

        protected virtual void OnGameTickPassed(System.EventArgs e)
        {
            if (_gametickPassedEvent != null)
            {
                _gametickPassedEvent(this, e);
            }
        }

        public void OnGameTickPassedHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Game ticked.");
            //what to perform on every gametick.
        }


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
