using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.Generic
{
    public class BuildingForLiving:Building
    {
        #region Members

        private Status _tenants;

        public Status Tenants
        {
            get { return _tenants; }            
        }

        #endregion

        #region Constructor

        public BuildingForLiving(int typeValue, string nameValue, Status hpValue, int costToBuildValue, Status levelValue, Gametime startBuildTimeValue,
                                 Gametime endBuildTimeValue, Status tenantsValue)
            : base(typeValue, nameValue, hpValue, costToBuildValue, levelValue, startBuildTimeValue, endBuildTimeValue)
        {
            _tenants = new Status(tenantsValue);                       
        }

        #endregion

        #region Methods

        public int populateLivingBuilding(int numberOfNewTenants)
        {
            if (numberOfNewTenants > (Tenants.Max - Tenants.Current))
            {
                Tenants.Current = Tenants.Max;
                return (numberOfNewTenants - (Tenants.Max - Tenants.Current));
            }// overpopulating the buildling - returns the amount not populated
            else
            {
                Tenants.Current += numberOfNewTenants;
                return 0;
            }
        }//returns the number of unsuccessful populated tenants (i.e: 8/10 hut and populates 3 ppl will return 1. popuating 1 or 2 will return 0.

        public override string getBuildingString()
        {
            string result = "";
            result += "Building type: " + base.Type + "\n";
            result += "Building name: " + base.Name + "\n";
            result += "Building HP: " + base.HP.Current + "/" + base.HP.Max + "\n";
            result += "Cost to build: " + base.CostToBuild + "\n";
            result += "Building level: " + base.Level.Current + "/" + base.Level.Max + "\n";
            result += "Start build time: " + base.StartBuildTime + "\n";
            result += "End build time: " + base.EndBuildTime + "\n";
            result += "Tenants: " + Tenants.Current + "/" + Tenants.Max + "\n";            
            
            return result;
        }

        #endregion
    }//building for people to live in
}
