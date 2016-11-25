using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Clinical
{
    public class BPatientTransfer : ProcessBase, IPatientTransfer
    {
        //Constructor    
        public BPatientTransfer()
        {

        }
        //
        public DataSet GetLatestTransferDate(int PatientId, int VisitID)
        {
            lock (this)
            {
                ClsObject TransferDateMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, PatientId.ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.VarChar, VisitID.ToString());
                return (DataSet)TransferDateMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetTopTransferDate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataTable GetSatelliteID(string PatientId)
        {
            lock (this)
            {
                ClsObject TransferMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, PatientId);
                return (DataTable)TransferMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_PatientSatellite_LocationID", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetDataValidate(string PatientId, string transferdate)
        {
            lock (this)
            {
                ClsObject TransferMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, PatientId);
                ClsUtility.AddParameters("@Transferdate", SqlDbType.VarChar, transferdate);
                return (DataSet)TransferMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_PatientSatelliteDataValidate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDateValidateBetween(string PatientId, string Existingdate)
        {
            lock (this)
            {
                ClsObject TransferMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, PatientId);
                ClsUtility.AddParameters("@Transferdate", SqlDbType.VarChar, Existingdate);
                return (DataSet)TransferMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_PatientSatelliteDateBetween_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetSatelliteLocation(string PatientId, string TransferId, int Flag, string SystemID)
        {
            lock (this)
            {
                ClsObject TransferMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, PatientId);
                ClsUtility.AddParameters("@TransferId", SqlDbType.VarChar, TransferId);
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, Flag.ToString());
                ClsUtility.AddParameters("@SystemID", SqlDbType.VarChar, SystemID);
                ClsUtility.AddParameters("@password", SqlDbType.Int, ApplicationAccess.DBSecurity);
                return (DataSet)TransferMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_PatientSatelliteDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveUpdate(string ID, string PatientId, string TransferfromId, string TransfertoId, string TransfertoDate, int userId, string createdate, int flag)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SaveUpdateMgr = new ClsObject();
                SaveUpdateMgr.Connection = this.Connection;
                SaveUpdateMgr.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.VarChar, ID);
                ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, PatientId);
                //ClsUtility.AddParameters("@LocationId", SqlDbType.VarChar, LocationId);
                ClsUtility.AddParameters("@TransferfromId", SqlDbType.VarChar, TransferfromId);
                ClsUtility.AddParameters("@TransfertoId", SqlDbType.VarChar, TransfertoId);
                ClsUtility.AddParameters("@TransfertoDate", SqlDbType.VarChar, TransfertoDate);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                ClsUtility.AddParameters("@Createdate", SqlDbType.VarChar, createdate);
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, flag.ToString());
                int RowsAffected = (Int32)SaveUpdateMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_PatientSatelliteTransferSaveUpdate_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Reason record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
        }
    }


}
