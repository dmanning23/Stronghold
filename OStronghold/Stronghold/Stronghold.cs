using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using OStronghold.Generic;

namespace OStronghold
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
            _treasury = new Treasury(100);
            _leader = new StrongholdLeader();

            //configure leader
            _leader._id = 9999;
            _leader._name = "SHLeader";
            _leader._age = 34;
            _leader._bodyneeds.HungerState = Consts.hungerState.Full;
            _leader._bodyneeds.SleepState = Consts.sleepState.Awake;
            _leader._health.defineHP(50, 0);
            _leader._health.defineStamina(100, 1);
            _leader._characterActions.insertItemIntoQueue(new CharacterAction(Consts.characterGeneralActions.Idle, Consts.actionsData[(int)Consts.characterGeneralActions.Idle]._actionPriority, Program._gametime + Consts.actionsData[(int)Consts.characterGeneralActions.Idle]._actionDuration));
            _leader._characterinventory.putInInventory(new Generic.InventoryItem(Consts.FOOD_NAME, Consts.FOOD_ID, Consts.FOOD_WEIGHT, 10));            

            //testing out job

            _allJobs = new LinkedList<Job>();
            //Job job;
            //for (int i = 1; i <= 5; i++)
            //{
            //    job = new Job(i, 9999, -1, "Farmer#" + i, Program._gametime, Program._gametime + Consts.rand.Next(0, 3600), new Gametime(0, Consts.rand.Next(0, 8)), new Gametime(0, Consts.rand.Next(12, 23)), Consts.rand.Next(1, 15), Consts.JobStatus.Available);
            //    _allJobs.AddLast(job);
            //}

            //buildings
            _buildingsList = new LinkedList<Building>();

            
                       
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
                commoner._bodyneeds.HungerState = Consts.hungerState.Full;
                commoner._bodyneeds.SleepState = Consts.sleepState.Awake;
                commoner._health.defineHP(20, 0);
                commoner._health.defineStamina(100, 10);
                commoner._characterActions.insertItemIntoQueue(new CharacterAction(Consts.characterGeneralActions.Idle, Consts.actionsData[(int)Consts.characterGeneralActions.Idle]._actionPriority, Program._gametime + Consts.actionsData[(int)Consts.characterGeneralActions.Idle]._actionDuration));
                commoner._characterinventory.putInInventory(new Generic.InventoryItem(Consts.FOOD_NAME, Consts.FOOD_ID, Consts.FOOD_WEIGHT, 10));
                commoner._characterinventory.putInInventory(new Generic.InventoryItem(Consts.GOLD_NAME, Consts.GOLD_ID, Consts.GOLD_WEIGHT, 50));

                _commoners.Add(commoner._id, commoner);

            }
        }//Populating by giving birth to x people

        public void printPopulation()
        {
            Character person = new Character();
            Job job;

            for (int i = 0; i < _commoners.Count; i++)
            {
                person = ((Character)_commoners[i]);
                //foreach (characteraction val in person._characteractions)
                //{
                //    Consts.printMessage(val.action + " (" + val.priority + ") ");
                //}
                job = searchJobByID(person._jobID);
                if (job == null)
                {
                    Consts.printMessage(person._name + " is currently " + person._characterActions.Peek().Action + " and has " + person._characterinventory.searchForItemByID(Consts.GOLD_ID).Quantity + " Gold.");
                }
                else
                {
                    Consts.printMessage(person._name + " is currently " + person._characterActions.Peek().Action + " as a " + job.JobName + " and has " + person._characterinventory.searchForItemByID(Consts.GOLD_ID).Quantity + " Gold (" + person._characterActions.Peek().FinishTime + ")");
                }
            }
        }//Prints in output all the commoner information

        public void printPerson(int ID)
        {
            Character person = ((Character)_commoners[ID]);
            Job job = searchJobByID(person._jobID);

            if (job == null)
            {
                Consts.printMessage(person._name + " is currently " + person._characterActions.Peek().Action + " and has " + person._characterinventory.searchForItemByID(Consts.GOLD_ID).Quantity + " Gold.");
            }
            else
            {
                Consts.printMessage(person._name + " is currently " + person._characterActions.Peek().Action + " as a " + job.JobName + " and has " + person._characterinventory.searchForItemByID(Consts.GOLD_ID).Quantity + " Gold (" + person._characterActions.Peek().FinishTime + ")");
            }
        }

        public void printJobs()
        {
            foreach (Job job in _allJobs)
            {
                if (job.JobStatus == Consts.JobStatus.Available)
                {
                    Consts.printMessage("Name: " + job.JobName + " Status: " + job.JobStatus + " Payroll: " + job.Payroll + " End date: " + job.EndDate);
                }
            }
        }

        public void activateIdleCommoners()
        {                                                        
        }//Decide what idle commoners should be doing

        public Job searchJobByID(int jobID)
        {
            foreach (Job job in _allJobs)
            {
                if (job.JobID == jobID)
                {
                    return job;
                }
            }
            return null;
        }//search job according to id

        public Building searchBuildingByID(int buildingID)
        {
            foreach (Building building in _buildingsList)
            {
                if (building.BuildingID == buildingID)
                {
                    return building;
                }
            }
            return null;
        }//search building according to id

        public LinkedList<Job> getAllAvailableJobs()
        {
            LinkedList<Job> list = new LinkedList<Job>();
            foreach (Job job in _allJobs)
            {
                if (job.JobStatus == Consts.JobStatus.Available)
                {
                    list.AddLast(job);
                }
            }
            return list;
        }//returns list of all jobs with status of available
       
        #endregion

        #region Methods for constructing buildings

        public void buildHut()
        {
            if (Treasury.haveEnoughToWithdraw(Consts.hut_costtobuild))
            {
                Generic.BuildingForLiving hut =
                    new Generic.BuildingForLiving(Program._aStronghold._buildingsList.Count + 1, //building ID
                                                  _leader._id, //owner ID
                                                  Consts.accomodation, //building type
                                                  Consts.hut_name, //building name
                                                  Consts.hut_hp, //building HP
                                                  Consts.hut_costtobuild, //building cost to build
                                                  new Generic.Status(1, Consts.hut_maxlevel), //building level
                                                  Program._gametime, //building start build time
                                                  Program._gametime + Consts.hut_buildtime, //building end build time
                                                  new Generic.Status(0, Consts.hut_maxtenants), //building tenants
                                                  new int[Consts.hut_maxtenants],
                                                  Consts.buildingState.Planned); //building state

                Program._aStronghold._buildingsList.AddLast(hut);
                Treasury.withdrawGold(Consts.hut_costtobuild);
            }//have enough money to build hut
            else
            {
                Consts.writeToDebugLog("Not enough money to build hut.");
            }
        }

        public void buildFarm()
        {
            if (Treasury.haveEnoughToWithdraw(Consts.farm_costtobuild))
            {
                int jobID, buildingID;
                Job farmerJob;
                int[] farmJobs = new int[Consts.numberOfFarmersPerFarm];

                buildingID = Program._aStronghold._buildingsList.Count + 1;
                
                for (int i = 0; i < Consts.numberOfFarmersPerFarm; i++)
                {
                    jobID = Program._aStronghold._allJobs.Count + 1;
                    farmerJob = new Job(jobID,
                                        buildingID,
                                        _leader._id,
                                        Consts.noworker,
                                        "Farmer",
                                        Program._gametime,
                                        Program._gametime + Consts.oneMonth,
                                        new Gametime(0, 5, 0),
                                        new Gametime(0, 15, 0),
                                        Consts.farmerPayroll,
                                        Consts.JobStatus.Available);
                    _allJobs.AddLast(farmerJob);
                    farmJobs[i] = jobID;
                } //creates jobs for the farmer


                Generic.BuildingWithJobsAndInventory farm =
                    new BuildingWithJobsAndInventory(buildingID, //building ID
                                                     _leader._id, //owner ID
                                                     Consts.farm, //type
                                                     Consts.farm_name, //name
                                                     Consts.farm_hp, //hp
                                                     Consts.farm_costtobuild, //cost to build
                                                     new Generic.Status(1, Consts.farm_maxlevel), //level
                                                     Program._gametime, //start build time
                                                     Program._gametime + Consts.farm_buildtime,  //end build time
                                                     farmJobs,//jobs
                                                     null,//inventory
                                                     Consts.buildingState.Planned);
                
                Program._aStronghold._buildingsList.AddLast(farm);
                Treasury.withdrawGold(Consts.farm_costtobuild);
            }//have enough money to build farm
            else
            {
                Consts.writeToDebugLog("Not enough money to build farm.");
            }
        }

        #endregion 
    }
}
