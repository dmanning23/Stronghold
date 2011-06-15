using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class CharacterAction
    {
        #region Members

        private Consts.characterGeneralActions _action;
        private int _priority; //the lower the number, the higher the priority

        public Consts.characterGeneralActions Action
        {
            get { return _action; }
        }

        public int Priority
        {
            get { return _priority; }
        }

        #endregion

        #region Constructor

        public CharacterAction()
        {
            _action = Consts.characterGeneralActions.Undefined;
            _priority = -1;
        }

        public CharacterAction(Consts.characterGeneralActions actionValue, int priorityValue)
        {
            _action = actionValue;
            _priority = priorityValue;
        }

        #endregion
    }
}
