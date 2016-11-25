using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.SCM;
using Application.Common;
using System.Collections;

namespace BusinessProcess.SCM
{
    class BLaboratory:ProcessBase, ILaboratory
    {
        #region "Constructor"
        public BLaboratory()
        {
        }
        #endregion

        public DataTable GetLabList(int labTestId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject Lablist = new ClsObject();
                ClsUtility.AddParameters("@labTestId", SqlDbType.Int, labTestId.ToString());
                return (DataTable)Lablist.ReturnObject(ClsUtility.theParams, "pr_SCM_GetLaboratoryList_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetLabLocationList()
        {
            lock (this)
            {
                ClsObject LabLocation = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataTable)LabLocation.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_GetLabTestLocationList_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public int SaveUpdateLabConfiguration(DataTable dtLabConfig,int UserId)
        {
            ClsObject ItemList = new ClsObject();
            int theRowAffected = 0;
            foreach (DataRow theDR in dtLabConfig.Rows)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SubTestId", SqlDbType.Int, theDR["SubTestId"].ToString());
                ClsUtility.AddParameters("@SubTestname", SqlDbType.VarChar, theDR["SubTestname"].ToString());
                ClsUtility.AddParameters("@Loinccode", SqlDbType.VarChar, theDR["Lionccode"].ToString());
                ClsUtility.AddParameters("@TestLocation", SqlDbType.Int, theDR["TestLocation"].ToString());
                ClsUtility.AddParameters("@EffectiveDate", SqlDbType.DateTime, theDR["EffectiveDate"].ToString());
                ClsUtility.AddParameters("@TestCostPrice", SqlDbType.Float, theDR["TestCostPrice"].ToString());
                ClsUtility.AddParameters("@TestMargin", SqlDbType.Float, theDR["TestMargin"].ToString());
                ClsUtility.AddParameters("@TestSellingPrice", SqlDbType.Float, theDR["TestSellingPrice"].ToString());
                ClsUtility.AddParameters("@OutsrcLocation", SqlDbType.VarChar, theDR["OutsrcLocation"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.VarChar, theDR["Status"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                theRowAffected = (int)ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveUpdateLabConfigration_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
            return theRowAffected;
        }
    }
}
