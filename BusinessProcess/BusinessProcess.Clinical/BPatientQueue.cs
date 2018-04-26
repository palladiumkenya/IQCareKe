using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Context.Repository;
using DataAccess.Entity;
using Entities.Queue;
using Interface.Clinical;
using System;
using System.Data;

namespace BusinessProcess.Clinical
{
    public class BPatientQueue: ProcessBase,IPatientQueue
    {
        public BPatientQueue()
        {

        }
        public void ChangeQueueStatus(int patientQueueId, QueueStatus queueStatus, int UserId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@WaitingListId", SqlDbType.Int, patientQueueId);
                ClsUtility.AddExtendedParameters("@RowStatus", SqlDbType.Int, (int)queueStatus);
                ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int,UserId);
                ClsObject WaitingListManager = new ClsObject();
                WaitingListManager.ReturnObject(ClsUtility.theParams, "pr_WaitingListChangePatientStatus", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
        }

        public DataTable GetPatientPendingQueue(int patientId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
                return (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_WaitingList_GetPatientsWaitingList", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public WaitingQueue GetQueueById(int id)
        {
            throw new NotImplementedException();
        }

        public WaitingQueue GetQueueByName(string name)
        {
            QueueManagerRepository repo = new QueueManagerRepository();
            return repo.QueueByName(name);
        }

        public DataTable GetQueuedPatient(int queueId, int moduleId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@ListId", SqlDbType.Int, queueId);
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
                //ClsUtility.AddExtendedParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FieldMgr = new ClsObject();
                return (DataTable)FieldMgr.ReturnObject(ClsUtility.theParams, "pr_WaitingList_GetPatientsOnWaitingList", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Queues the patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="QueueId">The queue identifier.</param>
        /// <param name="queueStatus">The queue status.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public int QueuePatient(int patientId, int QueueId, QueueStatus queueStatus, QueuePriority priority, int ModuleId, int UserId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
                ClsUtility.AddExtendedParameters("@QueueId", SqlDbType.Int, QueueId);
                ClsUtility.AddExtendedParameters("@Priority", SqlDbType.Int, (int)priority);
                ClsUtility.AddExtendedParameters("@QueueStatus", SqlDbType.Int, queueStatus);
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, ModuleId);
                ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, UserId);
                ClsObject BillManager = new ClsObject();
               DataRow row = (DataRow) BillManager.ReturnObject(ClsUtility.theParams, "WaitingList_QueuePatient", ClsUtility.ObjectEnum.DataRow);
               if (null != row)
               {
                   return Convert.ToInt32(row[0]);
               }
                return -1;

            }
        }
    }
}
