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
     public class BRptFieldValidations : ProcessBase, IRptFieldValidations
    {
         public DataSet GetRptFieldDetails()
        {
            lock (this)
            {
                ClsObject RptField = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)RptField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetCareEndedForm_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

         public DataTable ReturnDatatableQueryResult(string theQuery)
         {
             lock (this)
             {
                 ClsObject theQB = new ClsObject();
                 ClsUtility.Init_Hashtable();
                 ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                 return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_General_SQLTable_Parse", ClsUtility.ObjectEnum.DataTable);
             }
         }

         public DataTable ParseXml(string stringxml,int FacilityId,int ModuleId)
         {
             lock (this)
             {
                 ClsObject theQB = new ClsObject();
                 ClsUtility.Init_Hashtable();
                 ClsUtility.AddParameters("@data", SqlDbType.Xml, stringxml);
                 ClsUtility.AddParameters("@FaciltyId", SqlDbType.Int, FacilityId.ToString());
                 ClsUtility.AddParameters("@SelectModuleId", SqlDbType.Int, ModuleId.ToString());
                 return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_General_ProcessXml", ClsUtility.ObjectEnum.DataTable);
             }
         }


    }
}
