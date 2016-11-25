using System;
using System.Data;
using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;


namespace BusinessProcess.FormBuilder
{
    public class BFieldDetails : ProcessBase, IFieldDetail
    {

        public DataSet GetDrugType()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "Pr_BusinessRule_GetDrugType_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetConditionalformInfo(int FeatureId)
        {
            lock (this)
            {
                ClsObject Conditionalform = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                return (DataSet)Conditionalform.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetConditionalformInfo_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetBusinessRule()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "Pr_BusinessRule_GetBusinessRule_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFields(string strFieldName, int iModuleId, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, strFieldName);
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, iModuleId.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                DataSet ds = (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetCustomFields_Futures", ClsUtility.ObjectEnum.DataSet);
                ds.Tables[2].Rows.Clear();
                foreach (DataRow row in ds.Tables[0].Select("LookupValues Is Not Null Or LookupValues <> ''"))
                {
                    ds.Tables[2].Rows.Add(new object[]
                    {
                      row["Id"],
                      row["FieldName"],
                      row["LookupValues"],
                      row["Predefine"],
                      row["CodeId"],
                      row["BindTable"],
                      row["ModuleId"]
                    })  ;
                }
                //ds.Tables[0].DefaultView.ToTable(true, "Id", "FieldName", "LookupValues", "Predefine", "CodeId","BindTable","ModuleId").Copy();
                ds.AcceptChanges();
                return ds;

            }
        }
        public DataSet GetCustomFields(string strFieldName, int iModuleId, int flag ,int IsGridView)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, strFieldName);
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, iModuleId.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                ClsUtility.AddParameters("@isGridView", SqlDbType.Int, IsGridView.ToString());
               // return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetCustomFields_Futures", ClsUtility.ObjectEnum.DataSet);
                DataSet ds = (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetCustomFields_Futures", ClsUtility.ObjectEnum.DataSet);
                ds.Tables[2].Rows.Clear();

                foreach (DataRow row in ds.Tables[0].Select("LookupValues Is Not Null Or LookupValues <> ''"))
                {
                    ds.Tables[2].Rows.Add(new object[]
                    {
                      row["Id"],
                      row["FieldName"],
                      row["LookupValues"],
                      row["Predefine"],
                      row["CodeId"],
                      row["BindTable"],
                      row["ModuleId"]
                    });
                }
                //ds.Tables[0].DefaultView.ToTable(true, "Id", "FieldName", "LookupValues", "Predefine", "CodeId","BindTable","ModuleId").Copy();
                ds.AcceptChanges();
                return ds;
            }
        }
        public DataSet GetDuplicateCustomFields(int id, string fieldName, int ModuleId,int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, id.ToString());
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, fieldName);
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetDuplicateCustomFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }


        public DataSet CheckPredefineField(int fieldID)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());

                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetPredefineFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet CheckCustomFields(int fieldID)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());

                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetCustomFieldsDetails_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int ResetCustomFieldRules(int fieldID, int flag, int predefine, string FieldName)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                int theRowAffected = 0;
                ClsUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                ClsUtility.AddParameters("@Predefined", SqlDbType.Int, predefine.ToString());
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, FieldName);
                theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveUpdateCustomFields_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                return theRowAffected;
            }
        }
        public int DeleteCustomField(int fieldID, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                int theRowAffected = 0;
                ClsUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_DeleteCustomFields_Future", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Deleting Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                return theRowAffected;
            }
        }
        public int SaveUpdateCusomtField(int ID, string FieldName, int ControlID, int DeleteFlag, int UserID, int CareEnd, int flag, string SelectList,
            DataTable business, int Predefined, int SystemID, DataTable dtconditionalFields, DataTable dtICD10Fields)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomField = new ClsObject();
                CustomField.Connection = this.Connection;
                CustomField.Transaction = this.Transaction;
                int theRowAffected = 0;
                DataRow theDR;



                /************   Delete Previous Business Rule **********/
                if (ID != 0 && flag != 4)
                {

                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                    theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_Delete_FieldBusinessRule_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Updating Custom Field. Try Again..";
                        AppException.Create("#C1", theMsg);
                    }

                }
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, FieldName);
                ClsUtility.AddParameters("@ControlID", SqlDbType.Int, ControlID.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@CareEnd", SqlDbType.Int, CareEnd.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                ClsUtility.AddParameters("@SelectList", SqlDbType.VarChar, SelectList);
                ClsUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                ClsUtility.AddParameters("@SystemID", SqlDbType.Int, SystemID.ToString());


                theDR = (DataRow)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveUpdateCustomFields_Futures", ClsUtility.ObjectEnum.DataRow);
                int FieldID = Convert.ToInt32(theDR[0].ToString());
                if (FieldID == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theBL);
                }

                /************************Add Business Rule*************************/
                for (int i = 0; i < business.Rows.Count; i++)
                {
                    if (FieldName == business.Rows[i]["FieldName"].ToString())
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@FieldID", SqlDbType.Int, FieldID.ToString());
                        ClsUtility.AddParameters("@BusRuleID", SqlDbType.Int, business.Rows[i]["BusRuleId"].ToString());
                        ClsUtility.AddParameters("@Value", SqlDbType.VarChar, business.Rows[i]["Value"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                        //12may2011
                        ClsUtility.AddParameters("@Value1", SqlDbType.VarChar, business.Rows[i]["Value1"].ToString());

                        theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveBusinessRules_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (theRowAffected == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                            AppException.Create("#C1", theMsg);

                        }
                    }
                }

                /**************************Add Conditional Fields*************************************/
                int Rec = 0;

                if (dtconditionalFields != null && dtconditionalFields.Rows.Count == 0)
                {
                    if (CareEnd == 0)
                    {
                        ClsUtility.Init_Hashtable();
                        string theTSQL = "";
                        if (Predefined==1){
                        theTSQL = "delete from dbo.lnk_conditionalfields where ConFieldId =" + ID.ToString().Replace("9999", "");
                        }
                        else if (Predefined==0){
                        theTSQL = "delete from dbo.lnk_conditionalfields where ConFieldId =" + ID.ToString().Replace("8888", "");
                        }
                        Int32 theRow = (Int32)CustomField.ReturnObject(ClsUtility.theParams, theTSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (CareEnd == 2)
                    {
                        ClsUtility.Init_Hashtable();
                        string theTSQL = "";
                        if (Predefined == 1)
                        {
                            theTSQL = "delete from dbo.lnk_PatientRegconditionalfields where ConFieldId =" + ID.ToString().Replace("9999", "");
                        }
                        else if (Predefined == 0)
                        {
                            theTSQL = "delete from dbo.lnk_PatientRegconditionalfields where ConFieldId =" + ID.ToString().Replace("8888", "");
                        }
                        Int32 theRow = (Int32)CustomField.ReturnObject(ClsUtility.theParams, theTSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else
                    {
                        ClsUtility.Init_Hashtable();

                        string theTSQL = "";
                        if (Predefined == 1)
                        {
                            theTSQL = "delete from dbo.lnk_CareEndConditionalFields where ConFieldId =" + ID.ToString().Replace("9999", "");
                        }
                        else if (Predefined == 0)
                        {
                            theTSQL = "delete from dbo.lnk_CareEndConditionalFields where ConFieldId =" + ID.ToString().Replace("8888", "");
                        }
                        //string theTSQL = "delete from dbo.lnk_CareEndConditionalFields where ConFieldId =" + ID.ToString();
                        Int32 theRow = (Int32)CustomField.ReturnObject(ClsUtility.theParams, theTSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }
                foreach (DataRow theDRCon in dtconditionalFields.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    Rec = Rec + 1;
                    if (theDRCon["ConPredefine"].ToString() == "1" && CareEnd == 0)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("9999", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "1" && CareEnd == 1)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("9999", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "1" && CareEnd == 2)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("9999", ""));

                    if (theDRCon["ConPredefine"].ToString() == "0" && CareEnd == 0)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("8888", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "0" && CareEnd == 1)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("8888", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "0" && CareEnd == 2)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("8888", ""));
                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, theDRCon["SectionId"].ToString());
                    if (CareEnd == 1)
                    {
                        //ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString());
                        if (theDRCon["Predefined"].ToString() == "1")
                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("9999", ""));
                        else
                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("8888", ""));
                    }
                    else
                    {
                        if (theDRCon["Predefined"].ToString() == "1")
                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("9999", ""));
                        else
                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("8888", ""));

                    }

                    ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, theDRCon["FieldLabel"].ToString());
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, theDRCon["Predefined"].ToString());
                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, Rec.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, theDRCon["SectionName"].ToString());
                    ClsUtility.AddParameters("@Conpredefine", SqlDbType.Int, theDRCon["Conpredefine"].ToString());
                    if (Rec == 1)
                        ClsUtility.AddParameters("@Delete", SqlDbType.Int, "1");
                    ClsUtility.AddParameters("@CareEnd", SqlDbType.Int, CareEnd.ToString());
                    theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SavelnkConditionalForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
              //  int Deleted = 0;
                foreach (DataRow theDRCon in dtICD10Fields.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, FieldID.ToString());
                    ClsUtility.AddParameters("@BlockId", SqlDbType.Int, theDRCon["BlockId"].ToString().Replace("'", ""));
                    ClsUtility.AddParameters("@SubBlockId", SqlDbType.Int, theDRCon["SubBlockId"].ToString().Replace("'", ""));
                    ClsUtility.AddParameters("@CodeId", SqlDbType.Int, theDRCon["CodeId"].ToString().Replace("'",""));
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theDRCon["Deleteflag"].ToString()); 
                    theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   // Deleted = 1;
                }




                /**************************************************************************************/

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return FieldID;
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

        public DataSet GetConditionalFieldslist(Int32 Codeid, int FID, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@CId", SqlDbType.Int, Codeid.ToString());
                ClsUtility.AddParameters("@FID", SqlDbType.Int, FID.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetConditionalFieldslist_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetConditionalFieldsDetails(Int32 ConfieldID, Int32 CareEndconFlag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ConfieldID", SqlDbType.Int, ConfieldID.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, CareEndconFlag.ToString());
                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetConditionalFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        

        public int SaveModDeCode(DataTable dtModDeCode)
        {
            lock (this)
            {
                ClsObject conditionalfields = new ClsObject();

                int theRowAffected = 0;

                for (int i = 0; i <= dtModDeCode.Rows.Count - 1; i++)
                {
                    ClsUtility.Init_Hashtable();
                    if (dtModDeCode.Rows[i]["FieldID"].ToString() != "0")
                    {
                        ClsUtility.AddParameters("@FieldID", SqlDbType.Int, dtModDeCode.Rows[i]["FieldID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, dtModDeCode.Rows[i]["CodeName"].ToString());
                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtModDeCode.Rows[i]["Predefined"].ToString());
                        ClsUtility.AddParameters("@Index", SqlDbType.Int, (i + 1).ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, dtModDeCode.Rows[i]["UserID"].ToString());
                        ClsUtility.AddParameters("@SystemID", SqlDbType.Int, dtModDeCode.Rows[i]["SystemID"].ToString());

                        theRowAffected = (int)conditionalfields.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveModDeCode_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                }

                //if (theRowAffected == 0)
                //{
                //    MsgBuilder theMsg = new MsgBuilder();
                //    theMsg.DataElements["MessageText"] = "Error in Saving ConditionalFields. Try Again..";
                //    AppException.Create("#C1", theMsg);

                //}

                return theRowAffected;
            }
        }
        #region "Treeview of ICD10 List"
        public DataSet GetICDList()
        {
            lock (this)
            {
                ClsObject ICD10Manager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)ICD10Manager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetICD10List_Features", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetICD10Values(int FieldId)
        {
            lock (this)
            {
                ClsObject ICD10Manager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, FieldId.ToString());
                return (DataSet)ICD10Manager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetICD10Values_Features", ClsUtility.ObjectEnum.DataSet);
            }
        }


        #endregion



        public DataTable GetFieldLookUpValues(int fieldId,  bool predefined,int systemId)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@FieldId", SqlDbType.Int, fieldId);
                ClsUtility.AddExtendedParameters("@Predefined", SqlDbType.Bit, predefined);
                ClsUtility.AddExtendedParameters("@SystemId", SqlDbType.Int, systemId);
                return (DataTable)CustomField.ReturnObject(ClsUtility.theParams, "FormBuilder_GetFieldLookupValues", ClsUtility.ObjectEnum.DataTable);
            }
        }


        public DataTable GetFieldControlTypes()
        {
            lock (this)      
            {
                 ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();                
                return (DataTable)CustomField.ReturnObject(ClsUtility.theParams, "FormBuilder_GetFieldControlTypes", ClsUtility.ObjectEnum.DataTable);
            }
        }
    }
}

