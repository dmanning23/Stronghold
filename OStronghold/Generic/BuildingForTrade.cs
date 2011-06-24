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

        public BuildingForTrade(int idValue, string nameValue, Status hpValue, int costToBuildValue, Status levelValue, Gametime startBuildTimeValue,
                                Gametime endBuildTimeValue, LinkedList<Job> jobsList, LinkedList<InventoryItem> inventoryList)
            : base(idValue, nameValue, hpValue, costToBuildValue, levelValue, startBuildTimeValue, endBuildTimeValue)
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
    }//building for buying and selling goods
}
