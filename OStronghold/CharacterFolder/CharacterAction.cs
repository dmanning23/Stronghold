using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.CharacterFolder
{
    public class CharacterAction
    {
        #region Members

        private Consts.characterGeneralActions _action;
        private int _priority; //the lower the number, the higher the priority
        private Gametime _finishtime; //when the action should finish

        public Consts.characterGeneralActions Action
        {
            get { return _action; }
        }

        public int Priority
        {
            get { return _priority; }
        }

        public Gametime FinishTime
        {
            get { return _finishtime; }
        }

        #endregion

        #region Constructor

        public CharacterAction()
        {            
            _action = Consts.characterGeneralActions.Undefined;
            _priority = -1;
            _finishtime = new Gametime();            
        }

        public CharacterAction(Consts.characterGeneralActions actionValue, int priorityValue, Gametime finishTimeValue)
        {            
            _action = actionValue;
            _priority = priorityValue;
            _finishtime = new Gametime(finishTimeValue);            
        }

        #endregion
    }
}
