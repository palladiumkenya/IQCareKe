using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Administration
{
    public class BCustomList : ProcessBase, ICustomList
    {
        #region "Constructor"
        public BCustomList()
        {
        }
        #endregion
        public DataTable GetCustomListUpdateFlag(string TableName, int ID, int SystemId)
        {
            TableName = "mst_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetUpdateFlagCustomListMasters_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomListUpdatePriortorize(string TableName, int CategoryID, string SRno, int SystemId)
        {
            TableName = "mst_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@Category", SqlDbType.Int, CategoryID.ToString());
                ClsUtility.AddParameters("@SRNo", SqlDbType.VarChar, SRno.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetUpdatePriortorizeCustomListMasters_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetCustomList(string TableName, int Category, int SystemId)
        {
            TableName = "mst_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@CategoryType", SqlDbType.Int, Category.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetCustomListMasters_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomFieldList(Int32 SystemId, Int32 FacilityId)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetCustomFieldList_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomFieldListPMTCT()
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetCustomFieldListFormBuilder_Features", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomMasterLinkRecord(string TableName, Int32 CodeId)
        {
            TableName = "lnk_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@CodeId", SqlDbType.Int, CodeId.ToString());
                return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetCustomMasterLinkRecord_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomMasterNonSelectedRecord(string TableName)
        {
            TableName = "lnk_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetCustomMasterLinkSelectedRecord_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        //public DataTable GetAidsDefEvents(string TableName,string Name, string Code,int flag, int ID)
        //{
        //    TableName = "mst_" + TableName;
        //    ClsObject CustomManager = new ClsObject();
        //    ClsUtility.Init_Hashtable();
        //    ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
        //    ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name.ToString());
        //    ClsUtility.AddParameters("@Code", SqlDbType.VarChar, Code.ToString());
        //    ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
        //    ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());

        //    return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_ReturnOutput_Constella", ClsUtility.ObjectEnum.DataTable);
        //}

        //public DataTable GetDecode(string TableName, string Name,int flag, int ID)
        //{
        //    TableName = "mst_" + TableName;
        //    ClsObject CustomManager = new ClsObject();
        //    ClsUtility.Init_Hashtable();
        //    ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
        //    ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name.ToString());
        //    ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
        //    ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());

        //    return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_DecodeOutput_Constella", ClsUtility.ObjectEnum.DataTable);
        //}
        public int DeleteCustomMasterLinkRecord(string TableName, Int32 CodeId)
        {
            TableName = "lnk_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@CodeId", SqlDbType.Int, CodeId.ToString());
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_DeleteCustomMastersLink_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                return RowsAffected;
            }
        }
        public int DeleteCustomMasterLinkRecordParticular(string TableName, Int32 CodeId)
        {
            TableName = "lnk_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@CodeId", SqlDbType.Int, CodeId.ToString());
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_DeleteCustomMastersLinkParticular_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                return RowsAffected;
            }
        }
        public DataTable GetCustomMasterDetails(string TableName, int Id, int SystemId)
        {
            int n = TableName.IndexOf("mst_");
            if (n != -1)
            {

            }
            else
            {
                TableName = "mst_" + TableName;
            }
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@ID", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetCustomListMastersDetails_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }


        public DataTable SaveCustomMasterRecord(string TableName, string ListName, string Name, string Code, string Stage, int Sequence, int Category, int UserId, int SystemId, int CountryID, int ModuleId, string multiplier)
        {
            TableName = "mst_" + TableName;
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@ListName", SqlDbType.VarChar, ListName);
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                ClsUtility.AddParameters("@Code", SqlDbType.VarChar, Code);
                ClsUtility.AddParameters("@Stage", SqlDbType.VarChar, Stage);
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());
                ClsUtility.AddParameters("@Category", SqlDbType.Int, Category.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@CID", SqlDbType.Int, CountryID.ToString());
                ClsUtility.AddParameters("@ModuleID", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@multiplier", SqlDbType.Int, multiplier.ToString());
               
                //Int32 RowsAffected = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveCustomListMasters_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataTable RowsAffected = (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveCustomListMasters_Constella", ClsUtility.ObjectEnum.DataTable);
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int SaveUpdateCustomMasterLinkRecord(string TableName, int CodeId1, int CodeId2, int UserId)
        {
            TableName = "lnk_" + TableName;
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@CodeId1", SqlDbType.Int, CodeId1.ToString());
                ClsUtility.AddParameters("@CodeId2", SqlDbType.Int, CodeId2.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUpdateCustomMastersLink_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public DataTable GetVillageChairperson(int RegionID, int DistrictID, int WardID, int villageId)
        {

            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegionId", SqlDbType.Int, RegionID.ToString());
                ClsUtility.AddParameters("@DistrictID", SqlDbType.Int, DistrictID.ToString());
                ClsUtility.AddParameters("@WardID", SqlDbType.Int, WardID.ToString());
                ClsUtility.AddParameters("@VillageID", SqlDbType.Int, villageId.ToString());
                return (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetVillageChairperson_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataSet GetDistric(int RegionID, int SystemID)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegionID", SqlDbType.Int, RegionID.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetRegionDistric_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetWard(int DistricID, int SystemID)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DistricID", SqlDbType.Int, DistricID.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetDistricWard_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetVillage(int Ward, int SystemID)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ward", SqlDbType.Int, Ward.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetWardVillage_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #region "LPTF Patient Transfer"
        public DataSet GetLPTFPatientTransfer(int SystemID)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetLPTFPatientTransfer_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLPTFPatientTransferID(int LPTFId)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LPTFId", SqlDbType.Int, LPTFId.ToString());
                return (DataSet)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetLPTFPatientTransferID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable SaveLPTF(String LPTFID, String LPTFName, String Answer, String Status, String theUserID, String SystemID, String Flag)
        {
            DataTable Rowaffected = new DataTable();
            ClsObject CustomManager = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@LPTFID", SqlDbType.VarChar, LPTFID);
            ClsUtility.AddParameters("@LPTFName", SqlDbType.VarChar, LPTFName);
            ClsUtility.AddParameters("@Answer", SqlDbType.VarChar, Answer);
            ClsUtility.AddParameters("@Status", SqlDbType.VarChar, Status);
            ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, theUserID);
            ClsUtility.AddParameters("@SystemID", SqlDbType.VarChar, SystemID);
            ClsUtility.AddParameters("@Flag", SqlDbType.VarChar, Flag);

            if (Flag == "0")
            {
                Rowaffected = (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveLPTFPatientTransfer_Constella", ClsUtility.ObjectEnum.DataTable);
            }
            else if (Flag == "1")
            {
                Rowaffected = (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveLPTFPatientTransfer_Constella", ClsUtility.ObjectEnum.DataTable);
            }
            else if (Flag == "2")
            {
                Rowaffected = (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveLPTFPatientTransfer_Constella", ClsUtility.ObjectEnum.DataTable);
            }
            return Rowaffected;
        }
        #endregion

        public int SaveUpdateVillageChairperson(int RegionID, int DistrictID, int WardID, int VillageId, string CPersonName, int UserId, int flag)
        {
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegionId", SqlDbType.Int, RegionID.ToString());
                ClsUtility.AddParameters("@DistrictID", SqlDbType.Int, DistrictID.ToString());
                ClsUtility.AddParameters("@WardID", SqlDbType.Int, WardID.ToString());
                ClsUtility.AddParameters("@VillageId", SqlDbType.Int, VillageId.ToString());
                ClsUtility.AddParameters("@CPersonName", SqlDbType.Int, CPersonName.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@FlagID", SqlDbType.Int, flag.ToString());
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUpdateVillageChairperson_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int UpdateCustomMasterRecord(string TableName, int Id, string Name, string Code, string Stage, int Sequence, int Category, int Status, int UserId, int SystemId, int CountryID, int ModuleId, string multiplier)
        {
            TableName = "mst_" + TableName;
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                ClsUtility.AddParameters("@Code", SqlDbType.VarChar, Code);
                ClsUtility.AddParameters("@Stage", SqlDbType.VarChar, Stage);
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());
                ClsUtility.AddParameters("@Category", SqlDbType.Int, Category.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, Status.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@CID", SqlDbType.Int, CountryID.ToString());
                ClsUtility.AddParameters("@ModuleID", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@multiplier", SqlDbType.Int, multiplier.ToString());
               
                
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateCustomListMasters_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                CustomManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }

        #region "Treeview OI&Illness and Symtoms"
        public DataSet GetICDList()
        {
            lock (this)
            {
                ClsObject CustomManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)CustomManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetICD10List_Features", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable SaveICDCodeRecord(Hashtable theHT,  ArrayList theAL)
        {
            String TableName = "mst_" + theHT["TableName"].ToString();
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@ListName", SqlDbType.VarChar, theHT["ListName"].ToString());
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, theHT["Name"].ToString());
                ClsUtility.AddParameters("@Code", SqlDbType.VarChar, theHT["Code"].ToString());
                ClsUtility.AddParameters("@Stage", SqlDbType.VarChar, theHT["Stage"].ToString());
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, theHT["Sequence"].ToString());
                ClsUtility.AddParameters("@Category", SqlDbType.Int, theHT["Category"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, theHT["SystemId"].ToString());
                ClsUtility.AddParameters("@CID", SqlDbType.Int, theHT["CountryID"].ToString());
                DataTable RowsAffected = (DataTable)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveCustomListMasters_Constella", ClsUtility.ObjectEnum.DataTable);

                String DiseaseId = Convert.ToString(RowsAffected.Rows[0]["DiseaseId"]);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DiseaseId", SqlDbType.Int, DiseaseId);
                ClsUtility.AddParameters("@ICDCodeId", SqlDbType.VarChar, theHT["ICDCode"].ToString());
                if (Convert.ToInt32(theHT["Code"]) == 31)
                {
                    ClsUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "1");
                }
                else { ClsUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "0"); }
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                int RowsAffectedICDCode = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveCustomMastersLinkICDCodes_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                foreach(String AR in theAL)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@DiseaseId", SqlDbType.Int, DiseaseId);
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, AR);
                    ClsUtility.AddParameters("@ICDCodeId", SqlDbType.VarChar, theHT["ICDCode"].ToString());
                    if (Convert.ToInt32(theHT["Code"]) == 31)
                    {
                        ClsUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "1");
                    }
                    else { ClsUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "0"); }
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                    int RowsAffectedModuleICDCode = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveCustomMastersLinkModuleICDCodes_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int UpdateICDCodeRecord(string Id, Hashtable theHT,  ArrayList theAL)
         {
            String TableName = "mst_" + theHT["TableName"].ToString();
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, theHT["Name"].ToString());
                ClsUtility.AddParameters("@Code", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@Stage", SqlDbType.VarChar, theHT["Stage"].ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theHT["Status"].ToString());
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, theHT["Sequence"].ToString());
                ClsUtility.AddParameters("@Category", SqlDbType.Int, theHT["Category"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, theHT["SystemId"].ToString());
                ClsUtility.AddParameters("@CID", SqlDbType.Int, theHT["CountryID"].ToString());
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateCustomListMasters_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                
                String DiseaseId = Convert.ToString(Id);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DiseaseId", SqlDbType.Int, DiseaseId);
                ClsUtility.AddParameters("@ICDCodeId", SqlDbType.Int, theHT["ICDCode"].ToString());
                ClsUtility.AddParameters("@Validate", SqlDbType.Int, theHT["Validate"].ToString());
                if (Convert.ToInt32(theHT["Code"]) == 31)
                {
                    ClsUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "1");
                }
                else { ClsUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "0"); }
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                int RowsAffectedICDCode = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateCustomMastersLinkICDCodes_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                int CountFlag = 0;
                foreach (String AR in theAL)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@DiseaseId", SqlDbType.Int, DiseaseId);
                    ClsUtility.AddParameters("@CountFlag", SqlDbType.Int, CountFlag.ToString());
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, AR);
                    ClsUtility.AddParameters("@ICDCodeId", SqlDbType.Int, theHT["ICDCode"].ToString());
                    if (Convert.ToInt32(theHT["Code"]) == 31)
                    {
                        ClsUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "1");
                    }
                    else { ClsUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "0"); }
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                    int RowsAffectedModuleICDCode = (Int32)CustomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateCustomMastersLinkModuleICDCodes_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    CountFlag++;
                }
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffectedICDCode;
            }

            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                CustomManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }

        public DataSet GetICDData(int Id, int DiseaseFlag)
        {
            lock (this)
            {
                ClsObject CustomManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DiseaseId", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@DiseaseFlag", SqlDbType.VarChar, DiseaseFlag.ToString());
                return (DataSet)CustomManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetICD10ListData_Features", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion
    }
}