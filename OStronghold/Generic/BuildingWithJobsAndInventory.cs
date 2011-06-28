using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.Generic
{
    public class BuildingWithJobsAndInventory:Building
    {
        #region Members

        private int[] _jobs; //list of jobs the building offers
        private LinkedList<InventoryItem> _inventory; //building's inventory (list of goods)

        public int[] Jobs
        {
            get { return _jobs; }
        }
        public LinkedList<InventoryItem> Inventory
        {
            get { return _inventory; }
        }

        #endregion

        #region Constructor

        public BuildingWithJobsAndInventory()
            : base()
        {
        }

        public BuildingWithJobsAndInventory(int buildingIDValue,int ownerIDValue, int typeValue, string nameValue, Status hpValue, int costToBuildValue, Status levelValue, Gametime startBuildTimeValue,
                                Gametime endBuildTimeValue, int[] jobsList, LinkedList<InventoryItem> inventoryList, Consts.buildingState buildingStateValue)
            : base(buildingIDValue, ownerIDValue, typeValue, nameValue, hpValue, costToBuildValue, levelValue, startBuildTimeValue, endBuildTimeValue, buildingStateValue)
        {
            if (jobsList != null)
            {
                _jobs = new int[jobsList.Length];
                for (int i = 0; i < jobsList.Length; i++)
                {
                    _jobs[i] = jobsList[i];
                }
            }
            else _jobs = null;

            if (inventoryList != null)
            {
                InventoryItem tempItem;
                foreach (InventoryItem item in inventoryList)
                {
                    tempItem = new InventoryItem(item);
                    _inventory.AddLast(tempItem);
                }
            }
            else _inventory = null;
        }

        #endregion

        #region Methods

        public override string getBuildingString()
        {
            string result = "";
            result += "Building ID: " + base.BuildingID + "\n";
            result += "Owner ID: " + base.OwnerID + "\n";
            result += "Building type: " + base.Type + "\n";
            result += "Building name: " + base.Name + "\n";
            result += "Building HP: " + base.HP.Current + "/" + base.HP.Max + "\n";
            result += "Cost to build: " + base.CostToBuild + "\n";
            result += "Building level: " + base.Level.Current + "/" + base.Level.Max + "\n";
            result += "Start build time: " + base.StartBuildTime + "\n";
            result += "End build time: " + base.EndBuildTime + "\n";
            result += "Building state: " + base.BuildingState + "\n";
            result += "Jobs: \n";
            if (_jobs != null)
            {
                for (int i = 0; i < _jobs.Length; i++)
                {
                    result += Program._aStronghold.searchJobByID(_jobs[i]).getJobString();
                }
            }
            else result += "None.";

            result += "Inventory: \n";
            
            if (_inventory != null)
            {
                foreach (InventoryItem item in Inventory)
                {
                    result += item.getInventoryItemString();
                }
            }
            else result += "None.";

            return result;
        }

        #endregion
    }//building with jobs and inventory
}
