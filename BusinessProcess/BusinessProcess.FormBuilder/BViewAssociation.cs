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
    public class BViewAssociation : ProcessBase, IViewAssociation    
    {
        public DataSet GetViewAssociationFields(string FieldName,int ModuleId)
        {
            lock (this)
            {
                ClsObject Fields = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, FieldName);
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                return (DataSet)Fields.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetCustomFieldsViewAssociation_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetMoudleName()
        {
            lock (this)
            {
                ClsObject TechArea = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)TechArea.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_GetMasters_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
    }
}
