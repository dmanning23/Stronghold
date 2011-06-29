using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OStronghold.GenericFolder;

namespace OStronghold.CharacterFolder
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
        public int _jobID; // job ID , -1 if not working       
        public int _homeID; //home ID - where the person sleeps at night
        public int _locationID; //location ID where the person is currently located

        #endregion

        #region Constructor

        public Character()
        {
            _name = "";
            _id = 0;            
            _fame = 0;
            _age = 0;
            _jobID = -1;
            _mindset = new CharacterMindset();
            _bodyneeds = new CharacterBodyNeeds();
            _currentActionFinishTime = new Gametime(0, 0, 0);
            _health = new CharacterHealth();
            _characterActions = new MyPriorityQueue(); //the list of actions the character wants to do
            _characterinventory = new CharacterInventory();
            _locationID = Consts.stronghold_yard;
            _homeID = Consts.stronghold_yard;

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

        public InventoryItem getFoodFromInventory()
        {
            return _characterinventory.searchForItemByID(Consts.FOOD_ID);
        }

        public bool eatAction()
        {
            InventoryItem food = _characterinventory.retrieveItemInInventory(Consts.FOOD_NAME, -1);

            if (food.Quantity > 0)
            {
                food.DeductQuantity(1);
                if (food.Quantity == 0)
                {
                    _characterinventory.Inventory.Remove(food);
                }
                //if food quantity is greater than 1 then deduct one and put back to inventory
                //if food quantity is exactly 1 then food is finished and no need to put back to inventory                                        
                _characterinventory.putInInventory(food);
                return true;
            }
            else
            {
                return false;
                //character cannot eat
            }
        }//character eats , return true if ate , false if not ate

        public bool applyForJob(int jobID)
        {
            //search if jobID exists
            //check for qualifications for job (minimum requirements?)
            //need confirmation from owner of job.
            //if all successful - character gets job     

            foreach (Job job in Program._aStronghold._allJobs) 
            {
                if (job.JobStatus == Consts.JobStatus.Available && job.JobID == jobID)
                {
                    this._jobID = job.JobID;
                    job.JobStatus = Consts.JobStatus.Taken;
                    job.WorkerID = this._id;
                    return true;
                }
            }
            return false;
        }//applies for job - true = successfully applied, false = failed to apply

        public string getCharacterString()
        {
            string result = "";
            result += "ID: " + this._id + "\n";
            result += "Name: " + this._name + "\n";
            result += "Age: " + this._age + "\n";
            result += "Gender: " + this._gender + "\n";
            result += "Fame: " + this._fame + "\n";
            result += _mindset.getCharacterMindsetString();
            result += _bodyneeds.getCharacterBodyNeedsString();
            result += "Current Action Finish time: " + _currentActionFinishTime.ToString() + "\n";
            result += _health.getCharacterHealthString();
            result += "Character actions: \n";            
            foreach (CharacterAction action in this._characterActions)
            {
                result += action.Action + " (" + action.Priority + ") " + action.FinishTime + "\n";
            }
            result += "Character inventory: \n";
            result += "capacity: " + _characterinventory.CurrentInventoryCapacity + "/" + _characterinventory.MaxInventoryCapacity + "\n";
            foreach (InventoryItem item in this._characterinventory.Inventory)
            {
                result += item.getInventoryItemString();
            }
            result += "Job ID: " + this._jobID + "\n";
            result += "Location ID: " + this._locationID + "\n";
            result += "Home ID: " + this._homeID + "\n";

            return result;
        }

        public int findPlaceToLive()
        {
            foreach (Building building in Program._aStronghold._buildingsList)
            {
                if (building.Type == Consts.accomodation && building.BuildingState == Consts.buildingState.Built)
                {
                    if (((BuildingForLiving)building).isPopulable(1))
                    {
                        ((BuildingForLiving)building).TenantsID[((BuildingForLiving)building).Tenants.Current] = this._id;
                        ((BuildingForLiving)building).populateLivingBuilding(1);                        
                        return building.BuildingID;                        
                    }
                }//found place to live
            }

            return Consts.stronghold_yard;
        }

        #endregion

        #region Events

        public void OnHungryEventHandler(object sender, EventArgs e)
        {
            if (getFoodFromInventory().Quantity > 0)
            {
                Gametime finishTime = Program._gametime + Consts.actionsData[(int)Consts.characterGeneralActions.Eating]._actionDuration;
                _characterActions.insertItemIntoQueue(new CharacterAction(Consts.characterGeneralActions.Eating, Consts.actionsData[(int)Consts.characterGeneralActions.Eating]._actionPriority, finishTime));
                eatAction();
            }//eat only if there is enough food in inventory otherwise stay hungry
        }//actions to do when character is hungry

        #endregion

    }
}
