using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.Generic
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

        #endregion


    }//status class
}
