using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.Generic
{
    public class BuildingForTrade:Building
    {
        #region Members

        private LinkedList<Job> _jobs; //list of jobs the building offers
        private LinkedList<InventoryItem> _inventory; //building's inventory (list of goods)

        public LinkedList<Job> Jobs
        {
            get { return _jobs; }
        }
        public LinkedList<InventoryItem> Inventory
        {
            get { return _inventory; }
        }

        #endregion

        #region Constructor

        public BuildingForTrade()
            : base()
        {
        }

        public BuildingForTrade(int idValue, int typeValue, string nameValue, Status hpValue, int costToBuildValue, Status levelValue, Gametime startBuildTimeValue,
                                Gametime endBuildTimeValue, LinkedList<Job> jobsList, LinkedList<InventoryItem> inventoryList)
            : base(idValue, typeValue, nameValue, hpValue, costToBuildValue, levelValue, startBuildTimeValue, endBuildTimeValue)
        {
            Job tempJob;
            foreach (Job job in jobsList)
            {
                tempJob = new Job(job);
                _jobs.AddLast(job);
            }

            InventoryItem tempItem;
            foreach (InventoryItem item in inventoryList)
            {
                tempItem = new InventoryItem(item);
                _inventory.AddLast(tempItem);
            }
        }

        #endregion

        #region Methods

        public override string getBuildingString()
        {
            string result = "";
            result += "Building ID: " + base.ID + "\n";
            result += "Building type: " + base.Type + "\n";
            result += "Building name: " + base.Name + "\n";
            result += "Building HP: " + base.HP.Current + "/" + base.HP.Max + "\n";
            result += "Cost to build: " + base.CostToBuild + "\n";
            result += "Building level: " + base.Level.Current + "/" + base.Level.Max + "\n";
            result += "Start build time: " + base.StartBuildTime + "\n";
            result += "End build time: " + base.EndBuildTime + "\n";
            result += "Jobs: " + base.EndBuildTime + "\n";
            foreach (Job job in Jobs)
            {
                result += job.getJobString();
            }
            result += "Inventory: " + base.EndBuildTime + "\n";
            foreach (InventoryItem item in Inventory)
            {
                result += item.getInventoryItemString();
            }

            return result;
        }

        #endregion
    }//building for buying and selling goods
}
