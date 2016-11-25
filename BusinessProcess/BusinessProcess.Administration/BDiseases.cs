using System;
using System.Data;
using System.Data.SqlClient;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
namespace BusinessProcess.Administration
{
    public class BDiseases : ProcessBase,IDiseases 
    {
        #region "Constructor"
        public BDiseases()
        {
        }
        #endregion

        public DataSet GetDiseases()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DiseaseManager = new ClsObject();
                return (DataSet)DiseaseManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectDisease_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet DeleteDisease(int diseaseid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DiseaseManager = new ClsObject();
                ClsUtility.AddParameters("@Original_Disease_pk", SqlDbType.Int, diseaseid.ToString());
                return (DataSet)DiseaseManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteDisease_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDiseasesByID(int diseaseid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DiseaseManager = new ClsObject();
                ClsUtility.AddParameters("@diseaseid", SqlDbType.Int, diseaseid.ToString());

                return (DataSet)DiseaseManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectDiseasesByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewDisease(string DiseaseName, int UserID,int Sequence)
        {


            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DiseaseManager = new ClsObject();
                DiseaseManager.Connection = this.Connection;
                DiseaseManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DiseaseName", SqlDbType.VarChar, DiseaseName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
             //   ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());

                //DataRow theDR;
                int RowsAffected = (Int32)DiseaseManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_AddDisease_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Disease record. Try Again..";
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
        public int UpdateDisease(int Disease_Pk,string DiseaseName, int UserID,int DeleteFlag,int Sequence)
        {


            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DiseaseManager = new ClsObject();
                DiseaseManager.Connection = this.Connection;
                DiseaseManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DiseaseName", SqlDbType.VarChar, DiseaseName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@Disease_Pk", SqlDbType.Int, Disease_Pk.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());

              //  DataRow theDR;
                int RowsAffected  = (Int32)DiseaseManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateDisease_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Disease record. Try Again..";
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
