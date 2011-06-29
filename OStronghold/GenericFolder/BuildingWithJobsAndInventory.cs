using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.GenericFolder
{
    public class BuildingWithJobsAndInventory:Building
    {
        #region Members

        private int[] _jobs; //list of jobs the building offers
        private InventoryItem[] _inventory; //building's inventory (list of goods)
        private Status _maxInventoryCapacity; //maximum amount of items can be hold by inventory

        public int[] Jobs
        {
            get { return _jobs; }
        }
        public InventoryItem[] Inventory
        {
            get { return _inventory; }
        }
        public Status MaxInventoryCapacity
        {
            get { return _maxInventoryCapacity; }
        }

        #endregion

        #region Constructor

        public BuildingWithJobsAndInventory()
            : base()
        {
        }

        public BuildingWithJobsAndInventory(int buildingIDValue,int ownerIDValue, int typeValue, string nameValue, Status hpValue, int costToBuildValue, Status levelValue, Gametime startBuildTimeValue,
                                Gametime endBuildTimeValue, int[] jobsList, InventoryItem[] inventoryList, Consts.buildingState buildingStateValue, Status maxInvCapacityValue)
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
                _inventory = new InventoryItem[inventoryList.Length];
                for (int i = 0; i < inventoryList.Length; i++)
                {
                    if (inventoryList[i] == null)
                    {
                        _inventory[i] = null;
                    }
                    else
                    {
                        _inventory[i] = new InventoryItem(inventoryList[i]);
                    }
                }                
            }
            else _inventory = null;

            _maxInventoryCapacity = new Status(maxInvCapacityValue);
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

            result += "Max Inv Capacity: " + _maxInventoryCapacity.Current + "/" + MaxInventoryCapacity.Max + "\n";
            result += "Inventory: \n";

            if (_inventory != null)
            {
                for (int i = 0; i < _inventory.Length; i++)
                {
                    if (_inventory[i] != null)
                    {
                        result += _inventory[i].getInventoryItemString();
                    }
                }
            }
            else result += "None.";

            return result;
        }

        #endregion
    }//building with jobs and inventory
}
