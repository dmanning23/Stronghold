using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.GenericFolder
{
    public class Treasury
    {
        #region Members

        private int _gold;

        public int Gold
        {
            get { return _gold; }            
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

        #region Methods

        public void depositGold(int amount)
        {
            Consts.globalEvent.writeEvent(amount + " gold deposited into the Treasury.", Consts.eventType.Stronghold, Consts.EVENT_DEBUG_MIN);
            _gold += amount;
        }

        public void withdrawGold(int amount)
        {
            Consts.globalEvent.writeEvent(amount + " gold withdrawn from the Treasury.", Consts.eventType.Stronghold, Consts.EVENT_DEBUG_MIN);
            _gold -= amount;
        }

        public bool haveEnoughToWithdraw(int amount)
        {
            return (amount <= _gold);
        }

        #endregion
    }
}
