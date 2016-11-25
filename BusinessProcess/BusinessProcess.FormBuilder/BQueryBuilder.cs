using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using System.Data.SqlClient;
using System.Collections.Specialized;



namespace BusinessProcess.FormBuilder
{
    public class BQueryBuilder:ProcessBase,IQueryBuilder
    {
        #region "Constructor"
        public BQueryBuilder()
        {
        }
        #endregion

        #region "UserMethods"
        public SqlConnection GetConnectionQueryBuilder()
        {
            lock (this)
            {
                Utility objUtil = new Utility();
           
                string constr = objUtil.Decrypt( ConfigurationManager.AppSettings.Get("ConnectionString"));
                    //((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
                constr += ";connect timeout=" + (ConfigurationManager.AppSettings.Get("SessionTimeOut"));
                    //(NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["SessionTimeOut"].ToString();
                constr += ";packet size=8192";
                SqlConnection connection = new SqlConnection(constr);
                connection.Open();
                return connection;
            }
        }

        public DataSet ReturnQueryResult(string theQuery)
        {
            lock (this)
            {
                string theFields = theQuery.Remove(0, 6);
                theFields = theFields.Substring(0, theQuery.IndexOf("From") - 6);
                theFields = theFields.Replace("\r", "");
                theFields = theFields.Replace("\n", "");

                string[] theSelStr = theFields.Split(',');
                for (int i = 0; i < theSelStr.Length; i++)
                {
                    if (theSelStr[i].Contains("FirstName") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[FirstName]";
                    else if (theSelStr[i].Contains("LastName") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[LastName]";
                    else if (theSelStr[i].Contains("Address") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[Address]";
                    else if (theSelStr[i].Contains("Phone") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[Phone]";
                    else if (theSelStr[i].Contains("MiddleName") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[MiddleName]";
                    else if (theSelStr[i].Contains("EmergContactName") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[EmergContactName]";
                    else if (theSelStr[i].Contains("GuardianName") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[GuardianName]";
                    else if (theSelStr[i].Contains("GuardianInformation") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[GuardianInformation]";
                    else if (theSelStr[i].Contains("EmergContactPhone") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[EmergContactPhone]";
                    else if (theSelStr[i].Contains("EmergContactAddress") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[EmergContactAddress]";
                    else if (theSelStr[i].Contains("TenCellLeader") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[TenCellLeader]";
                    else if (theSelStr[i].Contains("TenCellLeaderAddress") == true)
                        theSelStr[i] = "convert(varchar(200),decryptbykey(" + theSelStr[i] + "))[TenCellLeaderAddress]";
                }
                string theNewQry = "Select ";
                for (int i = 0; i < theSelStr.Length; i++)
                {
                    if (theNewQry.Length == 7)
                        theNewQry = theNewQry + theSelStr[i].ToString();
                    else
                        theNewQry = theNewQry + "," + theSelStr[i].ToString();
                }
                theNewQry = theNewQry + " " + theQuery.Substring(theQuery.IndexOf("From"), theQuery.Length - theQuery.IndexOf("From"));
                //theNewQry = theNewQry + theQuery.Substring(theQuery.IndexOf("From"), theQuery.Length - theQuery.IndexOf("From"));

                if (theQuery.Contains("FirstName") == true || theQuery.Contains("LastName") == true || theQuery.Contains("Address") == true
                    || theQuery.Contains("Phone") == true || theQuery.Contains("MiddleName") == true || theQuery.Contains("EmergContactName") == true
                    || theQuery.Contains("GuardianName") == true || theQuery.Contains("GuardianInformation") == true || theQuery.Contains("EmergContactPhone") == true
                    || theQuery.Contains("EmergContactAddress") == true || theQuery.Contains("TenCellLeader") == true || theQuery.Contains("TenCellLeaderAddress") == true)
                {
                    theNewQry = "Open symmetric key Key_CTC decryption by password=" + ApplicationAccess.DBSecurity.ToString() + " " + theNewQry;
                }

                ClsObject theQB = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theNewQry);
                return (DataSet)theQB.ReturnObject(ClsUtility.theParams, "pr_General_SQL_Parse", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable GetReportsCategory()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_GetReportsCategory_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetCustomReports(Int32 CategoryId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                ClsUtility.AddParameters("@CategoryId", SqlDbType.Int, CategoryId.ToString());
                return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_GetCustomReports_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }
        
        public DataTable SaveCustomReport(Int32 ReportId,Int32 CategoryId,string CategoryName,string ReportName, string ReportQuery,Int32 UserId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                ClsUtility.AddParameters("@CategoryId", SqlDbType.Int, CategoryId.ToString());
                ClsUtility.AddParameters("@CategoryName", SqlDbType.VarChar, CategoryName);
                ClsUtility.AddParameters("@ReportName", SqlDbType.VarChar, ReportName);
                ClsUtility.AddParameters("@ReportQuery", SqlDbType.VarChar, ReportQuery);
                ClsUtility.AddParameters("@UserId", SqlDbType.VarChar, UserId.ToString());
                return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_SaveCustomReports_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable SaveCustomReport(Int32 ReportId, Int32 CategoryId, string CategoryName, string ReportName, string ReportQuery, Int32 UserId, string queryParameter)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                ClsUtility.AddParameters("@CategoryId", SqlDbType.Int, CategoryId.ToString());
                ClsUtility.AddParameters("@CategoryName", SqlDbType.VarChar, CategoryName);
                ClsUtility.AddParameters("@ReportName", SqlDbType.VarChar, ReportName);
                ClsUtility.AddParameters("@ReportQuery", SqlDbType.VarChar, ReportQuery);
                ClsUtility.AddParameters("@UserId", SqlDbType.VarChar, UserId.ToString());
                ClsUtility.AddParameters("@ParamXML", SqlDbType.Xml, queryParameter);
                return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_SaveCustomReports_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataSet ExportReport(int ReportId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                return (DataSet)theQB.ReturnObject(ClsUtility.theParams, "pr_Reports_QueryBuilderExport_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        //QueryBuilder Report

        public int SaveUpdateQueryBuilderReport(DataSet dsReportDetails,Int32 theUserId)
        {
            lock (this)
            {
                ClsObject QueryBuilderReport = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@CategoryName", SqlDbType.VarChar, dsReportDetails.Tables[0].Rows[0]["CategoryName"].ToString());
                ClsUtility.AddParameters("@ReportName", SqlDbType.VarChar, dsReportDetails.Tables[1].Rows[0]["ReportName"].ToString());
                ClsUtility.AddParameters("@ReportQuery", SqlDbType.VarChar, dsReportDetails.Tables[1].Rows[0]["ReportQuery"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.VarChar, theUserId.ToString());
                int theRowAffected = (int)QueryBuilderReport.ReturnObject(ClsUtility.theParams, "pr_QueryBuilderReportSaveUpdate_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                return theRowAffected;
            }
        }

        #endregion
    }
}
