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
        public CharacterBodyNeeds _bodyneeds; //character body needs (i.e: hunger, sleep, etc)        

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
            _bodyneeds = new CharacterBodyNeeds();            

            //eating events
            _bodyneeds._hungryEvent += this.OnHungryEventHandler; //hungry event listener

            //determining gender 50-50
            if (Consts.rand.Next(1, 1000) > 500)
            {
                _gender = Consts.gender.Male;
            }
            else _gender = Consts.gender.Female;
        }

        #endregion

        #region Methods        

        public void eatAction()
        {
            StrongholdClass._buildings.foodStorage--;
            _state = Consts.characterState.Eating;            
            switch (_bodyneeds.HungerState)
            {
                case Consts.hungerState.Hungry:
                    _bodyneeds.HungerState = Consts.hungerState.Full;                    
                    break;
                case Consts.hungerState.Normal:
                    _bodyneeds.HungerState = Consts.hungerState.Full;                    
                    break;
            }
            _bodyneeds.LastAteTime.CopyGameTime(Program._gametime);
            _state = Consts.characterState.Idle;
            StrongholdClass._buildings.foodStorage++;
        }//character eats and hunger status changes

        #endregion

        #region Events

        public void OnHungryEventHandler(object sender, EventArgs e)
        {            
            if (StrongholdClass._buildings.foodStorage > 0)
            {
                _state = Consts.characterState.LookingForFood;                
                eatAction();                
            }
        }//actions to do when character is hungry

        #endregion

    }
}
