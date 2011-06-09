using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    public class CharacterClass
    {
        #region Members

        public string _name;
        public Consts.characterState _state;
        public int _fame;

        #endregion

        #region Constructor

        public CharacterClass()
        {
            _name = "";
            _state = Consts.characterState.Undefined;
            _fame = 0;
        }

        #endregion

    }
}
