using System.Collections;
using Interface.Service;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
using System.Data;
using System;

namespace BusinessProcess.Service
{
    public class BMigration: ProcessBase,IMigration 
    {
        public BMigration()
        {
        }
        
        public string dbType(string DType,string dlength)
        {
            lock (this)
            {
                if (DType == "String" && dlength == "0")
                {
                    dlength = "8000";
                }
                if (dlength == "")
                {
                    dlength = "50";
                }

                switch (DType)
                {
                    case "String":
                        return "Varchar(" + dlength + ")";
                    case "Int32":
                        return "Numeric(18,0)";
                    case "Int16":
                        return "Int";
                    case "Double":
                        return "Numeric(18,0)";
                    case "Float":
                        return "Decimal(18,2)";
                    case "DateTime":
                        return "DateTime";
                    case "Boolean":
                        return "Bit";
                    case "Decimal":
                        return "Decimal(18,2)";
                    case "Int64":
                        return "Numeric(18,0)";
                    case "Single":
                        return "Decimal(18,2)";
                    case "Byte":
                        return "Decimal(18,0)";
                }
                return "";
            }
        }

        public string FString(string theInput)
        {
            lock (this)
            {
                string theRString = theInput.Replace("'", " ");
                if (theRString == "")
                {
                    return "null";
                }
                else
                {
                    return "'" + theRString + "'";
                }
            }
        }

        public int UpsizeData(DataTable theDT,DataTable tblDataTypes, string DBName,string TableName)
        {
                //////SqlConnection theCon = (SqlConnection)Entity.GetConnection(ConStr);
                //////SqlTransaction theTran = theCon.BeginTransaction();
          
            int i = 0;

                /////////////////////////////
                //StreamWriter theWriter = new StreamWriter("c:\\miglog.txt");
                //theWriter = File.AppendText("c:\\miglog.txt");
                ////////////////////////////

                try
                {
                    //////this.Connection = theCon;
                    //////this.Transaction = theTran;
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    //////Entity theObject = new Entity();
                    //////theObject.Connection = this.Connection;
                    //////theObject.Transaction = this.Transaction;
                    ClsObject objUpsize = new ClsObject();
                    objUpsize.Connection = this.Connection;
                    objUpsize.Transaction = this.Transaction;

                    /////// Table Creation//////
                    string ColumnList = "";
                    foreach (DataColumn theCol in theDT.Columns)
                    {
                        DataView theDV = new DataView(tblDataTypes);
                        theDV.RowFilter = "COLUMN_NAME ='" + theCol.ColumnName.ToString()+"'";

                        if (ColumnList == "")
                        {
                            ColumnList = "[" + theCol.ColumnName.ToString() + "]";
                            if (theDV[0]["CHARACTER_MAXIMUM_LENGTH"].ToString() != "")
                            {
                                ColumnList = ColumnList + " " + dbType(theCol.DataType.Name, theDV[0]["CHARACTER_MAXIMUM_LENGTH"].ToString());
                            }
                            else
                            {
                                ColumnList = ColumnList + " " + dbType(theCol.DataType.Name, theDV[0]["NUMERIC_PRECISION"].ToString());
                            }
                        }
                        else
                        {
                            ColumnList = ColumnList + "," + "[" + theCol.ColumnName.ToString() + "]";
                            if (theDV[0]["CHARACTER_MAXIMUM_LENGTH"].ToString() != "")
                            {
                                ColumnList = ColumnList + " " + dbType(theCol.DataType.Name, theDV[0]["CHARACTER_MAXIMUM_LENGTH"].ToString());
                            }
                            else
                            {
                                ColumnList = ColumnList + " " + dbType(theCol.DataType.Name, theDV[0]["NUMERIC_PRECISION"].ToString());
                            }
                        }
                    }
                    string cmd = "create table " + TableName + "( " + ColumnList + ")";
                    ClsUtility.Init_Hashtable();
                    int RowsAffected = (Int32)objUpsize.ReturnObject(ClsUtility.theParams, cmd, ClsUtility.ObjectEnum.ExecuteNonQuery);


                    ////////////////////////////////

                    ////////////////////////////////
                    /////Export Data/////////////
                    ColumnList = "";
                    cmd = "";
                    string theColumns = "";
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        cmd = "insert into " + TableName + " values(";
                        theColumns = "";
                        for (i = 0; i < theDT.Columns.Count; i++)
                        {
                            if (theColumns == "")
                            {
                                theColumns = FString(theDR[i].ToString());
                            }
                            else
                            {
                                theColumns = theColumns + "," + FString(theDR[i].ToString());
                            }
                        }
                        cmd = cmd + theColumns + ")";
                        ClsUtility.Init_Hashtable();
                        RowsAffected = (Int32)objUpsize.ReturnObject(ClsUtility.theParams, cmd, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //////theTran.Commit();
                    //////theCon.Close();
                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    return RowsAffected;

                }
                ////////////////////////////////
                //////catch (SqlException ex)
                catch 
                {
                    //theWriter.WriteLine(ex.Message.ToString()+"-"+ TableName + "-" + i.ToString());
                    //////theTran.Rollback();
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            
        }

        public int MigrateData(string cmd, int Location, string DBName, int CommandId)
        {
            //////SqlConnection theCon = (SqlConnection)Entity.GetConnection(ConStr);
            //////SqlTransaction theTran = theCon.BeginTransaction();
            try
            {
                //////this.Connection = theCon;
                //////this.Transaction = theTran;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject objMigration = new ClsObject();
                objMigration.Connection = this.Connection;
                objMigration.Transaction = this.Transaction;

                //////Entity theObject = new Entity();
                //////theObject.Connection = this.Connection;
                //////theObject.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@dbname", SqlDbType.VarChar, DBName);
                if (Location > 0)
                    ClsUtility.AddParameters("@Loc", SqlDbType.Int, Location.ToString());
                int RowsEffected = (int)objMigration.ReturnObject(ClsUtility.theParams, cmd, ClsUtility.ObjectEnum.ExecuteNonQuery);

                string thecmd = "update migration set complete = 1 where id = " + CommandId;
                ClsUtility.Init_Hashtable();
                int rowseffected = (int)objMigration.ReturnObject(ClsUtility.theParams, thecmd, ClsUtility.ObjectEnum.ExecuteNonQuery);
                //////theTran.Commit();
                //////theCon.Close();
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsEffected;
            }
            catch 
            {

                //theTran.Rollback();                
                DataMgr.RollBackTransation(this.Transaction);
                throw;
                //////throw ex;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet GetProcedures(string Constr,string DBName)
        {
            lock (this)
            {
                try
                {
                    //////Entity theObject = new Entity();
                    //////ClsUtility.Init_Hashtable();
                    //////ClsUtility.AddParameters("@dbname", SqlDbType.VarChar, DBName);
                    //////return (DataSet)theObject.ReturnObject(Constr, ClsUtility.theParams, "pr_GetProcedureList", ClsUtility.ObjectEnum.DataSet);

                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    ClsObject objProcList = new ClsObject();
                    objProcList.Connection = this.Connection;
                    objProcList.Transaction = this.Transaction;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@dbname", SqlDbType.VarChar, DBName);
                    return (DataSet)objProcList.ReturnObject(ClsUtility.theParams, "pr_GetProcedureList", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {

                    //theTran.Rollback();                
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                    //////throw ex;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
    }
}
