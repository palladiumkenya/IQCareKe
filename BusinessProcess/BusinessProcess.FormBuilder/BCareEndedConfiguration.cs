using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using System.Collections;
using Application.Common;

namespace BusinessProcess.FormBuilder
{
    public class BCareEndedConfiguration : ProcessBase, ICareEndedConfiguration
    {
        public DataSet GetCareEndedDetails()
        {
            lock (this)
            {
                ClsObject CareEnd = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)CareEnd.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetCareEndedForm_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCareEndQuery(int ModuleId, int Published)
        {
            lock (this)
            {
                ClsObject QueryIndicater = new ClsObject();
                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@Published", SqlDbType.Int, Published.ToString());

                return (DataSet)QueryIndicater.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetCareEndedFormDetails_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetCareEndedInfo(int FeatureId)
        {
            lock (this)
            {
                ClsObject CareEnded = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                return (DataSet)CareEnded.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetCareEndedInfo_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataTable SaveNewPatientExitReason(string theExitReason, Int32 theUserId, int theSystemId)
        {
            lock (this)
            {
                ClsObject CareEnded = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ExitReason", SqlDbType.VarChar, theExitReason);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, theSystemId.ToString());
                return (DataTable)CareEnded.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveExitReason_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public int StatusUpdate(Hashtable ht)
        {
            int RowsEffected = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, ht["FeatureID"].ToString());
                ClsUtility.AddParameters("@Published", SqlDbType.Int, ht["Status"].ToString());

                RowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_Security_UpdateStatus_constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
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
            return RowsEffected;

        }
        public int SaveCareEndDetails(DataSet dsCareEndDetail, Int32 FeatureId, string FeatureName, String SectionName, Int32 ModuleId, Int32 UserId, String CountryId, Int32 SectionId, DataTable dtconditionalFields, DataTable theDeathReason)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                DataRow theDR;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, FeatureName);
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryId.ToString());
                theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveCareEndFeature_Futures", ClsUtility.ObjectEnum.DataRow);
                Int32 iFeatureId = System.Convert.ToInt32(theDR[0].ToString());

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SectionId", SqlDbType.Int, SectionId.ToString());
                ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, SectionName);
                ClsUtility.AddParameters("@Seq", SqlDbType.Int, "1");
                ClsUtility.AddParameters("@CustomFlag", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
             
                theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveMstSection_Futures", ClsUtility.ObjectEnum.DataRow);
                Int32 iSectionId = System.Convert.ToInt32(theDR[0].ToString());

                for (int i = 0; i <= dsCareEndDetail.Tables[1].Rows.Count - 1; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    ClsUtility.AddParameters("@ReasonsId", SqlDbType.VarChar, dsCareEndDetail.Tables[1].Rows[i]["ExitReasonsId"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                    if (i == 0)
                        ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, "1");
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SavelnkProgramExitReason_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                for (int i = 0; i <= dsCareEndDetail.Tables[0].Rows.Count-1; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iSectionId.ToString());
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsCareEndDetail.Tables[0].Rows[i]["FieldId"].ToString().Substring(1,dsCareEndDetail.Tables[0].Rows[i]["FieldId"].ToString().Length-1));
                    ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dsCareEndDetail.Tables[0].Rows[i]["Field Label"].ToString());
                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, (i+1).ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsCareEndDetail.Tables[0].Rows[i]["Predefine"].ToString());
                    if (i == 0)
                        ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, "1"); 

                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveCareEndLnkForms_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   
                }

                for (int i = 0; i <= dsCareEndDetail.Tables[2].Rows.Count - 1; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsCareEndDetail.Tables[2].Rows[i]["ExitReasonId"].ToString());
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsCareEndDetail.Tables[2].Rows[i]["FieldId"].ToString().Substring(1, dsCareEndDetail.Tables[2].Rows[i]["FieldId"].ToString().Length - 1));
                    ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dsCareEndDetail.Tables[2].Rows[i]["Field Label"].ToString());
                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, (i + 1).ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsCareEndDetail.Tables[2].Rows[i]["Predefine"].ToString());
                    if (i == 0)
                        ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, "1");

                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveLnkCareEndForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }

                /**************************Add CareEndConditional Fields*************************************/
                int Rec = 0;
                foreach (DataRow theDRCon in dtconditionalFields.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    Rec = Rec + 1;
                    //ClsUtility.AddParameters("@Id", SqlDbType.Int, dtconditionalFields.Rows[i]["id"].ToString());
                    //ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString());
                    
                    //ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Substring(1, theDRCon["ConfieldId"].ToString().Length - 1));
                    ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString());

                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, theDRCon["SectionId"].ToString());
                    //ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Substring(1, theDRCon["FieldId"].ToString().Length - 1));
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString());
                   
                    ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, theDRCon["FieldLabel"].ToString());
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, theDRCon["Predefined"].ToString());
                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, theDRCon["Seq"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, theDRCon["SectionName"].ToString());
                    //ClsUtility.AddParameters("@FitureId", SqlDbType.Int, theDRCon["FeatureId"].ToString());
                    ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, FeatureName.ToString());
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                    ClsUtility.AddParameters("@Conpredefine", SqlDbType.Int, theDRCon["Conpredefine"].ToString());
  
                    if (Rec == 1)
                        ClsUtility.AddParameters("@Delete", SqlDbType.Int, "1");
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_Savelnk_CareEndConditionalFields_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                
                /**************************Add DeathReason in Lnk_ModuleDeathreason*************************************/
                for (int i = 0; i <= theDeathReason.Rows.Count - 1; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                    ClsUtility.AddParameters("@DeathReasonsId", SqlDbType.VarChar, theDeathReason.Rows[i]["DeathReasonID"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                    if (i == 0)
                        ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, "1");
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SavelnkModuleDeathReason_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return iFeatureId;
            }
            catch (Exception err)
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw err;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
        }

        public DataSet GetCareEndedDeathReason(int ModuleID)
        {
            lock (this)
            {
                ClsObject CareEnded = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Moduleid", SqlDbType.Int, ModuleID.ToString());
                return (DataSet)CareEnded.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetDeathReason_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        
        }

        public DataTable SaveNewPatientDeathReason(string theDeathReason, Int32 theUserId, int theSystemId)
        {
            lock (this)
            {
                ClsObject CareEnded = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DeathReason", SqlDbType.VarChar, theDeathReason.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, theSystemId.ToString());
                return (DataTable)CareEnded.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveDeathReason_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetCareendConditionalFieldsDetails(Int32 ConfieldID, Int32 FeatureID)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ConfieldID", SqlDbType.Int, ConfieldID.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureID.ToString());
                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetCareendConditionalFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
    
    }
}

