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
        public Consts.characterHungerFlowActions _hungerflowaction; //what is the character currently doing (hunger-wise)
        public int _fame; //how famous the character is
        public CharacterMindsetClass _mindset; //character personality
        public CharacterBodyNeeds _bodyneeds; //character body needs (i.e: hunger, sleep, etc)        
        public Gametime _currentActionFinishTime;
        public CharacterHealth _health; //character health related

        #endregion

        #region Constructor

        public CharacterClass()
        {
            _name = "";
            _id = 0;
            _hungerflowaction = Consts.characterHungerFlowActions.Undefined;
            _fame = 0;
            _age = 0;
            _mindset = new CharacterMindsetClass();
            _bodyneeds = new CharacterBodyNeeds();
            _currentActionFinishTime = new Gametime(0, 0);
            _health = new CharacterHealth();

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
            Program._aStronghold._buildings.foodStorage--;
            _hungerflowaction = Consts.characterHungerFlowActions.Eating;
            _currentActionFinishTime = Program._gametime + +Program._consts.characterHungerFlowActionDuration[(int)Consts.characterHungerFlowActions.Eating];      
        }//character eats

        #endregion

        #region Events

        public void OnHungryEventHandler(object sender, EventArgs e)
        {            
            _hungerflowaction = Consts.characterHungerFlowActions.LookingForFood;
            _currentActionFinishTime = Program._gametime + +Program._consts.characterHungerFlowActionDuration[(int)Consts.characterHungerFlowActions.LookingForFood];          
        }//actions to do when character is hungry

        #endregion

    }
}
