﻿using System;
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

        public Gametime(Gametime target)
        {
            this.CopyGameTime(target);
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
            OnGameTickPassed(System.EventArgs.Empty);
        }

        private void incOneHour()
        {
            Hour++;            
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
            Generic.InventoryItem item;

            for (int x = 0; x < Program._aStronghold._stats.currentPopulation; x++)
            {
                Character person = ((Character)Program._aStronghold._commoners[x]);

                person._currentActionFinishTime = person._characterActions.Peek().FinishTime;

                  if (person._currentActionFinishTime > Program._gametime)
                {
                    //wait
                }//person is doing something - wait until action is over
                else //person is finished doing action, do results
                {
                    switch (person._characterActions.Peek().Action)
                    {
                        case Consts.characterGeneralActions.Eating:
                            person._bodyneeds.HungerState = Consts.hungerState.JustAte;
                            person._bodyneeds.LastAteTime.CopyGameTime(Program._gametime);
                            person._characterActions.Dequeue(); //action is finished 
                            break;
                        case Consts.characterGeneralActions.Sleeping:
                            person._bodyneeds.SleepState = Consts.sleepState.Awake;
                            person._bodyneeds.LastSleptTime.CopyGameTime(Program._gametime);
                            person._characterActions.Dequeue();
                            break;
                    }                    
                }

                if (person._characterActions.Count > 0)
                {
                    if (person._characterActions.Peek().Action == Consts.characterGeneralActions.Idle)
                    {
                        Gametime finishTime;
                        #region Hunger check
                        //Check if hungry
                        if (person._bodyneeds.HungerState == Consts.hungerState.JustAte)
                        {
                            person._bodyneeds.HungerState = Consts.hungerState.Full;
                        }//just ate last round, this round character is full
                        else if (person._bodyneeds.HungerState == Consts.hungerState.Full &&
                            Program._gametime >= person._bodyneeds.LastAteTime + (int)Consts.hungerTimer.Normal)
                        {
                            person._bodyneeds.HungerState = Consts.hungerState.Normal;
                        }//pass normal hunger time
                        else if (person._bodyneeds.HungerState == Consts.hungerState.Normal &&
                            Program._gametime > person._bodyneeds.LastAteTime + (int)Consts.hungerTimer.Full)
                        {
                            person._bodyneeds.HungerState = Consts.hungerState.Hungry;
                        }//pass hungry hunger time
                        else if (person._bodyneeds.HungerState == Consts.hungerState.Hungry)
                        {
                            person._bodyneeds.HungerState = Consts.hungerState.Hungry;
                        }//if hungry and then remains hungry
                        #endregion

                        #region Sleep check
                        if (Program._gametime > person._bodyneeds.LastSleptTime + (int)Consts.sleepTimer.Awake)
                        {
                            person._bodyneeds.SleepState = Consts.sleepState.MustSleep;
                            finishTime = Program._gametime + Consts.actionsData[(int)Consts.characterGeneralActions.Sleeping]._actionDuration;
                            person._characterActions.insertItemIntoQueue(new CharacterAction(Consts.characterGeneralActions.Sleeping, Consts.actionsData[(int)Consts.characterGeneralActions.Sleeping]._actionPriority,finishTime));                            
                        }                        
                        #endregion

                        #region Employment check
                        if (person._jobID == -1) //&& person wants to look for job)
                        {
                            LinkedList<Generic.Job> listOfAvailableJobs = Program._aStronghold.getAllAvailableJobs();
                            if (listOfAvailableJobs.Count > 0)
                            {
                                person.applyForJob(Program._aStronghold.getAllAvailableJobs().First.Value.JobID); //need to decide how the person determines what job he/she wants to apply for
                            }
                        }//person applies for first job in the list
                        else
                        {
                            Generic.Job job = Program._aStronghold.searchJobByID(person._jobID);
                            if (job != null)
                            {
                                item = person._characterinventory.searchForItemByID(Consts.GOLD_ID);
                                if (item != null)
                                {
                                    item.Quantity += job.Payroll;
                                    person._characterinventory.putInInventory(item);
                                }//person has gold
                                else
                                {
                                    person._characterinventory.putInInventory(new Generic.InventoryItem(Consts.GOLD_NAME, Consts.GOLD_ID, Consts.GOLD_WEIGHT, job.Payroll));
                                }//person doesn't have gold, put gold into inventory
                            }//person is already employed
                        }//person already employed or doesn't want to work
                        #endregion
                    }
                    else if (person._characterActions.Peek().Action == Consts.characterGeneralActions.Eating)
                    {                            
                        person._health.staminaUsedThisTick = 3;                                                
                    }
                    else if (person._characterActions.Peek().Action == Consts.characterGeneralActions.Sleeping)
                    {
                        //sleeping
                    }                
                }

                //person health is updated at the end of each tick
                person._health.hp.Current += person._health.hp.Regeneration;
                person._health.mp.Current += person._health.mp.Regeneration;
                person._health.stamina.Current = person._health.stamina.Current + person._health.stamina.Regeneration - person._health.staminaUsedThisTick;
                person._health.staminaUsedThisTick = 0; //reset the stamina usage at the end of gametick
            }
        }//actions to do in every game tick


        protected virtual void OnHourPassed(System.EventArgs e)
        {
            if (_anHourPassedEvent != null)
            {
                _anHourPassedEvent(this, e);
            }
        }

        public void OnHourPassedEventHandler(object sender, EventArgs e)
        {
            
        }//every hour passed must perform actions       
        #endregion
    }//game time
}
