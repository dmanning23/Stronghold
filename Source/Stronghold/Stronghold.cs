using System;
using System.Collections;
using System.Collections.Generic;
using Stronghold.Windows;

namespace Stronghold
{
	public class Stronghold
	{
		#region Members

		private Treasury _treasury;

		public Hashtable _commoners; //hashtable to hold the commoners themselves
		public LinkedList<Building> _buildingsList; //list of buildings (each node contains a building type plus quantity)
		public StrongholdLeader _leader;
		public Treasury Treasury
		{
			get { return _treasury; }
		}
		public LinkedList<Job> _allJobs;


		#endregion

		#region Constructor

		public Stronghold()
		{
			_commoners = new Hashtable();
			_treasury = new Treasury(500);
			_leader = new StrongholdLeader();

			//configure leader
			_leader._id = 9999;
			_leader._name = "SHLeader";
			_leader._age = 34;
			_leader._bodyneeds.HungerState = HungerState.Full;
			_leader._bodyneeds.SleepState = SleepState.Awake;
			_leader._health.defineHP(50, 0);
			_leader._health.defineStamina(100, 1);
			_leader._characterActions.insertItemIntoQueue(new CharacterAction(CharacterState.Idle, Consts.actionsData[(int)CharacterState.Idle]._actionPriority, Program._gametime + Consts.actionsData[(int)CharacterState.Idle]._actionDuration));
			_leader._characterinventory.putInInventory(new InventoryItem(Consts.FOOD_NAME, Consts.FOOD_ID, Consts.FOOD_WEIGHT, 10));

			//testing out job
			_allJobs = new LinkedList<Job>();

			//buildings
			_buildingsList = new LinkedList<Building>();

			//order to build employment office
			_leader._decisionmaker.insertPhenomenon(Consts.stronghold, Consts.employmentoffice, subobject.Existence, behaviour.Empty, parameters.BuildImmediate);
			_leader._decisionmaker.insertPhenomenon(Consts.stronghold, Consts.hut, subobject.Existence, behaviour.Empty, parameters.BuildImmediate);
			_leader._decisionmaker.insertPhenomenon(Consts.stronghold, Consts.farm, subobject.Existence, behaviour.Empty, parameters.BuildImmediate);

		}//Constructor

		#endregion

		#region Methods

		public void populate(int numberofCommonersToProduce)
		{
			int count = _commoners.Count;
			for (int i = count + 1; i < count + 1 + numberofCommonersToProduce; i++)
			{
				Character commoner = new Character();
				commoner._id = i;
				commoner._name = "P#" + commoner._id;
				commoner._age = 18;
				commoner._bodyneeds.HungerState = HungerState.Full;
				commoner._bodyneeds.SleepState = SleepState.Awake;
				commoner._health.defineHP(20, 0);
				commoner._health.defineStamina(100, 10);
				commoner._characterActions.insertItemIntoQueue(new CharacterAction(CharacterState.Idle, Consts.actionsData[(int)CharacterState.Idle]._actionPriority, Program._gametime + Consts.actionsData[(int)CharacterState.Idle]._actionDuration));
				commoner._characterinventory.putInInventory(new InventoryItem(Consts.FOOD_NAME, Consts.FOOD_ID, Consts.FOOD_WEIGHT, 1));
				commoner._characterinventory.putInInventory(new InventoryItem(Consts.GOLD_NAME, Consts.GOLD_ID, Consts.GOLD_WEIGHT, 50));

				_commoners.Add(commoner._id, commoner);

			}
		}//Populating by giving birth to x people

		#region Print outputs
		public void printPopulation()
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);

			Character person = new Character();
			Job job;

			for (int i = 0; i < _commoners.Count; i++)
			{
				person = ((Character)_commoners[i]);
				//foreach (characteraction val in person._characteractions)
				//{
				//    //Consts.printMessage(val.action + " (" + val.priority + ") ");
				//}
				job = searchJobByID(person._jobID);
				if (job == null)
				{
					//Consts.printMessage(person._name + " is currently " + person._characterActions.Peek().Action + " and has " + person._characterinventory.searchForItemByID(Consts.GOLD_ID).Quantity + " Gold.");
				}
				else
				{
					//Consts.printMessage(person._name + " is currently " + person._characterActions.Peek().Action + " as a " + job.JobName + " and has " + person._characterinventory.searchForItemByID(Consts.GOLD_ID).Quantity + " Gold (" + person._characterActions.Peek().FinishTime + ")");
				}
			}
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
		}//Prints in output all the commoner information

		public void printPerson(int ID)
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);

			Character person = ((Character)_commoners[ID]);
			Job job = searchJobByID(person._jobID);

			if (job == null)
			{
				//Consts.printMessage(person._name + " is currently " + person._characterActions.Peek().Action + " and has " + person._characterinventory.searchForItemByID(Consts.GOLD_ID).Quantity + " Gold.");
			}
			else
			{
				//Consts.printMessage(person._name + " is currently " + person._characterActions.Peek().Action + " as a " + job.JobName + " and has " + person._characterinventory.searchForItemByID(Consts.GOLD_ID).Quantity + " Gold (" + person._characterActions.Peek().FinishTime + ")");
			}
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
		}

		public void printJobs()
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);

			foreach (Job job in _allJobs)
			{
				if (job.JobStatus == JobStatus.Available)
				{
					//Consts.printMessage("Name: " + job.JobName + " Status: " + job.JobStatus + " Payroll: " + job.Payroll + " End date: " + job.EndDate);
				}
			}
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
		}
		#endregion

		#region Searches
		public Job searchJobByID(int jobID)
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);

			foreach (Job job in _allJobs)
			{
				if (job.JobID == jobID)
				{
					//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
					return job;
				}
			}
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			return null;
		}//search job according to id

		public Job searchFirstAvailableJobByName(string jobName)
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			foreach (Job job in _allJobs)
			{
				if ((String.Compare(job.JobName, jobName) == 0) && job.JobStatus == JobStatus.Available)
				{
					//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
					return job;
				}
			}
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			return null;
		}//search first available job according to id

		public void searchBuildingByID(int buildingID, out Building result)
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);

			foreach (Building building in _buildingsList)
			{
				if (building.BuildingID == buildingID)
				{
					result = building;
					//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
					return;
				}
			}
			result = null;
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
		}//search building according to id

		public LinkedList<Building> searchBuildingsByType(int buildingType)
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);

			LinkedList<Building> results = new LinkedList<Building>();

			foreach (Building building in _buildingsList)
			{
				if (building.Type == buildingType)
				{
					results.AddLast(building);
				}//found building with right type - add to result link list
			}
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			return results;
		}//search building by type

		public LinkedList<Building> searchBuildingsByBuildingState(BuildingState state)
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			LinkedList<Building> results = new LinkedList<Building>();
			foreach (Building building in _buildingsList)
			{
				if (building.BuildingState == state)
				{
					results.AddLast(building);
				}//found building with right building state
			}
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			return results;
		}




		#endregion

		public LinkedList<Job> getAllAvailableJobs()
		{
			LinkedList<Job> list = new LinkedList<Job>();
			foreach (Job job in _allJobs)
			{
				if (job.JobStatus == JobStatus.Available)
				{
					list.AddLast(job);
				}
			}
			return list;
		}//returns list of all jobs with status of available        

		#region Checks

		public bool buildingPlannedToBeBuild(int buildingtype)
		{
			bool buildingPlanned = false;

			foreach (ActionsToDo todo in _leader._decisionmaker.listOfActionsToDo)
			{
				if (todo._action == action.Build &&
					todo._objectTypeID == buildingtype)
				{
					buildingPlanned = true;
				}
			}
			return buildingPlanned;
		}

		public bool hasEnoughPlannedOrConstruction(int buildingType)
		{
			LinkedList<Building> listOfBuildings;
			int totalPotentialCapacity = 0;

			if (buildingType == Consts.hut)
			{
				listOfBuildings = searchBuildingsByType(Consts.hut);
				foreach (Building hut in listOfBuildings)
				{
					totalPotentialCapacity += ((BuildingForLiving)hut).Tenants.Max;
				}//adds all potential tenant space of all huts that are built/under construction/planned
				foreach (ActionsToDo todo in _leader._decisionmaker.listOfActionsToDo)
				{
					if (todo._action == action.Build &&
						todo._objectTypeID == Consts.hut)
					{
						totalPotentialCapacity += Consts.hut_maxtenants;
					}
				}//adds all potential tenant space of all going-to-be-built huts that are already on the list of actions to do
				return (totalPotentialCapacity >= _commoners.Count);
			}
			else if (buildingType == Consts.farm)
			{
				return buildingPlannedToBeBuild(buildingType);
			}
			else if (buildingType == Consts.granary)
			{
				listOfBuildings = searchBuildingsByType(Consts.granary);
				if (listOfBuildings.Count != 0)
				{
					foreach (Building granary in listOfBuildings)
					{
						if (((BuildingWithJobsAndInventory)granary).InventoryCapacity.Current != ((BuildingWithJobsAndInventory)granary).InventoryCapacity.Max)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		public bool farmsHasAvailableJobs()
		{
			LinkedList<Building> farms = searchBuildingsByType(Consts.farm);

			if (farms == null)
			{
				return false;
			}//no farms
			else
			{
				foreach (Building farm in farms)
				{
					if (((BuildingWithJobsAndInventory)farm).hasAvailableJobs())
					{
						return true;
					}
				}//checks in all farms if there are any jobs available
			}
			return false;
		}

		public bool hasAtLeastOneBuilderEmployed()
		{
			foreach (Job job in _allJobs)
			{
				if (job.JobStatus == JobStatus.Taken && (String.Compare(job.JobName, Consts.builderName) == 0))
				{
					return true;
				}//builder job is taken
			}
			return false;
		}

		#endregion

		#endregion

		#region Methods for constructing buildings

		public int buildEmploymentOffice(int buildTime)
		{
			int buildingID = -1;

			Job builderJob;
			int[] builderJobs = new int[Consts.numberOfInitialBuilders];

			buildingID = Program._aStronghold._buildingsList.Count + 1;

			if (Treasury.haveEnoughToWithdraw(Consts.employmentoffice_costtobuild))
			{
				for (int i = 0; i < Consts.numberOfInitialBuilders; i++)
				{
					builderJob = Job_creater.createBuilderJob(buildingID);
					builderJobs[i] = builderJob.JobID;
					_allJobs.AddLast(builderJob);
				} //creates jobs for builders


				BuildingWithJobsAndInventory employmentoffice =
					new BuildingWithJobsAndInventory(buildingID,
													 _leader._id,
													 Consts.employmentoffice,
													 Consts.employmentoffice_name,
													 Consts.employmentoffice_hp,
													 Consts.employmentoffice_costtobuild,
													 new Status(1, Consts.employmentoffice_maxlevel),
													 Program._gametime,
													 buildTime,
													 builderJobs,
													 null,
													 BuildingState.Planned,
													 new Status(0, 0));
				Program._aStronghold._buildingsList.AddFirst(employmentoffice);
				Treasury.withdrawGold(Consts.employmentoffice_costtobuild);
			}//have enough money to build employment office
			return buildingID;
		}

		public int buildHut(int buildTime)
		{
			int buildingID = -1;

			if (Treasury.haveEnoughToWithdraw(Consts.hut_costtobuild))
			{
				buildingID = Program._aStronghold._buildingsList.Count + 1;
				BuildingForLiving hut =
					new BuildingForLiving(buildingID, //building ID
										  _leader._id, //owner ID
										  Consts.hut, //building type
										  Consts.hut_name, //building name
										  Consts.hut_hp, //building HP
										  Consts.hut_costtobuild, //building cost to build
										  new Status(1, Consts.hut_maxlevel), //building level
										  Program._gametime, //building start build time
										  buildTime, //building build time
										  new Status(0, Consts.hut_maxtenants), //building tenants
										  new int[Consts.hut_maxtenants],
										  BuildingState.Planned); //building state

				Program._aStronghold._buildingsList.AddLast(hut);
				Treasury.withdrawGold(Consts.hut_costtobuild);
			}//have enough money to build hut
			return buildingID;
		}//returns the building id

		public int buildFarm(int buildTime)
		{
			int buildingID = -1;
			if (Treasury.haveEnoughToWithdraw(Consts.farm_costtobuild))
			{
				Job farmerJob;
				int[] farmJobs = new int[Consts.numberOfFarmersPerFarm];

				buildingID = Program._aStronghold._buildingsList.Count + 1;

				for (int i = 0; i < Consts.numberOfFarmersPerFarm; i++)
				{
					farmerJob = Job_creater.createFarmerJob(buildingID);
					farmJobs[i] = farmerJob.JobID;
					_allJobs.AddLast(farmerJob);
				} //creates jobs for the farmer

				BuildingWithJobsAndInventory farm =
					new BuildingWithJobsAndInventory(buildingID, //building ID
													 _leader._id, //owner ID
													 Consts.farm, //type
													 Consts.farm_name, //name
													 Consts.farm_hp, //hp
													 Consts.farm_costtobuild, //cost to build
													 new Status(1, Consts.farm_maxlevel), //level
													 Program._gametime, //start build time
													 buildTime,  //build time 
													 farmJobs,//jobs
													 null,//inventory
													 BuildingState.Planned,
													 new Status(0, 0));

				Program._aStronghold._buildingsList.AddLast(farm);
				Treasury.withdrawGold(Consts.farm_costtobuild);
			}//have enough money to build farm
			return buildingID;
		}//returns building ID

		public int buildGranary(int buildTime)
		{
			int buildingID = -1;
			if (Treasury.haveEnoughToWithdraw(Consts.granary_costtobuild))
			{
				Job granaryKeeperJob;
				int[] granaryJobs = new int[Consts.numberOfGranaryKeepersPerGranary];
				LinkedList<InventoryItem> granaryInventory = new LinkedList<InventoryItem>();

				buildingID = Program._aStronghold._buildingsList.Count + 1;

				for (int i = 0; i < Consts.numberOfGranaryKeepersPerGranary; i++)
				{
					granaryKeeperJob = Job_creater.createGranaryKeeper(buildingID);
					granaryJobs[i] = granaryKeeperJob.JobID;
					_allJobs.AddLast(granaryKeeperJob);
				} //creates jobs for the Granary Keeper

				BuildingWithJobsAndInventory granary =
					new BuildingWithJobsAndInventory(buildingID, //building ID
													 _leader._id, //owner ID
													 Consts.granary, //type
													 Consts.granary_name, //name
													 Consts.granary_hp, //hp
													 Consts.granary_costtobuild, //cost to build
													 new Status(1, Consts.granary_maxlevel), //level
													 Program._gametime, //start build time
													 buildTime,  //build time
													 granaryJobs,//jobs
													 granaryInventory,//inventory
													 BuildingState.Planned,
													 new Status(0, Consts.granaryMaxInventory));

				Program._aStronghold._buildingsList.AddLast(granary);
				Treasury.withdrawGold(Consts.granary_costtobuild);
			}//have enough money to build farm
			return buildingID;
		}//returns building ID

		#endregion
	}
}
