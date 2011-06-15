using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OStronghold.Generic;

namespace OStronghold
{
    public class CharacterClass
    {
        #region Members
        
        public string _name; //character name
        public int _id; //character id
        public int _age; //character age
        public Consts.gender _gender; //character gender        
        public int _fame; //how famous the character is
        public CharacterMindsetClass _mindset; //character personality
        public CharacterBodyNeeds _bodyneeds; //character body needs (i.e: hunger, sleep, etc)        
        public Gametime _currentActionFinishTime;
        public CharacterHealth _health; //character health related
        public MyPriorityQueue _characterActions;

        #endregion

        #region Constructor

        public CharacterClass()
        {
            _name = "";
            _id = 0;            
            _fame = 0;
            _age = 0;
            _mindset = new CharacterMindsetClass();
            _bodyneeds = new CharacterBodyNeeds();
            _currentActionFinishTime = new Gametime(0, 0, 0);
            _health = new CharacterHealth();
            _characterActions = new MyPriorityQueue(); //the list of actions the character wants to do

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
            _health.staminaUsedThisTick = 2; //eating takes 2 stamina            
        }//character eats

        #endregion

        #region Events

        public void OnHungryEventHandler(object sender, EventArgs e)
        {
            _characterActions.insertBeginningofQueue(new CharacterAction(Consts.characterGeneralActions.Eat, 1));
        }//actions to do when character is hungry

        #endregion

    }
}
