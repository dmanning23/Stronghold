using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OStronghold.GenericFolder;

namespace OStronghold.CharacterFolder
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
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (_currentInventoryCapacity + item.Quantity > _maxInventoryCapacity)
            {
                Consts.globalEvent.writeEvent("Inventory has no room for " + item.Name + ".", Consts.eventType.Character, Consts.EVENT_DEBUG_MAX);
                Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            else
            {
                _inventory.AddLast(item);
                _currentInventoryCapacity += item.Quantity;
                Consts.globalEvent.writeEvent(item.Name + " added to Inventory.", Consts.eventType.Character, Consts.EVENT_DEBUG_MAX);
                Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
                return true;
            }
        }//puts an item into the inventory and returns results.

        public bool putInInventory(LinkedList<InventoryItem> items)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            foreach (InventoryItem item in items)
            {
                if (_currentInventoryCapacity + item.Quantity > _maxInventoryCapacity)
                {
                    Consts.globalEvent.writeEvent("Inventory has no room for " + item.Name + ".", Consts.eventType.Character, Consts.EVENT_DEBUG_MAX);
                    Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return false;
                }
                else
                {
                    _inventory.AddLast(item);
                    _currentInventoryCapacity += item.Quantity;
                    Consts.globalEvent.writeEvent(item.Name + " added to Inventory.", Consts.eventType.Character, Consts.EVENT_DEBUG_MAX);
                }
            }
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return true;
        }//puts an item into the inventory and returns results.

        public InventoryItem searchForItemByName(string targetName)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);  
            foreach (InventoryItem item in _inventory)
            {
                if (item.Name.CompareTo(targetName) == 0)
                {
                    Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return item;
                }
            }
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return null;
        }//searches the inventory for all the items that match the target string and returns the objects in a linklist

        public InventoryItem searchForItemByID(int targetID)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            foreach (InventoryItem item in _inventory)
            {
                if (item.ID == targetID)
                {
                    Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return item;
                }
            }
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return null;
        }//searches the inventory for all the items that match the target ID and returns the objects in a linklist

        public InventoryItem retrieveItemInInventory(string targetName, int targetID)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            InventoryItem itemSearched = new InventoryItem(); ;
            if (targetName == null || targetName == "")
            {
                itemSearched = searchForItemByID(targetID);                
            }
            else if (targetID == -1)
            {
                itemSearched = searchForItemByName(targetName);
            }
            _currentInventoryCapacity -= itemSearched.Quantity; //frees up inventory capacity
            _inventory.Remove(itemSearched);
            Consts.globalEvent.writeEvent(itemSearched.Name + " removed from Inventory.", Consts.eventType.Character, Consts.EVENT_DEBUG_MAX);
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return itemSearched;            
        }//if targetName = null or "", then searchbyID else if targetID = -1 then search by Name

        public bool IsEmpty()
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return (_inventory.Count == 0);
        }//checks if inventory is empty

        public override string ToString()
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            string output = "";
            foreach (InventoryItem item in _inventory)
            {
                output += (item.Name + " x " + item.Quantity + " ");
            }
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return output;
        }

        public string searchItemByIDAndPrintInfo(int targetItemID)
        {
            string result = "";
            foreach (InventoryItem item in _inventory)
            {
                if (item.ID == targetItemID)
                {
                    result = item.Name + " x " + item.Quantity;
                }
            }
            return result;
        }
        
        #endregion
    }//character inventory
}
