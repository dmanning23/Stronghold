using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OStronghold.Generic;

namespace OStronghold
{
    public class CharacterInventory
    {
        #region Members

        private int _currentInventoryCapacity;
        private int _maxInventoryCapacity;
        private LinkedList<InventoryItem> _inventory;

        public LinkedList<InventoryItem> Inventory
        {
            get { return _inventory; }            
        }

        public int CurrentInventoryCapacity
        {
            get { return _currentInventoryCapacity; }
            set { _currentInventoryCapacity = value; }
        }

        public int MaxInventoryCapacity
        {
            get { return _maxInventoryCapacity; }
        }

        #endregion

        #region Constructor

        public CharacterInventory()
        {
            _currentInventoryCapacity = 0;
            _maxInventoryCapacity = Consts.startCharInventoryMaxCapacity;
            _inventory = new LinkedList<InventoryItem>();            
        }

        #endregion

        #region Methods

        public bool putInInventory(InventoryItem item)
        {
            if (_currentInventoryCapacity + item.Quantity > _maxInventoryCapacity)
            {
                return false;
            }
            else
            {
                _inventory.AddLast(item);
                _currentInventoryCapacity += item.Quantity;
                return true;
            }
        }//puts an item into the inventory and returns results.

        public bool putInInventory(LinkedList<InventoryItem> items)
        {
            foreach (InventoryItem item in items)
            {
                if (_currentInventoryCapacity + item.Quantity > _maxInventoryCapacity)
                {
                    return false;
                }
                else
                {
                    _inventory.AddLast(item);
                    _currentInventoryCapacity += item.Quantity;                    
                }
            }
            return true;
        }//puts an item into the inventory and returns results.

        public LinkedList<InventoryItem> searchForItemsByName(string targetName)
        {
            LinkedList<InventoryItem> result = new LinkedList<InventoryItem>();
            
            foreach (InventoryItem item in _inventory)
            {
                if (item.Name.CompareTo(targetName) == 0)
                {
                    result.AddLast(item);                    
                }
            }
            return result;
        }//searches the inventory for all the items that match the target string and returns the objects in a linklist

        public LinkedList<InventoryItem> searchForItemsByID(int targetID)
        {
            LinkedList<InventoryItem> result = new LinkedList<InventoryItem>();

            foreach (InventoryItem item in _inventory)
            {
                if (item.ID == targetID)
                {
                    result.AddLast(item);
                }
            }
            return result;
        }//searches the inventory for all the items that match the target ID and returns the objects in a linklist

        public LinkedList<InventoryItem> retrieveItemInInventory(string targetName, int targetID)
        {
            LinkedList<InventoryItem> result = new LinkedList<InventoryItem>();
            if (targetName == null) result = searchForItemsByID(targetID);
            else if (targetID == -1) result = searchForItemsByName(targetName);
            else result = null; //error because rule is to have either search by name or id

            if (result != null)
            {
                foreach (InventoryItem item in result)
                {
                    _currentInventoryCapacity -= item.Quantity; //frees up inventory capacity
                    _inventory.Remove(item);                    
                }
            }//need to remove the found objects from the inventory

            return result;
        }//if targetName = null, then searchbyID else if targetID = -1 then search by Name

        public bool IsEmpty()
        {
            return (_inventory.Count == 0);
        }//checks if inventory is empty

        public override string ToString()
        {
            string output = "";
            foreach (InventoryItem item in _inventory)
            {
                output += (item.Name + " x " + item.Quantity + " ");
            }
            return output;
        }

        #endregion
    }//character inventory
}
