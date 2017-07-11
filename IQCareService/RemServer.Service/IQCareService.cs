using Application.BusinessProcess;
using Application.Common;
using DataAccess.Common;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Install;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.ServiceProcess;
//for auto backup service
using System.Threading;

namespace RemServer.Service
{
    public class IQCareService : ServiceBase
    {
        /// <summary>
        /// The sr v_ name
        /// </summary>
        public static string theSRV_Name = "IQCare Service";

        /// <summary>
        /// The _connection string emr
        /// </summary>
        public string _connectionStringEMR;

        /// <summary>
        /// The dt backup time
        /// </summary>
        public DateTime? dtBackupTime = null;

        /// <summary>
        /// The string backup drive
        /// </summary>
        public string strBackupDrive = "";

        private static List<Facility> _facilities;

        /// <summary>
        /// The log
        /// </summary>
        private static EventLog theLog = new EventLog();

        /// <summary>
        /// The CLS utility
        /// </summary>
        private Utility clsUtil;
        bool backupInProgress = false;
        /// <summary>
        /// The connection emr
        /// </summary>
        private SqlConnection connectionEMR;

        /// <summary>
        /// The o timer
        /// </summary>
        private System.Threading.Timer oTimer;

        /// <summary>
        /// The container
        /// </summary>
        private System.ComponentModel.Container theContainer = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="IQCareService"/> class.
        /// </summary>
        public IQCareService()
        {
            this.ServiceName = RemServer.Service.IQCareService.GetServiceName();// GetServiceName();// theSRV_Name;//
        }

        /// <summary>
        /// Gets the configuration file.
        /// </summary>
        /// <value>
        /// The configuration file.
        /// </value>
        private string ConfigFile
        {
            get
            {
                Process theProc = Process.GetCurrentProcess();
                string strConfig = theProc.MainModule.FileName;
                strConfig = strConfig + ".config";
                return strConfig;
            }
        }

        /// <summary>
        /// Gets the facility list.
        /// </summary>
        /// <value>
        /// The facility list.
        /// </value>
        private List<Facility> FacilityList
        {
            get
            {
                if (_facilities == null)
                {
                    FacilityDetails();
                }
                return _facilities;
            }
        }

        void UpdateNextRunDate(string taskName, int offSet)
        {

            ClsObject obj = new ClsObject();
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@NextRunDate", SqlDbType.DateTime, DateTime.Now.AddMinutes(offSet));
                ClsUtility.AddExtendedParameters("@LastRunDate", SqlDbType.DateTime, DateTime.Now);
                ClsUtility.AddParameters("@TaskName", SqlDbType.VarChar, taskName);
                obj.ReturnObject(ClsUtility.theParams, "Schedule_UpdateTask", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
            obj = null;
        }

        public void DBEntry(Object Message)
        {

            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            List<ScheduledTask> tasks = null;

            DateTime? nullDate = null;
            string appointmentTaskName = "Appointment.Update";
            string backupTaskName = "Database.Backup";
            string iqtoolTaskName = "IQTools.Update";

            if (backupInProgress == false)
            {
                //clean up the waiting list
                try
                {
                    //WaitingList_SystemCleanup
                    using (SqlCommand command = new SqlCommand("WaitingList_SystemCleanup", connectionEMR))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (connectionEMR.State == ConnectionState.Closed)
                            connectionEMR.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    theLog.WriteEntry(ex.Message + ex.StackTrace);
                }
                //Get scheduled jobs
                try
                {
                   ClsObject obj = new ClsObject();
                        ClsUtility.Init_Hashtable();

                        DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Schedule_GetTask", ClsUtility.ObjectEnum.DataTable);
                        tasks = new List<ScheduledTask>();
                        foreach (DataRow row in dt.Rows)
                        {
                            tasks.Add(new ScheduledTask
                            {
                                TaskName = row["TaskName"].ToString(),
                                LastRunDate = row["LastRunDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(row["LastRunDate"]),
                                NextRunDate = row["NextRunDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(row["NextRunDate"]),
                            });
                        }

                    obj = null; 
                }
                catch (Exception ex)
                {
                    theLog.WriteEntry(ex.Message + ex.StackTrace);
                }

                //update appointments
                try
                {
                    ScheduledTask appointmentTask = (tasks.FirstOrDefault(t => t.TaskName == appointmentTaskName));
                    if (appointmentTask != null)
                    {
                        if (appointmentTask.NextRunDate == nullDate || appointmentTask.NextRunDate < DateTime.Now)
                        {
                            int appointmentOffset = 60;
                            if (ConfigurationManager.AppSettings["AppointmentUpdateInterval"] != null)
                            {
                                appointmentOffset = Convert.ToInt32(ConfigurationManager.AppSettings["AppointmentUpdateInterval"]);
                            }
                            UpdateNextRunDate(appointmentTask.TaskName, appointmentOffset);
                            Facility[] facilities = FacilityList.ToArray();

                            ThreadPool.QueueUserWorkItem(UpdateAppointment, facilities);

                        }
                    }



                }
                catch (Exception ex)
                {
                    theLog.WriteEntry(ex.Message + ex.StackTrace);
                }
                //refresh iqtools
                try
                {
                    ScheduledTask iQToolsTask = (tasks.FirstOrDefault(t => t.TaskName == iqtoolTaskName));

                    if (iQToolsTask != null && (ConfigurationManager.AppSettings["IQToolsConnectionString"] != null))
                    {
                        if (iQToolsTask.NextRunDate == nullDate || iQToolsTask.NextRunDate < DateTime.Now)
                        {
                            int offset = 300;
                            if (ConfigurationManager.AppSettings["IQToolsRefreshInterval"] != null)
                            {
                                offset = Convert.ToInt32(ConfigurationManager.AppSettings["IQToolsRefreshInterval"]);
                            }
                            UpdateNextRunDate(iQToolsTask.TaskName, offset);
                            ThreadPool.QueueUserWorkItem(new WaitCallback(IQToolsRefresh));

                        }
                    }
                }
                catch (Exception ex)
                {
                    theLog.WriteEntry(ex.Message + ex.StackTrace);
                }


                //try
                //{
                //    DateTime dateNextRefreshDate;
                //    if (ConfigurationManager.AppSettings["AppointmentNextUpdate"] != null)
                //    {
                //        dateNextRefreshDate = Convert.ToDateTime(ConfigurationManager.AppSettings["AppointmentNextUpdate"]);
                //    }
                //    else
                //    {
                //        dateNextRefreshDate = DateTime.Now.AddMinutes(15);
                //    }
                //    if (dateNextRefreshDate <= DateTime.Now)
                //    {
                //        //update the AppointmentNextUpdate - to avoid run on subsequent poll
                //        config.AppSettings.Settings["AppointmentNextUpdate"].Value = DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd hh:mm:ss");
                //        config.Save(ConfigurationSaveMode.Modified);
                //        ConfigurationManager.RefreshSection("appSettings");
                //        //update appointment
                //        foreach (Facility f in FacilityList)
                //        {
                //            UpdateAppointment(f.ID, dateNextRefreshDate.ToString("yyyy-MMM-dd"));
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    theLog.WriteEntry(ex.Message + ex.StackTrace);
                //}
            }
            //backup the database
            try
            {
                ScheduledTask backupTask = (tasks.FirstOrDefault(t => t.TaskName == backupTaskName));
                if (backupTask != null)
                {
                    if (backupTask.NextRunDate == nullDate || backupTask.NextRunDate < DateTime.Now)
                    {
                        int backupOffset = 720;
                        if (ConfigurationManager.AppSettings["DatabaseBackupInterval"] != null)
                        {
                            backupOffset = Convert.ToInt32(ConfigurationManager.AppSettings["DatabaseBackupInterval"]);
                        }
                        if (backupInProgress == false)
                        {
                            backupInProgress = true;
                            UpdateNextRunDate(backupTaskName, backupOffset);
                            SqlCommand cmdTest;
                            cmdTest = new SqlCommand("pr_SystemAdmin_GetBackupTime_Constella", connectionEMR);
                            cmdTest.CommandType = CommandType.StoredProcedure;
                            int theTimeOut = int.Parse(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                            cmdTest.CommandTimeout = theTimeOut;
                            if (connectionEMR.State == ConnectionState.Closed)
                                connectionEMR.Open();
                            using (SqlDataReader readerBackupDetail = cmdTest.ExecuteReader())
                            {
                                if (readerBackupDetail.HasRows)
                                {
                                    readerBackupDetail.Read();
                                    if (readerBackupDetail["BackupTime"].ToString() != "" || readerBackupDetail.IsDBNull(0) != true)
                                        dtBackupTime = (DateTime)readerBackupDetail["BackupTime"];
                                    if (readerBackupDetail["BackupDrive"].ToString() != "" || readerBackupDetail.IsDBNull(1) != true)
                                        strBackupDrive = (string)readerBackupDetail["BackupDrive"];
                                }
                            }
                            if (!string.IsNullOrEmpty(strBackupDrive))
                            {
                                //this.RequestAdditionalTime(50000);
                                theLog.WriteEntry("IQCare database backup started at " + DateTime.Now.ToLongDateString());
                                cmdTest = new SqlCommand("pr_SystemAdmin_Backup_Constella", connectionEMR);
                                cmdTest.CommandType = CommandType.StoredProcedure;
                                cmdTest.Parameters.Add(new SqlParameter("@FileName", SqlDbType.VarChar, 500));
                                cmdTest.Parameters["@FileName"].Value = strBackupDrive + "\\IQCareDBBackup";
                                cmdTest.Parameters.Add("@Deidentified", SqlDbType.Int).Value = 0;
                                cmdTest.Parameters.Add("@LocationId", SqlDbType.Int).Value = 0;
                                cmdTest.Parameters.Add("@dbKey", SqlDbType.VarChar).Value = ApplicationAccess.DBSecurity.ToString();
                                cmdTest.CommandTimeout = theTimeOut;
                                // connectionEMR.Open();
                                cmdTest.ExecuteNonQuery();
                                theLog.WriteEntry("IQCare database backup completed at " + DateTime.Now.ToLongDateString());
                            }
                            connectionEMR.Close();
                            UpdateNextRunDate(backupTaskName, backupOffset);
                            backupInProgress = false;
                        }
                        //UpdateNextRunDate(backupTask.TaskName, backupOffset);


                    }
                }


                ;
            }
            catch (Exception err)
            {
                backupInProgress = false;
                connectionEMR.Close();
                theLog.WriteEntry(err.Message + err.StackTrace);
            }
       

            //ThreadPool.QueueUserWorkItem(new WaitCallback(IQToolsRefresh));
        }

        /// <summary>
        /// This process will pick the backup time and backdrive from database.
        /// </summary>
        public void DoDelayedTasks()
        {
            connectionEMR = new SqlConnection(_connectionStringEMR);
             int iTIME_INTERVAL = 120000; //    ' 2 minutes.
            TimerCallback timerDelegate = new TimerCallback(DBEntry);
            oTimer = new Timer(timerDelegate, null, 0, iTIME_INTERVAL);
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the <see cref="T:System.ServiceProcess.ServiceBase" />.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (theContainer != null)
                {
                    theContainer.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            try
            {
                Process theProc = Process.GetCurrentProcess();
                string Config = theProc.MainModule.FileName;
                Config = Config + ".config";
                #region "Connection Parameters"

                clsUtil = new Utility();
                _connectionStringEMR = clsUtil.Decrypt(ConfigurationManager.AppSettings["ConnectionString"].ToString());
                if (_connectionStringEMR.Trim() == "")
                {

                    _connectionStringEMR = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                    if (_connectionStringEMR == "")
                    {
                        Environment.Exit(1);
                    }
                }

                #endregion "Connection Parameters"

                DoDelayedTasks();
                RemotingConfiguration.Configure(Config, false);
                RemotingConfiguration.ApplicationName = "IQCAREEMR";
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(BusinessServerFactory), "BusinessProcess.rem", WellKnownObjectMode.Singleton);
                theLog.WriteEntry(string.Format("{0} Started", theSRV_Name));
            }
            catch (Exception err)
            {
                theLog.WriteEntry(err.Message + err.StackTrace);
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                theLog.WriteEntry(string.Format("{0} Stopped.", theSRV_Name));
            }
            catch (Exception err)
            {
                theLog.WriteEntry(err.Message);
            }
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        /// <returns></returns>
        private static string GetServiceName()
        {
            Process theProc = Process.GetCurrentProcess();
            string Config = theProc.MainModule.FileName;
            Config = Config + ".config";
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string s = config.AppSettings.Settings["ServiceName"].Value;
            return s;
        }

        /// <summary>
        /// Installs the service.
        /// </summary>
        private static void InstallService()
        {
            if (IsServiceInstalled())
            {
                UninstallService();
            }

            ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
            theLog.Source = "IQCare";
            theLog.WriteEntry(string.Format("{0} Initializing", "IQCare Service"));
        }

        /// <summary>
        /// Determines whether [is service installed].
        /// </summary>
        /// <returns></returns>
        private static bool IsServiceInstalled()
        {
            return ServiceController.GetServices().Any(s => s.ServiceName == theSRV_Name);
        }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    for (int ii = 0; ii < args.Length; ii++)
                    {
                        switch (args[ii].ToUpper())
                        {
                            case "/I":
                                InstallService();
                                return;

                            case "/U":
                                UninstallService();
                                return;

                            default:
                                break;
                        }
                    }
                }
                else
                {
                    theLog.Source = "IQCare";
                    theLog.WriteEntry(string.Format("{0} Initializing", "IQCare Service"));
                    ServiceBase.Run(new IQCareService());
                }
                //theLog.Log = "IQCare";
            }
            catch (Exception err)
            {
                theLog.WriteEntry(err.Message + err.StackTrace);
            }
        }

        /// <summary>
        /// Uninstalls the service.
        /// </summary>
        private static void UninstallService()
        {
            ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
            theLog.Source = "IQCare";
            theLog.WriteEntry(string.Format("{0} Unistalling", "IQCare Service"));
        }

        /// <summary>
        /// Facilities the name.
        /// </summary>
        /// <returns></returns>
        private void FacilityDetails()
        {
            if (_facilities == null)
            {
                SqlCommand cmdTest;
                connectionEMR = new SqlConnection(_connectionStringEMR);
                cmdTest = new SqlCommand(@"Select	F.FacilityID,F.FacilityName From dbo.mst_Facility F Where F.DeleteFlag = 0 Or F.DeleteFlag Is Null Order By 1", connectionEMR);
                connectionEMR.Open();
                IDataReader dr = cmdTest.ExecuteReader(CommandBehavior.Default);
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();
                _facilities = new List<Facility>();
                foreach (DataRow row in dt.Rows)
                {
                    _facilities.Add(new Facility { ID = int.Parse(row["FacilityID"].ToString()), Name = row["FacilityName"].ToString() });
                }
            }
        }

        private void GetBackupDetails()
        {
            try
            {
                SqlCommand cmdTest;
                cmdTest = new SqlCommand("pr_SystemAdmin_GetBackupTime_Constella", connectionEMR);
                cmdTest.CommandType = CommandType.StoredProcedure;
                //int theTimeOut = Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["CommandTimeOut"]);
                int theTimeOut = int.Parse(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                cmdTest.CommandTimeout = theTimeOut;
                if (connectionEMR.State == ConnectionState.Closed)
                    connectionEMR.Open();
                using (SqlDataReader readerBackupDetail = cmdTest.ExecuteReader())
                {
                    if (readerBackupDetail.HasRows)
                    {
                        readerBackupDetail.Read();
                        if (readerBackupDetail["BackupTime"].ToString() != "" || readerBackupDetail.IsDBNull(0) != true)
                            dtBackupTime = (DateTime)readerBackupDetail["BackupTime"];
                        if (readerBackupDetail["BackupDrive"].ToString() != "" || readerBackupDetail.IsDBNull(1) != true)
                            strBackupDrive = (string)readerBackupDetail["BackupDrive"];
                    }
                }

                //this.RequestAdditionalTime(50000);
                cmdTest = new SqlCommand("pr_SystemAdmin_Backup_Constella", connectionEMR);
                cmdTest.CommandType = CommandType.StoredProcedure;
                cmdTest.Parameters.Add(new SqlParameter("@FileName", SqlDbType.VarChar, 500));
                cmdTest.Parameters["@FileName"].Value = strBackupDrive + "\\IQCareDBBackup";
                cmdTest.Parameters.Add("@Deidentified", SqlDbType.Int).Value = 0;
                cmdTest.Parameters.Add("@LocationId", SqlDbType.Int).Value = 0;
                cmdTest.Parameters.Add("@dbKey", SqlDbType.VarChar).Value = ApplicationAccess.DBSecurity.ToString();
                cmdTest.CommandTimeout = theTimeOut;
                // connectionEMR.Open();
                cmdTest.ExecuteNonQuery();

                connectionEMR.Close();
            }
            catch (Exception err)
            {
                connectionEMR.Close();
                theLog.WriteEntry(err.Message + err.StackTrace);
            }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            theContainer = new System.ComponentModel.Container();
            this.ServiceName = "IQCare Service";
        }

        /// <summary>
        /// Iqs the tools refresh.
        /// </summary>
        private void IQToolsRefresh(object StateInfo)
        {
            try
            {
                string strIQToolsConnection = "";
                string strIQToolsInit = "";
                DateTime dateNextRefreshDate = DateTime.Now;
                if (clsUtil == null) clsUtil = new Utility();
                bool isError = false;
                if (ConfigurationManager.AppSettings["IQToolsConnectionString"] == null)
                    return;

                try
                {
                    strIQToolsConnection = clsUtil.Decrypt(ConfigurationManager.AppSettings.Get("IQToolsConnectionString"));
                }
                catch (Exception e1)
                {
                    theLog.WriteEntry(e1.Message + e1.StackTrace + " IQToolsConnectionString");
                    isError = true;
                }
                try
                {
                    strIQToolsInit = clsUtil.Decrypt(ConfigurationManager.AppSettings["IQToolsInitializationProcedures"]);
                }
                catch (Exception e1)
                {
                    theLog.WriteEntry(e1.Message + e1.StackTrace + " IQToolsInitializationProcedures");
                    isError = true;
                }

                //try
                //{
                //    //XmlConvert.ToDateTime()
                //    dateNextRefreshDate = Convert.ToDateTime(ConfigurationManager.AppSettings["IQToolsNextRefreshDateTime"]);
                //}
                //catch (Exception e1)
                //{
                //    theLog.WriteEntry(e1.Message + e1.StackTrace + " IQToolsNextRefreshDateTime");
                //    isError = true;
                //}
                if (isError) throw new Exception("Errocurred when parsing the config file");

                if (strIQToolsConnection != "")
                {
                    // int refreshInterval = 300;
                    // int.TryParse(ConfigurationManager.AppSettings["IQToolsRefreshInterval"], out refreshInterval);
                    // string strIQToolsRefreshTime = (DateTime.Now.AddMinutes(refreshInterval)).ToString("yyyy-MM-dd hh:mm:ss");
                    //  Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    //config.AppSettings.Settings["IQToolsNextRefreshDateTime"].Value = strIQToolsRefreshTime;
                    // config.Save(ConfigurationSaveMode.Modified);
                    // ConfigurationManager.RefreshSection("appSettings");

                    SqlConnection connectionIQtools = new SqlConnection(strIQToolsConnection);
                    connectionIQtools.Open();
                    SqlCommand command;

                    string[] procedures = strIQToolsInit.Split(';');
                    string facilityName = FacilityList[0].Name;
                    foreach (string procedure in procedures)
                    {
                        try
                        {
                            command = new SqlCommand(procedure, connectionIQtools);
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;
                            SqlCommandBuilder.DeriveParameters(command);
                            command.Parameters["@EMR"].Value = "IQCare";
                            command.Parameters["@FacilityName"].Value = facilityName;
                            if (command.Parameters.Contains("@PatientPK"))
                                command.Parameters["@PatientPK"].Value = 0;
                            if (command.Parameters.Contains("@EMRVersion"))
                                command.Parameters["@EMRVersion"].Value = "3.5";
                            if (command.Parameters.Contains("@VisitPK"))
                                command.Parameters["@VisitPK"].Value = 0;
                            command.ExecuteNonQuery();
                        }
                        catch (Exception e2)
                        {
                            theLog.WriteEntry(e2.Message + e2.StackTrace + procedure);
                        }
                    }

                    connectionIQtools.Close();
                    connectionIQtools.Dispose();
                }
            }
            catch (Exception e0)
            {
                theLog.WriteEntry(e0.Message + e0.StackTrace);
            }
        }

        /// <summary>
        /// For automated backup service.
        /// This function takes the backup when server time matches with user specified time.
        /// </summary>
        /// <param name="Message"></param>
        ///
        private void PerformBackup()
        {
            try
            {
                SqlCommand cmdTest;
                cmdTest = new SqlCommand("pr_SystemAdmin_GetBackupTime_Constella", connectionEMR);
                cmdTest.CommandType = CommandType.StoredProcedure;
                //int theTimeOut = Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["CommandTimeOut"]);
                int theTimeOut = int.Parse(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                cmdTest.CommandTimeout = theTimeOut;
                if (connectionEMR.State == ConnectionState.Closed)
                    connectionEMR.Open();
                cmdTest = new SqlCommand("pr_SystemAdmin_Backup_Constella", connectionEMR);
                cmdTest.CommandType = CommandType.StoredProcedure;
                cmdTest.Parameters.Add(new SqlParameter("@FileName", SqlDbType.VarChar, 500));
                cmdTest.Parameters["@FileName"].Value = strBackupDrive + "\\IQCareDBBackup";
                cmdTest.Parameters.Add("@Deidentified", SqlDbType.Int).Value = 0;
                cmdTest.Parameters.Add("@LocationId", SqlDbType.Int).Value = 0;
                cmdTest.Parameters.Add("@dbKey", SqlDbType.VarChar).Value = ApplicationAccess.DBSecurity.ToString();
                cmdTest.CommandTimeout = theTimeOut;
                // connectionEMR.Open();
                cmdTest.ExecuteNonQuery();
                connectionEMR.Close();
            }
            catch (Exception err)
            {
                connectionEMR.Close();
                theLog.WriteEntry(err.Message + err.StackTrace);
            }
        }

        /// <summary>
        /// Updates the appointment.
        /// </summary>
        private void UpdateAppointment(object state)
        {
            //*******Update appointment status priviously missed, missed, careended and met from pending*******//pr_Scheduler_UpdateAppointmentStatusMissedAndMet_Constella
            Facility[] array = state as Facility[];
            foreach (Facility facility in array)
            {
                try
                {
                    ClsObject obj = new ClsObject();
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddExtendedParameters("@locationid", SqlDbType.Int, facility.ID);
                        ClsUtility.AddExtendedParameters("@CurrentDate", SqlDbType.DateTime, DateTime.Now);                        
                        obj.ReturnObject(ClsUtility.theParams, "pr_Scheduler_UpdateAppointmentStatusMissedAndMet_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    obj = null;
                }
                catch (Exception err) { theLog.WriteEntry(err.Message + err.StackTrace + (err.InnerException != null ? err.InnerException.Message : "")); }
                try
                {
                    ClsObject obj = new ClsObject();
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddExtendedParameters("@locationid", SqlDbType.Int, facility.ID);
                        ClsUtility.AddExtendedParameters("@CurrentDate", SqlDbType.DateTime, DateTime.Now);
                        obj.ReturnObject(ClsUtility.theParams, "pr_Scheduler_UpdateAppointmentStatus", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    obj = null;
                }
                catch (Exception err) { theLog.WriteEntry(err.Message + err.StackTrace + (err.InnerException != null ? err.InnerException.Message : "")); }
            }


        }
    }
    internal class ScheduledTask
    {
        public string TaskName { get; set; }
        public DateTime? LastRunDate { get; set; }
        public DateTime? NextRunDate { get; set; }
    }
    internal class Facility
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; internal set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }
    }
}