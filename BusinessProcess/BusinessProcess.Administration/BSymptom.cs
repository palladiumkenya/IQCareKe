using System;
using System.Data;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
namespace BusinessProcess.Administration
{
    public class BSymptom : ProcessBase,ISymptom
    {
        #region "Constructor"
        public BSymptom()
        {
        }
        #endregion


        public DataSet GetSymptom()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject SymptomManager = new ClsObject();
                return (DataSet)SymptomManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectSymptom_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewSymptom(int SymptomCategoryID, string SymptomName, int UserID)
        {
            try
            {
               
                ClsObject SymptomManager = new ClsObject();
               
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SymptomCategoryID", SqlDbType.Int, SymptomCategoryID.ToString());
                ClsUtility.AddParameters("@SymptomName", SqlDbType.VarChar, SymptomName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)SymptomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertSymptom_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Symptom record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                 SymptomManager = null;
                return Convert.ToInt32(theDR[0]);
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

        public int UpdateSymptom(int SymptomID,int SymptomCategoryID, string SymptomName, int UserID)
        {
            try
            {
              
                ClsObject SymptomManager = new ClsObject();
              
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SymptomCategoryID", SqlDbType.Int, SymptomCategoryID.ToString());
                ClsUtility.AddParameters("@SymptomName", SqlDbType.VarChar, SymptomName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@SymptomId", SqlDbType.Int, SymptomID.ToString());

                DataRow theDR;
                theDR = (DataRow)SymptomManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateSymptom_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Symptom record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                SymptomManager = null;
                return Convert.ToInt32(theDR[0]);
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
    }
}
