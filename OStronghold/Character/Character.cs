using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OStronghold.Generic;

namespace OStronghold
{
    public class Character
    {
        #region Members
        
        public string _name; //character name
        public int _id; //character id
        public int _age; //character age
        public Consts.gender _gender; //character gender        
        public int _fame; //how famous the character is
        public CharacterMindset _mindset; //character personality
        public CharacterBodyNeeds _bodyneeds; //character body needs (i.e: hunger, sleep, etc)        
        public Gametime _currentActionFinishTime;
        public CharacterHealth _health; //character health related
        public MyPriorityQueue _characterActions; //character actions
        public CharacterInventory _characterinventory; //character inventory
        

        #endregion

        #region Constructor

        public Character()
        {
            _name = "";
            _id = 0;            
            _fame = 0;
            _age = 0;
            _mindset = new CharacterMindset();
            _bodyneeds = new CharacterBodyNeeds();
            _currentActionFinishTime = new Gametime(0, 0, 0);
            _health = new CharacterHealth();
            _characterActions = new MyPriorityQueue(); //the list of actions the character wants to do
            _characterinventory = new CharacterInventory();                   

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

        public LinkedList<InventoryItem> getFoodFromInventory()
        {
            LinkedList<InventoryItem> foods = _characterinventory.searchForItemsByName("Food");
            return (foods);
        }

        public bool eatAction()
        {
            LinkedList<InventoryItem> foods = _characterinventory.retrieveItemInInventory("Food", -1);
            if (foods.Count > 0)
            {
                foreach (InventoryItem food in foods)
                {
                    if (food.Quantity > 0)
                    {
                        food.DeductQuantity(1);
                        if (food.Quantity == 0)
                        {
                            foods.Remove(food);
                        }
                        break;
                    }//if food quantity is greater than 1 then deduct one and put back to inventory
                    //if food quantity is exactly 1 then food is finished and no need to put back to inventory                                        
                }
                _characterinventory.putInInventory(foods);
                return true;
            }//there is food
            else
            {
                return false;
                //character cannot eat
            }
        }//character eats , return true if ate , false if not ate

        #endregion

        #region Events

        public void OnHungryEventHandler(object sender, EventArgs e)
        {
            if (getFoodFromInventory().Count > 0)
            {
                Gametime finishTime = Program._gametime + Consts.actionsData[(int)Consts.characterGeneralActions.Eating]._actionDuration;
                _characterActions.insertItemIntoQueue(new CharacterAction(Consts.characterGeneralActions.Eating, Consts.actionsData[(int)Consts.characterGeneralActions.Eating]._actionPriority, finishTime));
                eatAction();
            }//eat only if there is enough food in inventory otherwise stay hungry
        }//actions to do when character is hungry

        #endregion

    }
}
