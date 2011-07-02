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
        private LinkedList<InventoryItem> _inventory; //building's inventory (list of goods)
        private Status _inventoryCapacity; //maximum amount of items can be hold by inventory

        public int[] Jobs
        {
            get { return _jobs; }
        }
        private LinkedList<InventoryItem> Inventory //private because method created for adding inventory
        {
            get { return _inventory; }
        }
        public Status InventoryCapacity
        {
            get { return _inventoryCapacity; }
        }

        #endregion

        #region Constructor

        public BuildingWithJobsAndInventory()
            : base()
        {
        }

        public BuildingWithJobsAndInventory(int buildingIDValue,int ownerIDValue, int typeValue, string nameValue, Status hpValue, int costToBuildValue, Status levelValue, Gametime startBuildTimeValue,
                                Gametime endBuildTimeValue, int[] jobsList, LinkedList<InventoryItem> inventoryList, Consts.buildingState buildingStateValue, Status maxInvCapacityValue)
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

            _inventory = new LinkedList<InventoryItem>();
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

            _inventoryCapacity = new Status(maxInvCapacityValue);
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

            result += "Max Inv Capacity: " + _inventoryCapacity.Current + "/" + InventoryCapacity.Max + "\n";
            result += "Inventory: \n";

            if (_inventory != null)
            {
                foreach (InventoryItem item in _inventory)
                {
                    result += item.getInventoryItemString();
                }
            }
            else result += "None.";

            return result;
        }

        public bool hasEnoughStorageSpace(int insertAmount)
        {
            return (_inventoryCapacity.Max - _inventoryCapacity.Current >= insertAmount);            
        }

        public void addToInventory(InventoryItem targetItem)
        {
            if (_inventory.Count == 0)
            {
                _inventory.AddLast(targetItem);
            }//empty inventory
            foreach (InventoryItem item in _inventory)
            {
                if (item.Name == targetItem.Name)
                {
                    item.Quantity += targetItem.Quantity;
                }//item already exists - need to update quantity
                else
                {
                    _inventory.AddLast(targetItem);    
                }//no similar item in inventory
            }
            _inventoryCapacity.Current += targetItem.Quantity;
            Consts.globalEvent.writeEvent(targetItem.Name + " added to " + this.Name + "'s inventory.", Consts.eventType.Building, Consts.EVENT_DEBUG_MAX);
        }

        public bool removeFromInventory(string targetName ,int quantityToRemove)
        {
            foreach (InventoryItem item in _inventory)
            {
                if (item.Name == targetName)
                {
                    if (quantityToRemove > item.Quantity) return false;
                    else
                    {
                        item.Quantity -= quantityToRemove;
                        _inventoryCapacity.Current -= quantityToRemove;
                        Consts.globalEvent.writeEvent(item.Name + " removed from " + this.Name + "'s inventory.", Consts.eventType.Building, Consts.EVENT_DEBUG_MAX);
                        return true;
                    }
                }//item exists - need to update quantity                
            }
            Consts.globalEvent.writeEvent(targetName + " does not exist in " + this.Name + "'s inventory.", Consts.eventType.Building, Consts.EVENT_DEBUG_MAX);
            return false;            
        }//return false if item does not exists in inventory or trying to remove too much 

        public LinkedList<InventoryItem> getInventory()
        {
            return _inventory;
        }

        public InventoryItem searchInventoryByID(int targetID)
        {
            foreach (InventoryItem item in _inventory)
            {
                if (item.ID == targetID)
                {
                    return item;
                }               
            }
            return null;
        }
    
        #endregion
    }//building with jobs and inventory
}
