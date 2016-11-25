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
    public class BHomePageConfiguration : ProcessBase, IHomePageConfiguration
    {
        public DataSet GetHomePageIndicatorQuery(int ModuleId, int Published)
        {
            lock (this)
            {
                ClsObject QueryIndicater = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@Published", SqlDbType.Int, Published.ToString());
                return (DataSet)QueryIndicater.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetHomePageIndicatorQuery_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetTechnicalArea()
        {
            lock (this)
            {
                ClsObject TechArea = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)TechArea.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_GetMasters_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetIndicatorQueryResult(int FeatureId)
        {
            lock (this)
            {
                ClsObject SearchIndicator = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                return (DataSet)SearchIndicator.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetIndicatorQueryResult_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int DeleteIndicator(int ID, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                int theRowAffected = 0;
                ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());

                theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_DeleteIndicator_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Deleting Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                return theRowAffected;
            }
        }
        public String ParseSQLStatement(string sqlstr)
        {
            lock (this)
            {
                try
                {
                    ClsObject Query = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@QryString", SqlDbType.NVarChar, sqlstr);

                    DataTable dt1 = (DataTable)Query.ReturnObject(ClsUtility.theParams, "pr_General_SQL_Parse", ClsUtility.ObjectEnum.DataTable);

                    if (dt1.Rows.Count == 0)
                    {
                        return ("No Records");
                    }
                    else
                    {
                        return ("Valid SQL");
                    }
                }
                catch (SqlException sqlEx)
                {
                    return sqlEx.Message.ToString();
                }
                catch (Exception ex)
                {
                    //throw ex;
                    return ex.Message.ToString();
                }
            }
        }
        public String ParseSQLColoumns(string sqlstr)
        {
            lock (this)
            {
                try
                {

                    ClsObject Query = new ClsObject();

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@QryString", SqlDbType.NVarChar, sqlstr);

                    DataTable dt1 = (DataTable)Query.ReturnObject(ClsUtility.theParams, "pr_General_SQL_Parse", ClsUtility.ObjectEnum.DataTable);

                    if (dt1.Rows.Count == 0)
                    {
                        return ("No Records");
                    }
                    //else if (dt1.Rows.Count > 1 || dt1.Columns.Count > 1)
                    //{
                    //    return ("InValid Rows or Columns.Pelase use count function in Query");
                    //}
                    else
                    {
                        //DataRow row = dt1.Rows[0];
                        //object item = row.ItemArray[0];
                        if (dt1.Columns.Count < 4)
                        {
                            return ("Valid Value");
                        }
                        else
                        {
                            return ("InValid Value");
                        }


                    }

                }
                catch (SqlException sqlEx)
                {
                    return sqlEx.Message.ToString();
                }
                catch (Exception ex)
                {
                    //throw ex;
                    return ex.Message.ToString();
                }
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
        public int SaveHomePageIndicator(DataSet dsSaveIndicatorQuery, string Flag)
        {
            int iFeatureId;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                DataRow theDR;
                int iId;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsSaveIndicatorQuery.Tables[1].Rows[0]["FeatureId"].ToString());
                ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsSaveIndicatorQuery.Tables[1].Rows[0]["FeatureName"].ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveIndicatorQuery.Tables[1].Rows[0]["UserID"].ToString());
                ClsUtility.AddParameters("@Published", SqlDbType.Int, dsSaveIndicatorQuery.Tables[1].Rows[0]["Published"].ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsSaveIndicatorQuery.Tables[1].Rows[0]["ModuleId"].ToString());
                theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveFeature_Futures", ClsUtility.ObjectEnum.DataRow);
                iFeatureId = System.Convert.ToInt32(theDR[0].ToString());

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, dsSaveIndicatorQuery.Tables[2].Rows[0]["Id"].ToString());
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, dsSaveIndicatorQuery.Tables[2].Rows[0]["Name"].ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveIndicatorQuery.Tables[2].Rows[0]["UserId"].ToString());
                theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveHomePageIndicatorQuery_Futures", ClsUtility.ObjectEnum.DataRow);
                iId = System.Convert.ToInt32(theDR[0].ToString());
                for (int i = 0; i <= dsSaveIndicatorQuery.Tables[0].Rows.Count - 1; i++)
                {
                   ClsUtility.Init_Hashtable();

                   if (Flag == "U")
                    {
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, dsSaveIndicatorQuery.Tables[0].Rows[i]["Id"].ToString());
                    }
                    else
                    {
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, "0");
                    }
                        ClsUtility.AddParameters("@HomePageId", SqlDbType.Int, iId.ToString());
                        ClsUtility.AddParameters("@IndicatorName", SqlDbType.VarChar, dsSaveIndicatorQuery.Tables[0].Rows[i]["Indicator"].ToString());
                        ClsUtility.AddParameters("@Query", SqlDbType.VarChar, dsSaveIndicatorQuery.Tables[0].Rows[i]["Query"].ToString());
                        ClsUtility.AddParameters("@Seq", SqlDbType.Int, (i + 1).ToString());
                        ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, dsSaveIndicatorQuery.Tables[0].Rows[i]["UserId"].ToString());
                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SavedtlHomePageIndicatorQuery_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

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
            return iFeatureId;   
        }
    }


}
