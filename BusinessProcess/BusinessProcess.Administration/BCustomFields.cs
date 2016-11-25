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
    public class BCustomFields : ProcessBase, ICustomFields
    {
        #region #Constructor
        public BCustomFields()
        {
        }
        #endregion

        public DataSet GetFeatures(int SystemId, string ModuleId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FeatureList = new ClsObject();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());

                return (DataSet)FeatureList.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectFeatureName_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomList(int CodeID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FeatureList = new ClsObject();
                ClsUtility.AddParameters("@CodeID", SqlDbType.Int, CodeID.ToString());
                return (DataSet)FeatureList.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectCodeDecode_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetCustomListName(string CodeName)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FeatureList = new ClsObject();
                ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, CodeName.ToString());
                return (DataSet)FeatureList.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectCodeName_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFields(int SystemId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)CustomFields.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectCustomFields_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFieldsUnits(int CustomFieldID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                ClsUtility.AddParameters("@CustomFieldID", SqlDbType.Int, CustomFieldID.ToString());
                return (DataSet)CustomFields.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectCustomFieldUnit_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFieldListforAForm(int FormID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                ClsUtility.AddParameters("@FormID", SqlDbType.Int, FormID.ToString());
                return (DataSet)CustomFields.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectCustomFieldList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientVisit(int ptnID,int locationID,int visitType)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                ClsUtility.AddParameters("@ptnpk", SqlDbType.Int, ptnID.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());
                ClsUtility.AddParameters("@VisitType", SqlDbType.Int, visitType.ToString());
                return (DataSet)CustomFields.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectVisitID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFieldValues(string TableName, string fields, Int32 ptnID, Int32 HomeVisitId, Int32 visitpk, Int32 labID, Int32 ptn_pharmacy_pk, Int32 FeatureID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName.ToString());
                ClsUtility.AddParameters("@Columns", SqlDbType.VarChar, fields.ToString());
                ClsUtility.AddParameters("@PtnID", SqlDbType.Int, ptnID.ToString());
                ClsUtility.AddParameters("@HomeVisitId", SqlDbType.Int, HomeVisitId.ToString());
                ClsUtility.AddParameters("@Visitpk", SqlDbType.Int, visitpk.ToString());
                ClsUtility.AddParameters("@LabID", SqlDbType.Int, labID.ToString());
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, ptn_pharmacy_pk.ToString());
                ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, FeatureID.ToString());
                return (DataSet)CustomFields.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Select", ClsUtility.ObjectEnum.DataSet);
            }
        }
        /// <summary>
        /// Saving the new Custom Fields Records into CustomFields Table 
        /// </summary>
        /// <param name="Label"></param>
        /// <param name="FeatureID"></param>
        /// <param name="SectionID"></param>
        /// <param name="ControlID"></param>
        /// <param name="UserID"></param>
        /// <param name="UnitFlag" for Numeric or Non Numeric Values></param>
        /// <param name="DataType" for Creat Table DataType></param>
        /// <returns></returns>
        ///  
       
        public DataSet GetDecodeValues(Int32 codeId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                ClsUtility.AddParameters("@codeId", SqlDbType.Int, codeId.ToString());
                return (DataSet)CustomFields.ReturnObject(ClsUtility.theParams, "pr_Admin_GeDecodeValuesCodeIdWise_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetVisit(int VisitId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                return (DataSet)CustomFields.ReturnObject(ClsUtility.theParams, "pr_Admin_VisitID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveCustomFields(string Lblfield, string lbldesc, int FeatureID, int SectionID, int ControlID, int UserID, int UnitFlag, int MinValue, int MaxValue, string UnitsNum, int CodeID, string DataType, string OldLabel, int multiSelect, int iSize, string decodeValues, string deleteValues, int SystemId,int rowcount)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Label", SqlDbType.VarChar, Lblfield);
                ClsUtility.AddParameters("@flddesc", SqlDbType.VarChar, lbldesc);
                ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, FeatureID.ToString() == "3" ? "4" : FeatureID.ToString());
                ClsUtility.AddParameters("@SectionID", SqlDbType.Int, SectionID.ToString());
                ClsUtility.AddParameters("@ControlID", SqlDbType.Int, ControlID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@UnitFlag", SqlDbType.Int, UnitFlag.ToString());
                ClsUtility.AddParameters("@Min", SqlDbType.Int, MinValue.ToString());
                ClsUtility.AddParameters("@Max", SqlDbType.Int, MaxValue.ToString());
                ClsUtility.AddParameters("@Units", SqlDbType.VarChar, UnitsNum);
                ClsUtility.AddParameters("@DataType", SqlDbType.VarChar, DataType.ToString());
                ClsUtility.AddParameters("@OldLabel", SqlDbType.VarChar, OldLabel.ToString());
                ClsUtility.AddParameters("@Size", SqlDbType.Int, iSize.ToString());
                ClsUtility.AddParameters("@decodeValues", SqlDbType.VarChar, decodeValues.ToString());
                ClsUtility.AddParameters("@deleteValues", SqlDbType.VarChar, deleteValues.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@rowcount", SqlDbType.Int, rowcount.ToString());


                int RowsAffected = (Int32)CustomFields.ReturnObject(ClsUtility.theParams, "pr_Admin_CreateCustomField_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //ClsUtility.Init_Hashtable();
                //RowsAffected = (Int32)CustomFields.ReturnObject(ClsUtility.theParams, "pr_CustomFieldResults_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                /*
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Fields record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                else
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Saved Custom Fields record...";
                    AppException.Create("#C1", theBL);
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
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
        /// <summary>
        /// Updaing the Existing Custom Fields Records into CustomFields Table 
        /// </summary>
        /// <param name="Label"></param>
        /// <param name="FeatureID"></param>
        /// <param name="SectionID"></param>
        /// <param name="ControlID"></param>
        /// <param name="UserID"></param>
        /// <param name="CustomFieldID"></param>
        /// <returns></returns>
        public int UpdateCustomFields(string Lblfield,string lbldesc, int FeatureID, int SectionID, int ControlID, int UserID, int CustomFieldID, int UnitFlag, int MinValue, int MaxValue, string UnitsNum)
        {


            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Label", SqlDbType.VarChar, Lblfield);
                ClsUtility.AddParameters("@flddesc", SqlDbType.VarChar, lbldesc);
                ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, FeatureID.ToString());
                ClsUtility.AddParameters("@SectionID", SqlDbType.Int, SectionID.ToString());
                ClsUtility.AddParameters("@ControlID", SqlDbType.Int, ControlID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@CustomFieldID", SqlDbType.Int, CustomFieldID.ToString());
                ClsUtility.AddParameters("@UnitFlag", SqlDbType.Int, UnitFlag.ToString());
                ClsUtility.AddParameters("@Min", SqlDbType.Int, MinValue.ToString());
                ClsUtility.AddParameters("@Max", SqlDbType.Int, MaxValue.ToString());
                ClsUtility.AddParameters("@Units", SqlDbType.VarChar, UnitsNum);


                int RowsAffected = (Int32)CustomFields.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateCustomField_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                /*
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Fields record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw new ApplicationException("There is an unspecified eror");
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int SaveCustomFieldValues(string sqlstr)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, sqlstr);

                int RowsAffected = (Int32)CustomFields.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                
                /*
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Fields record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                else
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Saved Custom Fields record...";
                    AppException.Create("#C1", theBL);
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw ;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            } 
            
        }
        public int DeleteCustomFields(int CustomFieldID,int DFlag)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();
                
                ClsUtility.AddParameters("@CustomFieldID", SqlDbType.Int, CustomFieldID.ToString());
                ClsUtility.AddParameters("@ActivateFlag", SqlDbType.Int, DFlag.ToString());
                int RowsAffected = (Int32)CustomFields.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteCustomField_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                /*
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Deleting Custom Fields record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                else
                {
                    
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
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

        public DataSet SaveCodeDecode(string Name, string DName,int SRNO, int UserID)
        {
            DataSet dsCustomList;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                ClsUtility.AddParameters("@DecodeName", SqlDbType.VarChar, DName);
                ClsUtility.AddParameters("@SRNO", SqlDbType.Int, SRNO.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());


                dsCustomList = (DataSet)CustomFields.ReturnObject(ClsUtility.theParams, "pr_Admin_AddCodeDecode_Constella", ClsUtility.ObjectEnum.DataSet);
                /*
                if ( dsCustomList==null && dsCustomList.Tables[0].Rows.Count  == 0 )
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom List record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                else
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Saved Custom List record...";
                    AppException.Create("#C1", theBL);
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dsCustomList;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw new ApplicationException("There is an unspecified eror"); 
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet GetRearrangeCustomFields(int SystemId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)CustomFields.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectRearrangeCustomFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int RearrangeCustomFields(DataTable dtCustomFields, int SystemId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;
                int RowsAffected=0;

                
                foreach (DataRow dr in dtCustomFields.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@CustomFieldID", SqlDbType.Int, dr["CustomFieldID"].ToString());
                    ClsUtility.AddParameters("@Srno", SqlDbType.Int, dr["SrNo"].ToString());
                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                    RowsAffected = (Int32)CustomFields.ReturnObject(ClsUtility.theParams, "Pr_Admin_RearrangeCustomField_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw new ApplicationException("There is an unspecified eror");
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int SaveUpdateCustomFieldValues(string[] sqlstr)
        {
            int RowsAffected = 0;
            try
            {
                int i;
                
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;

                for (i = 0; i < sqlstr.Length; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, sqlstr[i]);

                    RowsAffected += (Int32)CustomFields.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                /*
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Fields record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                else
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Saved Custom Fields record...";
                    AppException.Create("#C1", theBL);
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                RowsAffected = 0;
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
    

