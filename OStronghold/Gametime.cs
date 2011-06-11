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
            }

        }

        #endregion

        #region Constructor

        public Gametime()
        {
        }

        public Gametime(int dayvalue, int hourvalue)
        {
            Day = dayvalue;
            Hour = hourvalue;
        }

        public Gametime(int hourvalue)
        {
            Hour = hourvalue;//automatically updates the day value in the hour encapsulation
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
    }//game time
}
