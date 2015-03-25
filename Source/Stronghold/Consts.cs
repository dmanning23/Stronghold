using System;
using System.IO;

namespace Stronghold
{
	public class Consts
	{
		#region Members

		#region action related

		public struct actionStruct
		{
			public int _actionDuration; //in minutes
			public int _actionPriority; //the lower the number and higher the priority
		}
		public static actionStruct[] actionsData;
		#endregion

		#region food related

		public enum hungerTimer { Hungry = 0, Normal = 240, Full = 420 }; //minutes

		#endregion

		public enum sleepTimer { Awake = 1080 };

		public static Random rand = new Random((int)DateTime.Now.Ticks);

		#region time related

		public static int gametickperSecond = 1; //how many real time seconds are there in a game tick
		public static int gametickIncreaseMinutes = 60; //how many game minutes are increased in every game tick
		public static int oneHour = 60;
		public static int halfDay = 720;
		public static int oneDay = 1440;
		public static int oneMonth = 43200;
		public static int oneYear = 518400;

		#endregion

		#region inventory related

		public static int startCharInventoryMaxCapacity = 1000;

		#endregion

		#region job related

		public static int noworker = -1;

		public static string farmerName = "Farmer";
		public static int farmerJobDuration = oneMonth;
		public static int farmerPayroll = 3;
		public static int numberOfFarmersPerFarm = 5;
		public static Gametime farmerBeginTime;
		public static Gametime farmerEndTime;
		public static int numberOfFoodProducedPerFarmer = 6;

		public static string granaryKeeperName = "Granary Keeper";
		public static int granaryKeeperJobDuration = oneMonth;
		public static int granaryKeeperPayroll = 5;
		public static int numberOfGranaryKeepersPerGranary = 1;
		public static Gametime granaryKeeperBeginTime;
		public static Gametime granaryKeeperEndTime;

		public static string builderName = "Builder";
		public static int builderJobDuration = oneYear;
		public static int builderPayroll = 3;
		public static Gametime builderBeginTime;
		public static Gametime builderEndTime;
		public static int numberOfInitialBuilders = 3;

		#endregion

		#region ID numbers reserved (1-99)
		#endregion
		#region ID types for buildings (100 -> 500)

		public static int hut = 100;
		public static int farm = 101;
		public static int granary = 102;
		public static int employmentoffice = 103;

		public static int stronghold_jobs = 497;
		public static int stronghold_treasury = 498;
		public static int stronghold_yard = 499;
		public static int stronghold = 500;


		#endregion
		#region ID numbers for inventory (1000 -> 5000)

		public static int GOLD_ID = 1000;
		public static string GOLD_NAME = "Gold";
		public static double GOLD_WEIGHT = 0.1;

		public static int FOOD_ID = 1001;
		public static string FOOD_NAME = "Food";
		public static double FOOD_WEIGHT = 0.1;

		#endregion

		#region Building information

		public static int immediatebuildtime = -1;

		#region hut
		public static string hut_name = "Hut";
		public static Status hut_hp;
		public static int hut_costtobuild = 25;
		public static int hut_maxlevel = 5;
		public static int hut_buildtime = 12; //12 man working hours
		public static int hut_maxtenants = 10;
		#endregion

		#region farm
		public static string farm_name = "Farm";
		public static Status farm_hp;
		public static int farm_costtobuild = 50;
		public static int farm_maxlevel = 3;
		public static int farm_buildtime = 12; //12 man working hours
		#endregion

		#region granary
		public static string granary_name = "Granary";
		public static Status granary_hp;
		public static int granary_costtobuild = 200;
		public static int granary_maxlevel = 2;
		public static int granary_buildtime = 24; //24 man working hours
		public static int granaryMaxInventory = 200;
		#endregion

		#region employmentoffice
		public static string employmentoffice_name = "Employment Office";
		public static Status employmentoffice_hp;
		public static int employmentoffice_costtobuild = 0;
		public static int employmentoffice_maxlevel = 1;
		public static int employmentoffice_buildtime = 0;
		#endregion

		#endregion

		#region Economy prices (in gold)

		public static int FOOD_PRICE = 1;

		#endregion

		#region Events + Debug related

		public enum eventType { Character, Building, Stronghold };

		public static int EVENT_DEBUG_MIN = 1;
		public static int EVENT_DEBUG_MINPLUS = 2;
		public static int EVENT_DEBUG_NORMAL = 3;
		public static int EVENT_DEBUG_NORMALPLUS = 4;
		public static int EVENT_DEBUG_MAX = 5;

		public static int CURRENT_EVENT_DEBUG_LEVEL = 3; //1=min, 5=max
		public static int CURRENT_DEBUG_LOG = 0; //0 = off, 1 = on
		public static int CURRENT_BUILDINGEVENTS_LOG = 1;
		public static int CURRENT_CHARACTEREVENTS_LOG = 1;
		public static int CURRENT_STRONGHOLDEVENTS_LOG = 1;

		#endregion

		#endregion

		#region Constructor

		public Consts()
		{
			actionsData = new actionStruct[(int)CharacterState.Count];
			actionsData[(int)CharacterState.BuyingFood]._actionDuration = 60;
			actionsData[(int)CharacterState.BuyingFood]._actionPriority = 4;
			actionsData[(int)CharacterState.Eating]._actionDuration = 60;
			actionsData[(int)CharacterState.Eating]._actionPriority = 5;
			actionsData[(int)CharacterState.LookingForPlaceToLive]._actionPriority = 10;
			actionsData[(int)CharacterState.Sleeping]._actionDuration = 480;
			actionsData[(int)CharacterState.Sleeping]._actionPriority = 20;
			actionsData[(int)CharacterState.Working]._actionPriority = 50;
			actionsData[(int)CharacterState.Idle]._actionDuration = 0;
			actionsData[(int)CharacterState.Idle]._actionPriority = 99;

			hut_hp = new Status(100);
			farm_hp = new Status(50);
			granary_hp = new Status(200);
			employmentoffice_hp = new Status(1000);

			farmerBeginTime = new Gametime(0, 5, 0);
			farmerEndTime = new Gametime(0, 15, 0);

			granaryKeeperBeginTime = new Gametime(0, 8, 0);
			granaryKeeperEndTime = new Gametime(0, 20, 0);

			builderBeginTime = new Gametime(0, 5, 0);
			builderEndTime = new Gametime(0, 23, 0);
		}

		#endregion
	}
}
