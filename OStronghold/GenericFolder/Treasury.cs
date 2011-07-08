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
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            _gold += amount;
            Consts.globalEvent.writeEvent(amount + " gold deposited into the Treasury. Treasury now has " + this.Gold + " gold.", Consts.eventType.Stronghold, Consts.EVENT_DEBUG_MIN);
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void withdrawGold(int amount)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            _gold -= amount;
            Consts.globalEvent.writeEvent(amount + " gold withdrawn from the Treasury. Treasury now has " + this.Gold + " gold.", Consts.eventType.Stronghold, Consts.EVENT_DEBUG_MIN);
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public bool haveEnoughToWithdraw(int amount)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return (amount <= _gold);
        }

        #endregion
    }
}
