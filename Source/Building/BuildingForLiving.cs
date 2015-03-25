
namespace Stronghold
{
	public class BuildingForLiving : Building
	{
		#region Members

		private Status _tenants;
		private int[] _tenantsID;

		public Status Tenants
		{
			get { return _tenants; }
		}

		public int[] TenantsID
		{
			get { return _tenantsID; }
		}

		#endregion

		#region Constructor

		public BuildingForLiving(int idValue, int ownerIDValue, int typeValue, string nameValue, Status hpValue, int costToBuildValue, Status levelValue, Gametime startBuildTimeValue,
								 int numberOfManBuildingHoursValue, Status tenantsValue, int[] tenantsIDValue, BuildingState buildingStateValue)
			: base(idValue, ownerIDValue, typeValue, nameValue, hpValue, costToBuildValue, levelValue, startBuildTimeValue, numberOfManBuildingHoursValue, buildingStateValue)
		{
			_tenants = new Status(tenantsValue);

			if (tenantsIDValue != null)
			{
				_tenantsID = new int[tenantsIDValue.Length];
				for (int i = 0; i < tenantsIDValue.Length; i++)
				{
					_tenantsID[i] = tenantsIDValue[i];
				}
			}
			else _tenantsID = null;
		}

		#endregion

		#region Methods

		public bool isPopulable(int numberOfNewTenants)
		{
			if (numberOfNewTenants > (Tenants.Max - Tenants.Current))
			{
				return false;
			}//overpopulating building
			else
			{
				return true;
			}
		}

		public int populateLivingBuilding(int numberOfNewTenants)
		{
			if (isPopulable(numberOfNewTenants))
			{
				Tenants.Current += numberOfNewTenants;
				return 0;
			}
			else
			{
				Tenants.Current = Tenants.Max;
				return (numberOfNewTenants - (Tenants.Max - Tenants.Current));
			}// overpopulating the buildling - returns the amount not populated
		}//returns the number of unsuccessful populated tenants (i.e: 8/10 hut and populates 3 ppl will return 1. popuating 1 or 2 will return 0.

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
			result += "Build time: " + base.NumberOfManBuildingHoursLeft + "\n";
			result += "Number of current builders: " + base.NumberOfCurrentBuilders + "\n";
			result += "Building state: " + base.BuildingState + "\n";
			result += "Tenants: " + Tenants.Current + "/" + Tenants.Max + "\n";
			result += "Tenants ID: \n";
			if (null != _tenantsID || _tenantsID.Length != 0)
			{
				for (int i = 0; i < _tenantsID.Length; i++)
				{
					result += _tenantsID[i] + "\n";
				}
			}
			else result += "None.";
			return result;
		}

		#endregion
	}//building for people to live in
}
