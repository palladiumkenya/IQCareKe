using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Interface.FormBuilder
{
    public interface ICareEndedConfiguration
    {
        DataSet GetCareEndedDetails();
        DataSet GetCareEndQuery(int ModuleId, int Published);
        DataSet GetCareEndedInfo(int FeatureId);
        DataTable SaveNewPatientExitReason(string theExitReason, Int32 theUserId, int theSystemId);
        int SaveCareEndDetails(DataSet dsCareEndDetail, Int32 FeatureId, string FeatureName, String SectionName, Int32 ModuleId, Int32 UserId, String CountryId, Int32 SectionId, DataTable dtconditionalFields, DataTable theDeathReason);
        int StatusUpdate(Hashtable ht);
        DataSet GetCareEndedDeathReason(int ModuleID);
        DataTable SaveNewPatientDeathReason(string theDeathReason, Int32 theUserId, int theSystemId);
        DataSet GetCareendConditionalFieldsDetails(Int32 ConfieldID, Int32 FeatureID);
    }
}
