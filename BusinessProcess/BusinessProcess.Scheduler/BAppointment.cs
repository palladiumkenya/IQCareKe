using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Scheduler;
using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessProcess.Scheduler
{
    /// <summary>
    /// 
    /// </summary>
    public class BAppointment : ProcessBase, IAppointment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BAppointment" /> class.
        /// </summary>
        public BAppointment()
        {
        }

        /// <summary>
        /// Checks the appointment existance.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="AppDate">The application date.</param>
        /// <param name="ReasonId">The reason identifier.</param>
        /// <param name="visitId">The visit identifier.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        public DataTable CheckAppointmentExistance(int patientPk, int locationId, DateTime AppDate, 
            int reasonId, 
            int visitId = 0, 
            int?   moduleId = null)
        {
            //lock (this)
            //{
                try
                {
                    DataTable theDt;

                    //this.Connection = DataMgr.GetConnection();
                   // this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject obj = new ClsObject();
                    obj.Connection = this.Connection;
                 //   obj.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientPk);
                    ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
                    ClsUtility.AddExtendedParameters("@AppDate", SqlDbType.DateTime, AppDate);
                    ClsUtility.AddExtendedParameters("@ReasonId", SqlDbType.Int, reasonId);
                    ClsUtility.AddExtendedParameters("@visitId", SqlDbType.Int, visitId);
                    if (moduleId.HasValue)
                    {
                        ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId.Value);
                    }
                    theDt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "pr_Scheduler_CheckAppointmentExistance_Constella", ClsUtility.ObjectEnum.DataTable);

                  //  DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    return theDt;
                }
                catch
                {
                   // DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
           // }
        }

        /// <summary>
        /// Deletes the patient appointment details.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public int DeletePatientAppointmentDetails(int appointmentId, int userId)
        {
            lock (this)
            {
                try
                {
                    int theAffectedRows = 0;
                   // this.Connection = DataMgr.GetConnection();
                   // this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject obj = new ClsObject();
                   // SaveAppointment.Connection = this.Connection;
                    //SaveAppointment.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@AppointmentId", SqlDbType.Int, appointmentId);
                    ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
                    theAffectedRows = (int)obj.ReturnObject(ClsUtility.theParams, "pr_Scheduler_DeletePatientAppointmentDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    obj = null;
                   // DataMgr.CommitTransaction(this.Transaction);
                    //DataMgr.ReleaseConnection(this.Connection);
                    return theAffectedRows;
                }
                catch
                {
                  //  DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    //if (this.Connection != null)
                      //  DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        public DataTable GetAppointmentList(int locationId, int? patientPk = null, int? moduleId = null, int? visitId = null, int? AppStatus = null, DateTime? FromDate = null, DateTime? ToDate = null, int? AppointmentReason = null)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject aMGR = new ClsObject();
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                if (AppStatus.HasValue)
                    ClsUtility.AddExtendedParameters("@AppStatus", SqlDbType.Int, AppStatus.Value);
                if (FromDate.HasValue)
                    ClsUtility.AddExtendedParameters("@FromDate", SqlDbType.DateTime, FromDate.Value);
                if (ToDate.HasValue)
                    ClsUtility.AddExtendedParameters("@ToDate", SqlDbType.DateTime, ToDate.Value);
                if (AppointmentReason.HasValue)
                    ClsUtility.AddExtendedParameters("@AppReason", SqlDbType.Int, AppointmentReason.Value);
                if (patientPk.HasValue)
                    ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientPk.Value);
                if (moduleId.HasValue)
                    ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId.Value);
                if (visitId.HasValue)
                    ClsUtility.AddExtendedParameters("@VisitId", SqlDbType.Int, visitId.Value);
                return (DataTable)aMGR.ReturnObject(ClsUtility.theParams, "pr_Scheduler_AppointmentList_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
    
        /// <summary>
        /// Gets the appointment status.
        /// </summary>
        /// <returns></returns>
        public DataSet GetAppointmentStatus()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AppointmentStatus = new ClsObject();
                return (DataSet)AppointmentStatus.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SelectAppStatus_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #region "Modified13June07(1)"

        /// <summary>
        /// Gets the appointment reasons.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public DataSet GetAppointmentReasons(int Id)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AppointmentReasons = new ClsObject();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());

                return (DataSet)AppointmentReasons.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SelectAppReason_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public DataSet GetEmployees(int Id)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject Employees = new ClsObject();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());

                return (DataSet)Employees.ReturnObject(ClsUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion "Modified13June07(1)"

        /// <summary>
        /// Gets the patientppointment details.
        /// </summary>
        /// <param name="patientPk">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="visitId">The visit identifier.</param>
        /// <returns></returns>
        public DataSet GetPatientppointmentDetails(int patientPk, int locationId, int visitId)
        {
            lock (this)
            {
                try
                {
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject obj = new ClsObject();
                    //obj.Connection = this.Connection;
                    //obj.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientId", SqlDbType.Int, patientPk.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                    ClsUtility.AddParameters("@VisitId", SqlDbType.Int, visitId.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    return (DataSet)obj.ReturnObject(ClsUtility.theParams, "pr_Scheduler_GetPatientAppointmentDetails_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    //  DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
   /// <summary>
        /// Saves the appointment.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        /// <returns></returns>
        public int SaveAppointment(Entities.Administration.Appointment appointment)
        {
           
            //lock (this)
            //{
                try
                {
                  DataTable dt =  this.CheckAppointmentExistance(appointment.PatientId, 
                        appointment.Location.Key, 
                        appointment.AppointmentDate, 
                        appointment.Purpose.Key, 
                        appointment.VisitId.HasValue? appointment.VisitId.Value : 0,
                        appointment.ServiceArea.Key);

                  if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                  {
                      throw new Exception("An appointment exists ");
                  }
                    int theAffectedRows = 0;
                   // this.Connection = DataMgr.GetConnection();
                   // this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject obj = new ClsObject();
                 //   obj.Connection = this.Connection;
                 //   obj.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, appointment.PatientId);
                    ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, appointment.Location.Key);
                    ClsUtility.AddExtendedParameters("@AppDate", SqlDbType.DateTime, appointment.AppointmentDate);
                    ClsUtility.AddExtendedParameters("@AppReasonId", SqlDbType.Int, appointment.Purpose.Key);
                    if (!appointment.Provider.Equals(default(KeyValuePair<int, string>)))
                    {
                        
                            ClsUtility.AddExtendedParameters("@AppProviderId", SqlDbType.Int, appointment.Provider.Key);
                    }
                    ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, appointment.BookedBy.Key);
                    ClsUtility.AddExtendedParameters("@CreateDate", SqlDbType.DateTime, appointment.StatusDate);
                    ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, appointment.ServiceArea.Key);
                    ClsUtility.AddParameters("@AppNote", SqlDbType.VarChar, appointment.Notes);

                    theAffectedRows = (int)obj.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SaveAppointment_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                obj = null;

                //  DataMgr.CommitTransaction(this.Transaction);
                // DataMgr.ReleaseConnection(this.Connection);
                return theAffectedRows;
                }
                catch
                {
                  //  DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                  //  if (this.Connection != null)
                   //     DataMgr.ReleaseConnection(this.Connection);
                }
           // }
        }

        /// <summary>
        /// Searches the patient appointment.
        /// </summary>
        /// <param name="LName">Name of the l.</param>
        /// <param name="FName">Name of the f.</param>
        /// <param name="patientPk">The patient identifier.</param>
        /// <param name="HospitalID">The hospital identifier.</param>
        /// <param name="DOB">The dob.</param>
        /// <param name="Sex">The sex.</param>
        /// <param name="AppStatus">The application status.</param>
        /// <returns></returns>
        public DataSet SearchPatientAppointment(string LName, string FName, int patientPk, string HospitalID, DateTime DOB, int Sex, int AppStatus)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SchedulerMgr = new ClsObject();
                SchedulerMgr.Connection = this.Connection;
                SchedulerMgr.Transaction = this.Transaction;

                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, LName);
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, FName);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientPk.ToString());
                ClsUtility.AddParameters("@HospitalID", SqlDbType.VarChar, HospitalID);
                ClsUtility.AddParameters("@DOB", SqlDbType.DateTime, DOB.ToString());
                ClsUtility.AddParameters("@Sex", SqlDbType.Int, Sex.ToString());
                ClsUtility.AddParameters("@AppStatus", SqlDbType.Int, AppStatus.ToString());

                DataSet SchedulerDR;
                SchedulerDR = (DataSet)SchedulerMgr.ReturnObject(ClsUtility.theParams, "pr_Scheduler_Search_PatientAppointment_Constella", ClsUtility.ObjectEnum.DataSet);

                return SchedulerDR;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                {
                    DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }

        /// <summary>
        /// Updates the patientppointment.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        /// <returns></returns>
        public int UpdatePatientppointment(Entities.Administration.Appointment appointment)
        {
            lock (this)
            {
                try
                {
                    DataTable dt = this.CheckAppointmentExistance(appointment.PatientId,
                        appointment.Location.Key,
                        appointment.AppointmentDate,
                        appointment.Purpose.Key,
                        appointment.VisitId.HasValue ? appointment.VisitId.Value : 0,
                        appointment.ServiceArea.Key);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        throw new Exception("An appointment exists ");
                    }

                    int theAffectedRows = 0;
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject obj = new ClsObject();
                    obj.Connection = this.Connection;
                    obj.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@AppointmentId", SqlDbType.Int, appointment.AppointmentId);
                    ClsUtility.AddExtendedParameters("@AppDate", SqlDbType.DateTime, appointment.AppointmentDate);
                    ClsUtility.AddExtendedParameters("@AppReasonId", SqlDbType.Int, appointment.Purpose.Key);
                    ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, appointment.BookedBy.Key);
                   
                    if (!appointment.Provider.Equals(default(KeyValuePair<int, string>)))
                    {

                        ClsUtility.AddExtendedParameters("@AppProviderId", SqlDbType.Int, appointment.Provider.Key);
                    }
                    ClsUtility.AddExtendedParameters("@Updationdate", SqlDbType.DateTime, appointment.StatusDate);
                    ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, appointment.ServiceArea.Key);
                    ClsUtility.AddParameters("@AppNote", SqlDbType.VarChar, appointment.Notes);
                    theAffectedRows = (int)obj.ReturnObject(ClsUtility.theParams, "pr_Scheduler_UpdatePatientAppointmentDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    return theAffectedRows;
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }

        /// <summary>
        /// Updates the patientppointment details.
        /// </summary>
        /// <param name="AppointmentId">The appointment identifier.</param>
        /// <param name="AppDate">The application date.</param>
        /// <param name="AppReasonId">The application reason identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="AppProviderId">The application provider identifier.</param>
        /// <param name="Updationdate">The updationdate.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="AppNote">The application note.</param>
        /// <returns></returns>
        public int UpdatePatientppointmentDetails(int AppointmentId, DateTime AppDate, int AppReasonId, int UserId, int AppProviderId, DateTime Updationdate, int? ModuleId = null, string AppNote = "")
        {
            lock (this)
            {
                try
                {
                    int theAffectedRows = 0;
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject obj = new ClsObject();
                    obj.Connection = this.Connection;
                    obj.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@AppointmentId", SqlDbType.Int, AppointmentId);
                    ClsUtility.AddExtendedParameters("@AppDate", SqlDbType.DateTime, AppDate);
                    ClsUtility.AddExtendedParameters("@AppReasonId", SqlDbType.Int, AppReasonId);
                    ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, UserId);
                    ClsUtility.AddExtendedParameters("@AppProviderId", SqlDbType.Int, AppProviderId);
                    ClsUtility.AddExtendedParameters("@Updationdate", SqlDbType.DateTime, Updationdate);
                    if (ModuleId.HasValue)
                    {
                        ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, ModuleId.Value);
                    }
                    if (AppNote != "")
                    {
                        ClsUtility.AddParameters("@AppNote", SqlDbType.VarChar, AppNote);
                    }
                    theAffectedRows = (int)obj.ReturnObject(ClsUtility.theParams, "pr_Scheduler_UpdatePatientAppointmentDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    return theAffectedRows;
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }


        public DataSet GetAppointmentGrid(int LocationId, int? PatientId = null, int? VisitId = null, int? AppStatus = null, DateTime? FromDate = null, DateTime? ToDate = null, int? AppointmentReason = null)
        {
            throw new NotImplementedException();
        }

      
    }
}