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
    public class BCustomForm : ProcessBase, ICustomForm
    {
        #region "Constructor"
        public BCustomForm()
        {
        }
        #endregion

        public string GetSystemTime(int Format)
        {
            DataTable theTable = new DataTable();
            DataRow theDR = theTable.NewRow();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject MgrTime = new ClsObject();
                MgrTime.Connection = this.Connection;
                MgrTime.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                if (Format == 24)
                {
                    string theSQL = string.Format("Select CONVERT(Varchar(5),GetDate(),108)[TimeFormat]");
                    theDR = (DataRow)MgrTime.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataRow);
                }
                else if (Format == 12)
                {
                    string theSQL = string.Format("SELECT REPLACE(REPLACE(RIGHT('0'+LTRIM(RIGHT(CONVERT(varchar,getDate(),100),7)),7),'AM',' AM'),'PM',' PM')[TimeFormat]");
                    theDR = (DataRow)MgrTime.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataRow);
                }
                return (String)theDR[0];
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
        
        public DataSet Validate(string formName, string visitDate, int patientId, int moduleId)
        {
             

            DataSet theDS = new DataSet();
            try
            {
                //this.Connection = DataMgr.GetConnection();
                //this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject MgrValidate = new ClsObject();
                //MgrValidate.Connection = this.Connection;
                //MgrValidate.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, patientId.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, formName.ToString());
                ClsUtility.AddParameters("@VisitDate", SqlDbType.VarChar, visitDate.ToString());
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
                return theDS = (DataSet)MgrValidate.ReturnObject(ClsUtility.theParams, "pr_Clinical_ValidateCustomForm_Futures", ClsUtility.ObjectEnum.DataSet);
               
            }


            catch{
                  throw;
            }

            finally
            {
                //if (this.Connection != null)
                //    DataMgr.ReleaseConnection(this.Connection);
            }

        }

        public int DeleteForm(string FormName, int VisitID, int PatientId, int UserID)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteForm = new ClsObject();
                DeleteForm.Connection = this.Connection;
                DeleteForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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

        #region "FormNames"
        public DataSet GetFormName(int ModuleId, int CountryID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@CountryId", SqlDbType.Int, CountryID.ToString());
                ClsObject CustomFormMgr = new ClsObject();
                return (DataSet)CustomFormMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetCustomFormName_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "FieldNames with Labels"
        public DataSet GetFieldName_and_Label(int FeatureId, int PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                //ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FieldMgr = new ClsObject();
                if (FeatureId == 126)
                {
                    return (DataSet)FieldMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientRegistrationCustomFormFieldLabel_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                else
                {
                    return (DataSet)FieldMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetCustomFormFieldLabel_Constella", ClsUtility.ObjectEnum.DataSet);
                }
            }
        }
        #endregion

        public DataSet GetPmtctDecodeTable(string CodeID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@CodeID", SqlDbType.Int, CodeID.ToString());
                //ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                //ClsUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                //ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FieldMgr = new ClsObject();
                return (DataSet)FieldMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPmtctDeCode_Futures", ClsUtility.ObjectEnum.DataSet);
            }


        }

        public DataSet SaveUpdate(String Insert, DataSet DS, int TabId)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject CustomMgrSave = new ClsObject();
                CustomMgrSave.Connection = this.Connection;
                CustomMgrSave.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Insert", SqlDbType.VarChar, Insert.ToString());
                theDS = (DataSet)CustomMgrSave.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsUtility.ObjectEnum.DataSet);

                int LabID = Convert.ToInt32(theDS.Tables[2].Rows[0]["LabID"]);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@LabID", SqlDbType.Int, LabID.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, theDS.Tables[2].Rows[0]["LocationId"].ToString());
                    ClsUtility.AddParameters("@LabTestID", SqlDbType.Int, DS.Tables[0].Rows[i]["LabTestId"].ToString());
                    ClsUtility.AddParameters("@ParameterID", SqlDbType.Int, DS.Tables[0].Rows[i]["LabParameterId"].ToString());
                    ClsUtility.AddParameters("@TestResults", SqlDbType.Decimal, DS.Tables[0].Rows[i]["LabResult"].ToString());
                    ClsUtility.AddParameters("@TestResults1", SqlDbType.Decimal, DS.Tables[0].Rows[i]["LabResult1"].ToString());
                    ClsUtility.AddParameters("@Financed", SqlDbType.Int, DS.Tables[0].Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, theDS.Tables[2].Rows[0]["UserId"].ToString());
                    ClsUtility.AddParameters("@TabId", SqlDbType.Int, TabId.ToString());
                    ClsUtility.AddParameters("@Flag", SqlDbType.VarChar, "Lab");
                    int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomFormLabPharmacyRegimen_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                int PharmacyID = Convert.ToInt32(theDS.Tables[1].Rows[0]["PharmacyID"]);
                for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    ClsUtility.AddParameters("@Drug_Id", SqlDbType.Int, DS.Tables[1].Rows[i]["DrugId"].ToString());
                    ClsUtility.AddParameters("@GenericID", SqlDbType.Int, DS.Tables[1].Rows[i]["GenericId"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, DS.Tables[1].Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, DS.Tables[1].Rows[i]["FrequencyId"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, DS.Tables[1].Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, "0");
                    ClsUtility.AddParameters("@QtyPrescribed", SqlDbType.Decimal, DS.Tables[1].Rows[i]["QtyPrescribed"].ToString());
                    ClsUtility.AddParameters("@QtyDispensed", SqlDbType.Decimal, DS.Tables[1].Rows[i]["QtyDispensed"].ToString());
                    ClsUtility.AddParameters("@ARFinance", SqlDbType.Int, DS.Tables[1].Rows[i]["ARFinance"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, theDS.Tables[1].Rows[0]["UserID"].ToString());
                    ClsUtility.AddParameters("@TabId", SqlDbType.Int, TabId.ToString());
                    ClsUtility.AddParameters("@Flag", SqlDbType.VarChar, "ARVDrug");
                    int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomFormLabPharmacyRegimen_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                for (int i = 0; i < DS.Tables[2].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    ClsUtility.AddParameters("@Drug_Id", SqlDbType.Int, DS.Tables[2].Rows[i]["DrugId"].ToString());
                    ClsUtility.AddParameters("@GenericID", SqlDbType.Int, DS.Tables[2].Rows[i]["GenericId"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, DS.Tables[2].Rows[i]["UnitId"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, DS.Tables[2].Rows[i]["SingleDose"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, DS.Tables[2].Rows[i]["FrequencyID"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, DS.Tables[2].Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@QtyPrescribed", SqlDbType.Decimal, DS.Tables[2].Rows[i]["QtyOrdered"].ToString());
                    ClsUtility.AddParameters("@QtyDispensed", SqlDbType.Decimal, DS.Tables[2].Rows[i]["QtyDispensed"].ToString());
                    ClsUtility.AddParameters("@ARFinance", SqlDbType.Int, DS.Tables[2].Rows[i]["ARFinance"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, theDS.Tables[1].Rows[0]["UserID"].ToString());
                    ClsUtility.AddParameters("@TabId", SqlDbType.Int, TabId.ToString());
                    ClsUtility.AddParameters("@Flag", SqlDbType.VarChar, "NonARVDrug");
                    int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomFormLabPharmacyRegimen_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

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

            return theDS;
        
        }

        public DataSet Common_GetSaveUpdate(string Insert)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject CustomMgrSave = new ClsObject();
                CustomMgrSave.Connection = this.Connection;
                CustomMgrSave.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Insert", SqlDbType.VarChar, Insert.ToString());
                theDS = (DataSet)CustomMgrSave.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsUtility.ObjectEnum.DataSet);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDS; 

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
}
