using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.Generic
{
    public class Job
    {
        #region Members

        private int _jobID; //job id (unique)
        private int _ownerID; //id of job owner - the one who is paying
        private int _workerID; //id of worker - the one who is working and getting paid
        private string _jobName; //job name                
        private Gametime _startDate; //date when job is available
        private Gametime _endDate; //date when job is not relevant anymore
        private Gametime _startTime; //time when character must start to work
        private Gametime _endTime; //time when character stops to work
        private int _payroll; //amount of money character receives per starttime to endtime
        private Consts.JobStatus _jobStatus; //status of the Job - taken or available

        public int JobID
        {
            get { return _jobID; }
        }
        public int OwnerID
        {
            get { return _ownerID; }            
        }        
        public int WorkerID
        {
            get { return _workerID; }
            set { _workerID = value; }
        }
        public string JobName
        {
            get { return _jobName; }
        }
        public Gametime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public Gametime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        public Gametime StartTime
        {
            get { return _startTime; }
        }
        public Gametime EndTime
        {
            get { return _endTime; }
        }
        public int Payroll
        {
            get { return _payroll; }
        }
        public Consts.JobStatus JobStatus
        {
            get {return _jobStatus;}
            set { _jobStatus = value; }
        }

        #endregion

        #region Constructor

        public Job()
        {
        }

        public Job(int jobID, int ownerID, int workerID, string jobName, Gametime startDate, Gametime endDate, Gametime startTime, Gametime endTime, int payroll, Consts.JobStatus jobStatus)
        {
            if (startDate <= endDate &&
                startTime <= endTime)
            {
                _jobID = jobID;
                _ownerID = ownerID;
                _workerID = workerID;
                _jobName = String.Copy(jobName);
                _startDate = new Gametime();
                _startDate.CopyGameTime(startDate);
                _endDate = new Gametime();
                _endDate.CopyGameTime(endDate);
                _startTime = new Gametime();
                _startTime.CopyGameTime(startTime);
                _endTime = new Gametime();
                _endTime.CopyGameTime(endTime);
                _payroll = payroll;
                _jobStatus = jobStatus;
            }
        }

        public Job(Job targetJob)
        {
            Clone(targetJob);
        }

        #endregion

        #region Methods

        public void Clone(Job targetJob)
        {
            _jobID = targetJob.JobID;
            _ownerID = targetJob.OwnerID;
            _workerID = targetJob.WorkerID;
            _jobName = String.Copy(targetJob.JobName);
            _startDate = new Gametime();
            _startDate.CopyGameTime(targetJob.StartDate);
            _endDate = new Gametime();
            _endDate.CopyGameTime(targetJob.EndDate);
            _startTime = new Gametime();
            _startTime.CopyGameTime(targetJob.StartTime);
            _endTime = new Gametime();
            _endTime.CopyGameTime(targetJob.EndTime);
            _payroll = targetJob.Payroll;
            _jobStatus = targetJob.JobStatus;
        }
        


        #endregion
    }
}
