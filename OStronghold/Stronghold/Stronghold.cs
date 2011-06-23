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

        public struct strongholdStats 
        {
            public int currentPopulation;            
        } //Statistics for Stronghold

        public strongholdStats _stats; //number of people currently living in Stronghold
        public Hashtable _commoners; //hashtable to hold the commoners themselves
        LinkedList<LinkedList<Building>> _buildingsList; //list of buildings (each node contains a building type plus quantity)
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
            _stats.currentPopulation = 0;            
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
            Job job;
            _allJobs = new LinkedList<Job>();
            for (int i = 1; i <= 5; i++)
            {
                job = new Job(i, 9999, -1, "Farmer#" + i, Program._gametime, Program._gametime + Consts.rand.Next(0, 3600), new Gametime(0, Consts.rand.Next(0, 8)), new Gametime(0, Consts.rand.Next(12, 23)), Consts.rand.Next(1, 15), Consts.JobStatus.Available);
                _allJobs.AddLast(job);
            }

            //buildings
            _buildingsList = new LinkedList<LinkedList<Building>>();
            
            
                        
        }//Constructor

        #endregion

        #region Methods

        public void populate(int numberofCommonersToProduce)
        {
            for (int i = 0; i < numberofCommonersToProduce; i++)
            {
                Character commoner = new Character();
                commoner._id = _stats.currentPopulation;
                commoner._name = "P#" + commoner._id;
                commoner._age = 18;                
                commoner._bodyneeds.HungerState = Consts.hungerState.Full;
                commoner._bodyneeds.SleepState = Consts.sleepState.Awake;
                commoner._health.defineHP(20,0);
                commoner._health.defineStamina(100,1);
                commoner._characterActions.insertItemIntoQueue(new CharacterAction(Consts.characterGeneralActions.Idle, Consts.actionsData[(int)Consts.characterGeneralActions.Idle]._actionPriority, Program._gametime + Consts.actionsData[(int)Consts.characterGeneralActions.Idle]._actionDuration));
                commoner._characterinventory.putInInventory(new Generic.InventoryItem(Consts.FOOD_NAME, Consts.FOOD_ID, Consts.FOOD_WEIGHT, 10));
                commoner._characterinventory.putInInventory(new Generic.InventoryItem(Consts.GOLD_NAME, Consts.GOLD_ID, Consts.GOLD_WEIGHT, 50));

                _commoners.Add(_stats.currentPopulation, commoner);                
                _stats.currentPopulation++;                
            }
        }//Populating by giving birth to x people

        public void printPopulation()
        {
            Character person = new Character();
            Job job;
                        
            for (int i = 0; i < _stats.currentPopulation; i++)
            {
                person = ((Character)_commoners[i]);
                //foreach (characteraction val in person._characteractions)
                //{
                //    console.writeline(val.action + " (" + val.priority + ") ");
                //}
                job = searchJobByID(person._jobID);
                if (job == null)
                {
                    Console.WriteLine(person._name + " is currently "+ person._characterActions.Peek().Action +" and has " + person._characterinventory.searchForItemByID(Consts.GOLD_ID).Quantity + " Gold.");
                }
                else
                {
                    Console.WriteLine(person._name + " is currently " + person._characterActions.Peek().Action + " as a " + job.JobName + " and has " + person._characterinventory.searchForItemByID(Consts.GOLD_ID).Quantity + " Gold (" + person._characterActions.Peek().FinishTime + ")");
                }
            }
        }//Prints in output all the commoner information

        public void printStrongholdLeader()
        {
            Console.WriteLine(_leader._name + " (" + _leader._health.hp.Current + "|" + _leader._health.stamina.Current + ") is " + _leader._characterActions.Peek().Action + " and " + _leader._bodyneeds.HungerState + " and " + _leader._bodyneeds.LastAteTime + " has: " + _leader._characterinventory.ToString());
        }

        public void printJobs()
        {
            foreach (Job job in _allJobs)
            {
                if (job.JobStatus == Consts.JobStatus.Available)
                {
                    Console.WriteLine("Name: " + job.JobName + " Status: " + job.JobStatus + " Payroll: " + job.Payroll + " End date: " + job.EndDate);
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
        }

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
        }

        #endregion
    }
}
