﻿
using Stronghold.Windows;

namespace Stronghold
{
	public class Job_creater
	{
		#region Farmer
		public static Job createFarmerJob(int buildingID)
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			int jobID = Program._aStronghold._allJobs.Count + 1;

			Job farmerJob = new Job(jobID,
									buildingID,
									Program._aStronghold._leader._id,
									Consts.noworker,
									Consts.farmerName,
									Program._gametime,
									Program._gametime + Consts.farmerJobDuration,
									Consts.farmerBeginTime,
									Consts.farmerEndTime,
									Consts.farmerPayroll,
									JobStatus.Available);
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			return farmerJob;
		}//create farmer job
		#endregion

		#region Granary keeper
		public static Job createGranaryKeeper(int buildingID)
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			int jobID = Program._aStronghold._allJobs.Count + 1;
			Job granaryKeeperJob = new Job(jobID,
										   buildingID,
										   Program._aStronghold._leader._id,
										   Consts.noworker,
										   Consts.granaryKeeperName,
										   Program._gametime,
										   Program._gametime + Consts.granaryKeeperJobDuration,
										   Consts.granaryKeeperBeginTime,
										   Consts.granaryKeeperEndTime,
										   Consts.granaryKeeperPayroll,
										   JobStatus.Available);
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			return granaryKeeperJob;
		}
		#endregion

		#region Builder

		//public static Job createBuilderJob(int buildingID, int jobID)
		//{
		//    //Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);            
		//    Job builderJob = new Job(jobID,
		//                              buildingID,
		//                              9999,
		//                              Consts.noworker,
		//                              Consts.builderName,
		//                              Program._gametime,
		//                              Program._gametime + Consts.builderJobDuration,
		//                              Consts.builderBeginTime,
		//                              Consts.builderEndTime,
		//                              Consts.builderPayroll,
		//                              JobStatus.Available);
		//    //Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
		//    return builderJob;
		//}

		public static Job createBuilderJob(int buildingID)
		{
			//Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			int jobID = Program._aStronghold._allJobs.Count + 1;
			Job builderJob = new Job(jobID,
									  buildingID,
									  Program._aStronghold._leader._id,
									  Consts.noworker,
									  Consts.builderName,
									  Program._gametime,
									  Program._gametime + Consts.builderJobDuration,
									  Consts.builderBeginTime,
									  Consts.builderEndTime,
									  Consts.builderPayroll,
									  JobStatus.Available);
			//Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
			return builderJob;
		}
		#endregion
	}
}
