using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.GenericFolder
{
    public class Status
    {
        #region Members

        private int _current;
        private int _max;

        public int Current
        {
            get { return _current; }
            set 
            { 
                _current = value;
                if (_current > _max)
                {
                    _current = _max;
                }
                if (_current < 0)
                {
                    _current = 0;
                }
            }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        #endregion

        #region Constructor

        public Status()
        {
        }

        public Status(int beginningValue)
        {            
            _max = beginningValue;
            _current = _max;            
        }

        public Status(int currentValue, int maxValue)
        {            
            _current = currentValue;
            _max = maxValue;            
        }


        public Status(Status target)
        {            
            Clone(target);            
        }

        #endregion

        #region Methods

        public void Clone(Status targetStatus)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            this.Max = targetStatus.Max;
            this.Current = targetStatus.Current;
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        #endregion

    }//status class
}
