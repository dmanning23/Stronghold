using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.Generic
{
    public class Treasury
    {
        #region Members

        private int _gold;

        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }
        
        #endregion

        #region Constructor

        public Treasury()
        {
        }

        public Treasury(int startingGold)
        {
            _gold = startingGold;
        }

        #endregion
    }
}
