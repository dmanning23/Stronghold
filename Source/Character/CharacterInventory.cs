using System.Collections.Generic;

namespace Stronghold
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

		public InventoryItem searchForItemByName(string targetName)
		{
			foreach (InventoryItem item in _inventory)
			{
				if (item.Name.CompareTo(targetName) == 0)
				{
					return item;
				}
			}
			return null;
		}//searches the inventory for all the items that match the target string and returns the objects in a linklist

		public InventoryItem searchForItemByID(int targetID)
		{
			foreach (InventoryItem item in _inventory)
			{
				if (item.ID == targetID)
				{
					return item;
				}
			}
			return null;
		}//searches the inventory for all the items that match the target ID and returns the objects in a linklist

		public InventoryItem retrieveItemInInventory(string targetName, int targetID)
		{
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
			return itemSearched;
		}//if targetName = null or "", then searchbyID else if targetID = -1 then search by Name

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
