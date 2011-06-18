using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.Generic
{
    public class Building
    {
        #region Members       

        private string _name; //name of building
        private Status _hp; //hp of buildling
        private int _cost; //cost to build building for each level
        private int _level; //level of building
        private Gametime startBuildTime; //timestamp of when started to build
        private Gametime endBuildTime; //timestamp of when building finish
        private LinkedList<Job> _jobs; //list of jobs the building offers
        private LinkedList<InventoryItem> _inventory; //building's inventory (list of goods)

        #endregion

        #region Constructor

        public Building()
        {
        }               

        #endregion

    }
}
