using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OStronghold
{
    public class CharacterClass
    {
        #region Members

        public string _name; //character name
        public int _id; //character id
        public int _age; //character age
        public Consts.gender _gender; //character gender
        public Consts.characterState _state; //what is the character currently doing
        public int _fame; //how famous the character is
        public CharacterMindsetClass _mindset; //character personality

        #endregion

        #region Constructor

        public CharacterClass()
        {
            _name = "";
            _id = 0;
            _state = Consts.characterState.Undefined;
            _fame = 0;
            _age = 0;
            _mindset = new CharacterMindsetClass();

            //determining gender 50-50
            if (Consts.rand.Next(1, 1000) > 500)
            {
                _gender = Consts.gender.Male;
            }
            else _gender = Consts.gender.Female;
        }

        #endregion

        #region Methods        

        #endregion
        
    }
}
