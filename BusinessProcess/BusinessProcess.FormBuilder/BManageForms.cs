using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using Application.Common;

namespace BusinessProcess.FormBuilder
{
    public class BManageForms : ProcessBase, IManageForms
    {
        public DataSet GetPublishedModuleList()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_GetPublishedModuleList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFormDetail(string strFormStatus, Int32 CountryId)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FormStatus", SqlDbType.VarChar, strFormStatus);
                ClsUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryId.ToString());
                return (DataSet)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ManageForm_GetPatientRegistrationFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFormDetail(string strFormStatus,string strTechArea,Int32 CountryId)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FormStatus", SqlDbType.VarChar, strFormStatus);
                ClsUtility.AddParameters("@TechArea", SqlDbType.VarChar, strTechArea);
                ClsUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryId.ToString());
                return (DataSet)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ManageForm_GetFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet CheckFormDetail(string strFormName,Int32 iFormId)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, strFormName);
                ClsUtility.AddParameters("@FormId", SqlDbType.VarChar, iFormId.ToString());
                return (DataSet)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ManageForm_CheckFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public void DeleteFormTableDetail(string strFormName,Int32 iFormId)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, strFormName);
                ClsUtility.AddParameters("@FormId", SqlDbType.VarChar, iFormId.ToString());
                FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ManageForm_DeleteFormTableDetail_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
         }
        public int ResetFormStatus(Int32 iFormId, string strValue, Int32 iUserID)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                ClsUtility.Init_Hashtable();
                int theRowAffected = 0;
                ClsUtility.AddParameters("@FormId", SqlDbType.VarChar, iFormId.ToString());
                ClsUtility.AddParameters("@FormValue", SqlDbType.VarChar, strValue);
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserID.ToString());
                theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ManageForm_ResetFormStatus_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                return theRowAffected;
            }
        }

        



    }
}
