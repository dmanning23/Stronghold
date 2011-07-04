using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OStronghold.CharacterFolder;

namespace OStronghold.StrongholdFolder
{
    public class StrongholdLeader : Character
    {
        #region Members

        public DecisionMaker _decisionmaker;

        #endregion

        #region Constructor

        public StrongholdLeader()
            : base()
        {
            _decisionmaker = new DecisionMaker();
            
            _decisionmaker.insertPhenomenon(Consts.stronghold, Consts.granary, subobject.Existence, behaviour.Empty);
        }

        #endregion
    }
}
