using System;
using System.Collections.Generic;
using System.Linq;
using Stronghold.Windows;

namespace Stronghold
{
	public struct actionHistory
	{
		public CharacterState _action;
		public Gametime _timestamp;
	}

	/// <summary>
	/// Class that represent a single NPC character.
	/// </summary>
	public class Character
	{
		#region Members

		public string _name; //character name
		public int _id; //character id
		public int _age; //character age
		public Gender _gender; //character gender        
		public int _fame; //how famous the character is
		public CharacterMindset _mindset; //character personality
		public CharacterBodyNeeds _bodyneeds; //character body needs (i.e: hunger, sleep, etc)        
		public Gametime _currentActionFinishTime;
		public CharacterHealth _health; //character health related
		public characterActionPriorityQueue _characterActions; //character actions
		public List<actionHistory> _characterPreviousActions; //character previous actions
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
			_characterActions = new characterActionPriorityQueue(); //the list of actions the character wants to do
			_characterPreviousActions = new List<actionHistory>();
			_characterinventory = new CharacterInventory();
			_locationID = Consts.stronghold_yard;
			_homeID = Consts.stronghold_yard;

			//eating events
			_bodyneeds._hungryEvent += this.OnHungryEventHandler; //hungry event listener

			//determining gender 50-50
			if (Consts.rand.Next(1, 1000) > 500)
			{
				_gender = Gender.Male;
			}
			else _gender = Gender.Female;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Retrieve food from character's inventory
		/// </summary>
		/// <returns>
		/// Food inventory item
		/// </returns>
		public InventoryItem getFoodFromInventory()
		{
			return _characterinventory.searchForItemByID(Consts.FOOD_ID);
		}

		public bool eatAction()
		{
			InventoryItem food = _characterinventory.retrieveItemInInventory(Consts.FOOD_NAME, -1);
			int amountOfFoodToEat = 1;

			if (food.Quantity > 0)
			{
				food.DeductQuantity(amountOfFoodToEat);
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

		/// <summary>
		/// Character tries to apply for target job.
		/// </summary>
		/// <param name="jobID">ID of the target job the character wants to apply for</param>
		/// <returns>true if character successfully applied for the job. false if character failed to apply for the job.</returns>
		public bool applyForJob(int jobID)
		{
			//search if jobID exists
			//check for qualifications for job (minimum requirements?)
			//need confirmation from owner of job.
			//if all successful - character gets job     

			foreach (Job job in Program._aStronghold._allJobs)
			{
				if (job.JobStatus == JobStatus.Available && job.JobID == jobID)
				{
					this._jobID = job.JobID;
					job.JobStatus = JobStatus.Taken;
					job.WorkerID = this._id;
					return true;
				}
			}
			return false;
		}//applies for job - true = successfully applied, false = failed to apply

		public void buyFood(int buildingID)
		{
			Building building;
			Program._aStronghold.searchBuildingByID(buildingID, out building);
			InventoryItem gold = this._characterinventory.retrieveItemInInventory(Consts.GOLD_NAME, -1);
			InventoryItem food = this._characterinventory.retrieveItemInInventory(Consts.FOOD_NAME, -1);
			InventoryItem foodInStock = ((BuildingWithJobsAndInventory)building).searchInventoryByID(Consts.FOOD_ID);
			int amountAffordedFood = gold.Quantity / Consts.FOOD_PRICE;
			int amountFoodInStock;

			if (foodInStock != null)
			{
				amountFoodInStock = foodInStock.Quantity;
				//determine how much of food to buy - random at the moment
				int amountToBuy = Consts.rand.Next(1, Math.Min(amountFoodInStock, amountAffordedFood)); //minimum between amount of food in stock and amount affordable


				gold.Quantity -= amountToBuy * Consts.FOOD_PRICE;
				food.Quantity += amountToBuy;
				Program._aStronghold.Treasury.depositGold(amountToBuy * Consts.FOOD_PRICE);
				this._characterinventory.putInInventory(gold);
				this._characterinventory.putInInventory(food);
				((BuildingWithJobsAndInventory)building).removeFromInventory(Consts.FOOD_NAME, amountToBuy);
			}
		}//buy food from building ID

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
			result += "Character previous actions: \n";
			foreach (actionHistory action in this._characterPreviousActions)
			{
				result += action._action.ToString() + " @ " + action._timestamp + "\n";
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
				if (building.Type == Consts.hut && building.BuildingState == BuildingState.Built)
				{
					if (((BuildingForLiving)building).isPopulable(1))
					{
						((BuildingForLiving)building).TenantsID[((BuildingForLiving)building).Tenants.Current] = this._id;
						((BuildingForLiving)building).populateLivingBuilding(1);
						return building.BuildingID;
					}
				}//found place to live
			}

			//no places to live -> causes stronghold leader to build more huts
			if (!Program._aStronghold.hasEnoughPlannedOrConstruction(Consts.hut))  //does not have enough huts planned for all population
			{
				Program._aStronghold._leader._decisionmaker.insertPhenomenon(Consts.stronghold, Consts.hut, subobject.Capacity, behaviour.Full, parameters.None);
			}
			return Consts.stronghold_yard;
		}

		public void addNewPreviousAction(CharacterState newAction)
		{
			actionHistory actionToAdd;

			actionToAdd._action = newAction;
			actionToAdd._timestamp = new Gametime(Program._gametime);

			if (this._characterPreviousActions.Count > 10)
			{
				this._characterPreviousActions.RemoveAt(0);
			}//if there are more than 10 saved previous actions then remove the oldest one
			this._characterPreviousActions.Add(actionToAdd);
		}

		public void delayTargetAction(CharacterState targetAction)
		{
			int index = -2;
			if (targetAction != CharacterState.Idle && _characterActions.actionExistsInQueue(targetAction))
			{
				for (int i = 0; i < _characterActions.Count; i++)
				{
					if (_characterActions.ElementAt(i).Action == targetAction)
					{
						index = i;
					}
				}//finds the index of action

				if (index != -2)
				{
					CharacterAction temp;
					if (_characterActions.ElementAt(index + 1).Action != CharacterState.Idle)
					{
						//_characterActions.                        
					}//if element after the target action is not idle, then swap the two actions
				}
			}
		}//delays target action and allow another action after it to be performed. Cannot be after Idle, cannot delay Idle

		#endregion

		#region Events

		public void OnHungryEventHandler(object sender, EventArgs e)
		{
			Gametime finishTime;
			if (getFoodFromInventory().Quantity > 0)
			{
				finishTime = Program._gametime + Consts.actionsData[(int)CharacterState.Eating]._actionDuration;
				_characterActions.insertItemIntoQueue(new CharacterAction(CharacterState.Eating, Consts.actionsData[(int)CharacterState.Eating]._actionPriority, finishTime));
				eatAction();
			}//eat only if there is enough food in inventory otherwise stay hungry
			else
			{
				if (!_characterActions.actionExistsInQueue(CharacterState.BuyingFood))
				{
					finishTime = Program._gametime + Consts.actionsData[(int)CharacterState.BuyingFood]._actionDuration;
					_characterActions.insertItemIntoQueue(new CharacterAction(CharacterState.BuyingFood, Consts.actionsData[(int)CharacterState.BuyingFood]._actionPriority, finishTime));
				}//if action does not exists yet
			}//go buy food
		}//actions to do when character is hungry

		#endregion
	}
}
