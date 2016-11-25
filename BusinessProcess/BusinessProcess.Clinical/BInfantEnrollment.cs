using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
//using DataAccess.Common;
namespace BusinessProcess.Clinical
{
  public  class BInfantEnrollment : ProcessBase, IInfantEnrollment
    {
   #region "Constructor"
      public BInfantEnrollment()
        {
        }
        #endregion

  
      public DataSet GetExposedInfantByParentId(int Ptn_Pk)
      {
          lock (this)
          {
              ClsUtility.Init_Hashtable();
              ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
              ClsObject VisitManager = new ClsObject();
              return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetExposedInfantByParentId", ClsUtility.ObjectEnum.DataSet);
          }
      }
      public int DeleteExposedInfantById(int Id)
      {
          lock (this)
          {
              ClsUtility.Init_Hashtable();
              ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
              ClsObject VisitManager = new ClsObject();
              int theRowAffected = 0;
              theRowAffected = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteExposedInfantById", ClsUtility.ObjectEnum.ExecuteNonQuery);
              return theRowAffected;
          }
      }
      public int SaveExposedInfant(int Id, int Ptn_Pk, int ExposedInfantId, string FirstName, string LastName, DateTime DOB, string FeedingPractice3mos,
          string CTX2mos, string HIVTestType, string HIVResult, string FinalStatus, DateTime? DeathDate, int UserID)
      {
          int theRowAffected = 0;
          ClsObject VisitManager = new ClsObject();
          try
          {
              this.Connection = DataMgr.GetConnection();
              this.Transaction = DataMgr.BeginTransaction(this.Connection);
              
              VisitManager.Connection = this.Connection;
              VisitManager.Transaction = this.Transaction;
              ClsUtility.Init_Hashtable();
              
              ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
              ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
              ClsUtility.AddParameters("@ExposedInfantId", SqlDbType.Int, ExposedInfantId.ToString());
              ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, FirstName.ToString());
              ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, LastName.ToString());
              ClsUtility.AddParameters("@DOB", SqlDbType.DateTime, DOB.ToString());
              ClsUtility.AddParameters("@FeedingPractice3mos", SqlDbType.VarChar, FeedingPractice3mos.ToString());
              ClsUtility.AddParameters("@CTX2mos", SqlDbType.VarChar, CTX2mos.ToString());
              ClsUtility.AddParameters("@HIVResult", SqlDbType.VarChar, HIVResult.ToString());
              ClsUtility.AddParameters("@HIVTestType", SqlDbType.VarChar, HIVTestType.ToString());
              ClsUtility.AddParameters("@FinalStatus", SqlDbType.VarChar, FinalStatus.ToString());
              ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, UserID.ToString());
              if (DeathDate != null)
              {
                  ClsUtility.AddParameters("@DeathDate", SqlDbType.VarChar, DeathDate == null ? null : DeathDate.ToString());
              }
              theRowAffected = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveExposedInfant", ClsUtility.ObjectEnum.ExecuteNonQuery);
              if (theRowAffected == 0)
              {
                  MsgBuilder theMsg = new MsgBuilder();
                  theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                  AppException.Create("#C1", theMsg);

              }
              else
              {
                  DataMgr.CommitTransaction(this.Transaction);
                  DataMgr.ReleaseConnection(this.Connection);
              }
              
          }
          catch
          {
              DataMgr.RollBackTransation(this.Transaction);
              throw;
          }
          finally
          {
              VisitManager = null;
              if (this.Connection != null)
                  DataMgr.ReleaseConnection(this.Connection);

          }
         return theRowAffected;
          
      }
    
      public DataSet CheckIdentity(string ExposedInfantId)
      {
          lock (this)
          {
              ClsUtility.Init_Hashtable();
              ClsUtility.AddParameters("@ExposedInfantId", SqlDbType.Int, ExposedInfantId.ToString());
              ClsObject VisitManager = new ClsObject();
              return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_CheckIdentityInfant", ClsUtility.ObjectEnum.DataSet);
          }
      }
    }
}