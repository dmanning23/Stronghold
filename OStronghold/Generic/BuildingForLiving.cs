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

        public Status Tenants;

        #endregion

        #region Constructor

        public BuildingForLiving(int idValue, string nameValue, Status hpValue, int costToBuildValue, Status levelValue, Gametime startBuildTimeValue,
                                 Gametime endBuildTimeValue, Status tenantsValue)
            : base(idValue, nameValue, hpValue, costToBuildValue, levelValue, startBuildTimeValue, endBuildTimeValue)
        {
            Tenants.Clone(tenantsValue);
        }

        #endregion
    }//building for people to live in
}
