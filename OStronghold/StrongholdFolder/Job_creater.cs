using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OStronghold.GenericFolder;

namespace OStronghold.StrongholdFolder
{
    public class Job_creater
    {
        #region Farmer
        public static Job createFarmerJob(int buildingID)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                                    Consts.JobStatus.Available);
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return farmerJob;
        }//create farmer job
        #endregion

        #region Granary keeper
        public static Job createGranaryKeeper(int buildingID)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                                           Consts.JobStatus.Available);
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return granaryKeeperJob;
        }
        #endregion
    }
}
