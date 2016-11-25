using System;
using System.Data;
using System.Data.SqlClient;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using System.Text;
namespace BusinessProcess.Administration
{
    public class BDrugMst : ProcessBase,IDrugMst 
    {
        #region "Constructor"
        public BDrugMst()
        {
        }
        #endregion

        public DataTable GetExistDrug(string DrugName)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                ClsUtility.AddParameters("@DrugName", SqlDbType.VarChar, DrugName);
                return (DataTable)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_ExistDrug_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
       
        /*******************Retriev Detail for Existing Drug **************/

        public DataSet GetExistDrugDetail(int Drug_pk, string DrugType, string Generic)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                ClsUtility.AddParameters("@Drug_pk", SqlDbType.Int, Drug_pk.ToString());
                ClsUtility.AddParameters("@Generic", SqlDbType.VarChar, Generic);
                ClsUtility.AddParameters("@DrugType", SqlDbType.VarChar, DrugType);

                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectDrugForID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
         
        public DataSet GetDrug()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectDrug_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDrugMst()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectDrugMst_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetGenericDrug(int DrugId, int GenericId, int DrugTypeId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                ClsUtility.AddParameters("@Drug_pk", SqlDbType.Int, DrugId.ToString());
                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, GenericId.ToString());
                ClsUtility.AddParameters("@DrugTypeId", SqlDbType.Int, DrugTypeId.ToString());
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectExistGenericDrug_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet DeleteDrug(int Drug_ID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                ClsUtility.AddParameters("@Original_Drug_pk", SqlDbType.Int, Drug_ID.ToString());
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteDrug_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        
        public DataTable GetGeneric(int DrugTypeID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                ClsUtility.AddParameters("@DrugTypeId", SqlDbType.Int, DrugTypeID.ToString());
                return (DataTable)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectGeneric_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetGenericByID(int GenericId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, GenericId.ToString());
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectGenericByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable GetExistGeneric(string DrugType)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                ClsUtility.AddParameters("@DrugType", SqlDbType.VarChar, DrugType);
                return (DataTable)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectExistGeneric_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetDrugTypes()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectDrugType_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetDrugStrength()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectStrength_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        
        public DataSet GetFrequencies()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectFrequency_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetAllDropDowns()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetDrugDropDowns_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetStrengthLists(int DrugId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DrugId", SqlDbType.Int, DrugId.ToString());
                ClsObject DrugManager = new ClsObject();
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetDrugStrengthFrequency_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
     
        public int SaveUpdateDrugDetails(int Drug_Pk, string DrugName, string DrugAbbreviation,int Status, DataTable ExistGeneric, DataTable Generics, decimal MaxDose, decimal MinDose, int DrugTypeID, int UserID, DataTable Strength, DataTable Frequency, DataTable Schedule,int Update)
        { 
            int DrugID = 0;
           // int theReturnValue = 0;
            int theAffectedRows = 0;
            
            DataTable theReturnDT = new DataTable();
            DataTable theExistRow = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DrugManager = new ClsObject();
                DrugManager.Connection = this.Connection;
                DrugManager.Transaction = this.Transaction;

                #region "Delete Exist Records"

                if (ExistGeneric.Rows.Count != 0)
                {
                    /****************Delete Exist ARV Records *******************/

                    if (DrugTypeID == 37)
                    {
                        int AffectedRows = 0;
                        foreach (DataRow dr in ExistGeneric.Rows)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@Drug_pk", SqlDbType.Int, Drug_Pk.ToString());

                            if (Drug_Pk == 0)
                            {
                                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, dr[0].ToString());
                                //ClsUtility.AddParameters("@GenericId", SqlDbType.Int, dr[1].ToString());
                                AffectedRows = (int)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteARVDruglnkDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                            }
                            else
                            {
                                int GenericId = 0;
                                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, GenericId.ToString());
                                AffectedRows = (int)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteARVDruglnkDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                break; // since all rows are deleted at once from lnk_drugGeneric
                            }
                        }
                    }
                    else
                    {
                        /************ Delete NonARV Details *****************/
                        int AffectedRows = 0;
                        foreach (DataRow dr in ExistGeneric.Rows)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@Drug_pk", SqlDbType.Int, Drug_Pk.ToString());

                            if (Drug_Pk == 0)
                            {
                                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, dr[0].ToString());
                                //ClsUtility.AddParameters("@GenericId", SqlDbType.Int, dr[1].ToString());
                                AffectedRows = (int)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteNonARVDrugDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                            }
                            else
                            {
                                int GenericId = 0;
                                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, GenericId.ToString());
                                AffectedRows = (int)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteNonARVDrugGenericlnkDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            }
                        }
                    }
                }
                #endregion

                #region "Save Trade Name and Map with Generic"
                   if (DrugName != "")
                    {
                        #region "Strength & Frequency Mapping"
                        int GenericID = 0;
                        int RowsAffected = 0;

                        if (DrugID != 0 && DrugTypeID == 37)
                        {
                            GenericID = 0;
                        }
                        else if (DrugID == 0 && DrugTypeID == 37 && DrugName == "" && Drug_Pk == 0)
                        {

                            GenericID = Convert.ToInt32(Generics.Rows[0]["Id"].ToString());
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@GenericID", SqlDbType.Int, GenericID.ToString());
                            //ClsUtility.AddParameters("@Drug_pk", SqlDbType.Int, Drug_Pk.ToString());
                            theExistRow = (DataTable)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_CheckLinkDrugStrength_Constella", ClsUtility.ObjectEnum.DataTable);
                        }


                        if (DrugName != "" && DrugID == 0)
                        {
                            theAffectedRows = 0;
                        }
                        else if (DrugID == 0 && theExistRow.Rows.Count != 0)
                        {
                            theAffectedRows = 2;
                        }

                        else
                        {
                            // if (DrugID == 0) //rupesh
                            // {
                            GenericID = Convert.ToInt32(Generics.Rows[0]["Id"].ToString());
                            //  }

                            //-------------

                        }
                        //-----------
                        if (DrugName != "" && DrugID == 0)
                        {
                            theAffectedRows = 0;
                        }
                        else if (DrugID == 0 && theExistRow.Rows.Count != 0)
                        {
                            theAffectedRows = 2;
                        }
                        else
                        {
                            if (DrugTypeID != 37)
                            {
                                /**************Save NONARV Details *************/

                                if (DrugID == 0 && Drug_Pk == 0)
                                {
                                    GenericID = Convert.ToInt32(Generics.Rows[0]["Id"].ToString());
                                    ClsUtility.Init_Hashtable();
                                    ClsUtility.AddParameters("@GenericID", SqlDbType.Int, GenericID.ToString());
                                    // ClsUtility.AddParameters("@Drug_pk", SqlDbType.Int, Drug_Pk.ToString());
                                    theExistRow = (DataTable)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_CheckNonARVDrug_Constella", ClsUtility.ObjectEnum.DataTable);
                                }
                                if (theExistRow.Rows.Count != 0)
                                {
                                    theAffectedRows = 2;
                                }
                                else
                                {
                                    foreach (DataRow dr in Generics.Rows)
                                    {
                                        GenericID = Convert.ToInt32(dr["Id"].ToString()); // rupesh 16-07-07
                                        ClsUtility.Init_Hashtable();
                                        ClsUtility.AddParameters("@Drug_pk", SqlDbType.Int, DrugID.ToString());
                                        ClsUtility.AddParameters("@GenericId", SqlDbType.Int, GenericID.ToString());
                                        ClsUtility.AddParameters("@MaxDose", SqlDbType.Decimal, MaxDose.ToString());
                                        ClsUtility.AddParameters("@MinDose", SqlDbType.Decimal, MinDose.ToString());
                                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                                        theAffectedRows = (int)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_InsertNonARVDrugDoseDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        if (theAffectedRows == 0)
                                        {
                                            //MsgBuilder theMsg = new MsgBuilder();
                                            //theMsg.DataElements["MessageText"] = "Error in Saving Non-ARV Drug Details. Try Again..";
                                            //AppException.Create("#C1", theMsg);
                                            throw new Exception("Error in Saving Non-ARV Drug Details. Try Again..");
                                        }
                                    }
                                }
                            }
                        }
                        

                            /////////////////////////////////////////////////////
                            /************** Saving Strengths ***************/
                            if (Strength.Rows.Count != 0)
                            {
                                foreach (DataRow dr in Strength.Rows)
                                {
                                    StringBuilder strgenname = new StringBuilder();
                                    foreach (DataRow drgenericname in Generics.Rows)
                                    {
                                        string genName = "";
                                        genName = drgenericname["Name"].ToString();
                                        //if (DrugTypeID == 37)
                                        //{
                                            if (genName.IndexOf("-[") > -1)
                                            {
                                                genName = genName.Substring(0, genName.IndexOf("-["));

                                            }
                                            genName = "/" + genName.ToString();
                                            //genName = genName.Substring(0, genName.Length - 1);
                                        //}
                                        strgenname.Append(genName.ToString());

                                    }
                                    //if (DrugTypeID == 37)
                                    //{
                                        strgenname.Remove(0, 1);
                                    //}

                                    ///////////////////////////////////////////////


                                    ClsUtility.Init_Hashtable();
                                    ClsUtility.AddParameters("@DrugId", SqlDbType.Int, Drug_Pk.ToString());
                                    ClsUtility.AddParameters("@DrugName", SqlDbType.VarChar, strgenname.ToString() + "-" + DrugName + " " + dr["Name"].ToString());
                                    ClsUtility.AddParameters("@DrugAbbreviation", SqlDbType.VarChar, DrugAbbreviation);
                                    ClsUtility.AddParameters("@DrugTypeID", SqlDbType.Int, DrugTypeID.ToString());
                                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                                    ClsUtility.AddParameters("@Status", SqlDbType.Int, Status.ToString());
                                    ClsUtility.AddParameters("@Update", SqlDbType.Int, Update.ToString());
                                    theReturnDT = (DataTable)DrugManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertDrug_Constella", ClsUtility.ObjectEnum.DataTable);

                                    if (theReturnDT.Rows[0][0].ToString() == "0")
                                    {
                                       // MsgBuilder theMsg = new MsgBuilder();
                                       // theMsg.DataElements["MessageText"] = "Drug Already Exists. Try Again..";
                                       // Exception ex = AppException.Create("#C1", theMsg);
                                        Exception ex = new Exception("Drug Already Exists. Try Again..");
                                        throw ex;
                                    }


                                    ///////////////////////////////////////////////

                                    foreach (DataRow drgeneric in Generics.Rows)
                                    {
                                                                              
                                        if (DrugName != "" && theReturnDT.Rows[0][0].ToString() != "0")
                                        {
                                            DrugID = Convert.ToInt32(theReturnDT.Rows[0][0].ToString());
                                        }
                                        else
                                            DrugID = 0;

                                        int theGeneric = 0;
                                        if (DrugName == "")
                                        {
                                            theGeneric = Convert.ToInt32(drgeneric["Id"].ToString());
                                        }
                                        int DeleteFlag = Convert.ToInt32(theReturnDT.Rows[0]["DeleteFlag"]);
                                        ClsUtility.Init_Hashtable();
                                        ClsUtility.AddParameters("@DrugId", SqlDbType.Int, DrugID.ToString());
                                        ClsUtility.AddParameters("@StrengthId", SqlDbType.Int, dr[0].ToString());
                                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                                        ClsUtility.AddParameters("@GenericID", SqlDbType.Int, theGeneric.ToString());
                                        RowsAffected = (Int32)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_LinkDrugStrength_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                                        if (RowsAffected == 0)
                                        {
                                            //MsgBuilder theMsg = new MsgBuilder();
                                            //theMsg.DataElements["MessageText"] = "Error in Saving Strength for Drug. Try Again..";
                                            //AppException.Create("#C1", theMsg);
                                            throw new Exception("Error in Saving Strength for Drug. Try Again..");
                                        }


                                        ClsUtility.Init_Hashtable();
                                        ClsUtility.AddParameters("@Drug_pk", SqlDbType.Int, DrugID.ToString());
                                        ClsUtility.AddParameters("@GenericId", SqlDbType.Int, drgeneric["Id"].ToString());
                                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                                        ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());

                                        theAffectedRows = (int)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_InsertDrugGenericDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                                        if (theAffectedRows == 0)
                                        {
                                            //MsgBuilder theMsg = new MsgBuilder();
                                            //theMsg.DataElements["MessageText"] = "Error in Saving Drug Generic Combinations. Try Again..";
                                            //AppException.Create("#C1", theMsg);
                                            throw new Exception("Error in Saving Drug Generic Combinations. Try Again..");
                                        }
                                 
                                    }


                        #endregion
                                    #region "Drug Scheduling Mapping"
                                    if (DrugName != "" && DrugTypeID == 60)
                                    {
                                        if (Schedule.Rows.Count != 0)
                                        {
                                            /************** Saving Frequency ***************/
                                            foreach (DataRow drSchedule in Schedule.Rows)
                                            {
                                                ClsUtility.Init_Hashtable();
                                                ClsUtility.AddParameters("@DrugId", SqlDbType.Int, DrugID.ToString());
                                                ClsUtility.AddParameters("@ScheduleId", SqlDbType.Int, drSchedule[0].ToString());
                                                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                                                theAffectedRows = (Int32)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_LinkDrugSchedule_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                                                if (theAffectedRows == 0)
                                                {
                                                    
                                                    throw new Exception("Error in Saving Schedule for Drug. Try Again..");
                                                }

                                            }

                                        }
                                    }
                                    #endregion

                                }
                            }//////
                        
                        
                    }
                
                #endregion

                ////////////////////////////////////////////////////
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return (theAffectedRows);
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

                
              
                
            
            
            
        
        

        public DataTable CreateStrength(string StrengthName,int UserId)
        {
            lock (this)
            {
                ClsObject StrengthManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@StrengthName", SqlDbType.VarChar, StrengthName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return (DataTable)StrengthManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveDrugStrength_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable CreateFrequency(string FrequencyName, int UserId)
        {
            lock (this)
            {
                ClsObject FrequencyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FrequencyName", SqlDbType.VarChar, FrequencyName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return (DataTable)FrequencyManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveDrugFrequency_Constella", ClsUtility.ObjectEnum.DataTable);
            }

        }
        public DataTable CreateSchedule(string ScheduleName, int UserId)
        {
            lock (this)
            {
                ClsObject ScheduleManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ScheduleName", SqlDbType.VarChar, ScheduleName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return (DataTable)ScheduleManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveDrugSchedule_Futures", ClsUtility.ObjectEnum.DataTable);
            }

        }
        public DataTable GetStrengthByGenericID(int GenericId)
        {
            lock (this)
            {
                ClsObject DrugStrengthManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, GenericId.ToString());
                return (DataTable)DrugStrengthManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetStrengthByGenericID_Constella", ClsUtility.ObjectEnum.DataTable);
            }

        }

        public DataTable GetFrequencyByGenericID(int GenericId)
        {
            lock (this)
            {
                ClsObject DrugStrengthManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, GenericId.ToString());
                return (DataTable)DrugStrengthManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetFrequencyByGenericID_Constella", ClsUtility.ObjectEnum.DataTable);
            }

        }
        public DataTable GetScheduleByDrugID(int DrugId)
        {
            lock (this)
            {
                ClsObject DrugStrengthManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DrugId", SqlDbType.Int, DrugId.ToString());
                return (DataTable)DrugStrengthManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetScheduleByDrugID_Futures", ClsUtility.ObjectEnum.DataTable);
            }

        }
        public DataTable GetDrugsForGenericID(int GenericId)
        {
            lock (this)
            {
                ClsObject DrugGenericManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, GenericId.ToString());
                return (DataTable)DrugGenericManager.ReturnObject(ClsUtility.theParams, "pr_admin_GetDrugsForGenericID", ClsUtility.ObjectEnum.DataTable);
            }

        }
        public int InActivateGeneric(int GenericId)
        {
            lock (this)
            {
                ClsObject DrugGenericManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, GenericId.ToString());
                return (int)DrugGenericManager.ReturnObject(ClsUtility.theParams, "pr_Admin_InactivateGeneric", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }

        }
        public int ActivateGeneric(int GenericId)
        {
            lock (this)
            {
                ClsObject DrugGenericManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@GenericId", SqlDbType.Int, GenericId.ToString());
                return (int)DrugGenericManager.ReturnObject(ClsUtility.theParams, "pr_Admin_ActivateGeneric", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }

        }
        public DataSet CreateGeneric(string GenericName, string GenericAbbrivation, int DrugTypeId, int UserId)
        {
            lock (this)
            {
                ClsObject GenericManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@GenericName", SqlDbType.VarChar, GenericName);
                ClsUtility.AddParameters("@GenericAbbv", SqlDbType.VarChar, GenericAbbrivation);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DrugTypeId", SqlDbType.Int, DrugTypeId.ToString());

                return (DataSet)GenericManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveDrugGeneric_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int SaveUpdateRegimenGeneric(string RegimenName,string RegimenCode,int RegimenID, string Stage, int Status, string GenericID, int UserID,int SRNo,int flag)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DrugManager = new ClsObject();
                DrugManager.Connection = this.Connection;
                DrugManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegimenName", SqlDbType.VarChar, RegimenName);
                ClsUtility.AddParameters("@RegimenCode", SqlDbType.VarChar, RegimenCode);
                ClsUtility.AddParameters("@Rid", SqlDbType.Int, RegimenID.ToString());
                ClsUtility.AddParameters("@Stage", SqlDbType.VarChar, Stage);
                ClsUtility.AddParameters("@Status", SqlDbType.Int, Status.ToString());
                ClsUtility.AddParameters("@GenericID", SqlDbType.VarChar, GenericID);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@SRNo", SqlDbType.Int, SRNo.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());

                Int32 RowsAffected = (Int32)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveRegimenGeneric_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Regimen Generic Combinations. Try Again..";
                    Exception ex = AppException.Create("#C1", theBL);
                    throw ex;
                }
                DrugManager = null;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
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
        public int SaveUpdateTBRegimenGeneric(string RegimenName,int RegimenID, int TreatmentTime, int Status, string GenericID, int UserID, int SRNo, int flag)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DrugManager = new ClsObject();
                DrugManager.Connection = this.Connection;
                DrugManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegimenName", SqlDbType.VarChar, RegimenName);
                ClsUtility.AddParameters("@Rid", SqlDbType.Int, RegimenID.ToString());
                ClsUtility.AddParameters("@TreatmentTime", SqlDbType.Int, TreatmentTime.ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.Int, Status.ToString());
                ClsUtility.AddParameters("@GenericID", SqlDbType.VarChar, GenericID);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@SRNo", SqlDbType.Int, SRNo.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());

                Int32 RowsAffected = (Int32)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveTBRegimenGeneric_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving TB Regimen Generic Combinations. Try Again..";
                    Exception ex = AppException.Create("#C1", theBL);
                    throw ex;
                }
                DrugManager = null;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
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
        public DataSet GetRegimenGeneric(int RegimenID)
        {
            lock (this)
            {
                ClsObject GenericManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, RegimenID.ToString());
                return (DataSet)GenericManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetRegimenGeneric_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetTBRegimenGeneric(int RegimenID)
        {
            lock (this)
            {
                ClsObject GenericManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, RegimenID.ToString());
                return (DataSet)GenericManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetTBRegimenGeneric_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetRegimenName(string RegimenName)
        {
            lock (this)
            {
                ClsObject GenericManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegimenName", SqlDbType.VarChar, RegimenName.ToString());
                return (DataSet)GenericManager.ReturnObject(ClsUtility.theParams, "pr_Admin_CheckRegimenGeneric_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetTBRegimenName(string RegimenName)
        {
            lock (this)
            {
                ClsObject GenericManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegimenName", SqlDbType.VarChar, RegimenName.ToString());
                return (DataSet)GenericManager.ReturnObject(ClsUtility.theParams, "pr_Admin_CheckTBRegimenGeneric_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetAllRegimenGeneric()
        {
            lock (this)
            {
                ClsObject GenericManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)GenericManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetAllRegimenGeneric_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetAllTBRegimenGeneric()
        {
            lock (this)
            {
                ClsObject GenericManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)GenericManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetTBAllRegimenGeneric_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
    }
}
