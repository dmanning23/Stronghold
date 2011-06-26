using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.Generic
{
    public class Building
    {
        #region Members       

        private int _id; //building id
        private string _name; //name of building
        private Status _hp; //hp of buildling
        private int _costToBuild; //cost to build building for each level
        private Status _level; //level of building
        private Gametime _startBuildTime; //timestamp of when started to build
        private Gametime _endBuildTime; //timestamp of when building finish


        public int ID
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _name; }
        }
        public Status HP
        {
            get { return _hp; }
        }
        public int CostToBuild
        {
            get { return _costToBuild; }
        }
        public Status Level
        {
            get { return _level; }
        }
        public Gametime StartBuildTime
        {
            get { return _startBuildTime; }
        }
        public Gametime EndBuildTime
        {
            get { return _endBuildTime; }
        }


        #endregion

        #region Constructor

        public Building()
        {
        }

        public Building(int idValue, string nameValue, Status hpValue, int costToBuildValue, Status levelValue, Gametime startBuildTimeValue,
                        Gametime endBuildTimeValue)
        {
            _id = idValue;
            _name = String.Copy(nameValue);
            _hp = new Status(hpValue);            
            _costToBuild = costToBuildValue;
            _level = new Status(levelValue);
            _startBuildTime = new Gametime(startBuildTimeValue);
            _endBuildTime = new Gametime(endBuildTimeValue);            
        }

        #endregion

    }
}
