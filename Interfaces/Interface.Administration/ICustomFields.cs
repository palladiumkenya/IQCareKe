using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Interface.Administration
{
    public interface ICustomFields
    {
        DataSet GetFeatures(int SystemId, string ModuleId);
        DataSet GetCustomFields(int SystemId);
        DataSet GetCustomFieldsUnits(int CustomFieldID);
        DataSet GetCustomListName(string CodeName);
        int SaveCustomFields(string Lblfield, string lbldesc, int FeatureID, int SectionID, int ControlID, int UserID, int UnitFlag, int MinValue, int MaxValue, string UnitsNum, int CodeID, string DataType, string OldLabel, int multiSelect, int iSize, string decodeValues, string deleteValues, int SystemId,int rowcount);
        int UpdateCustomFields(string Lblfield,string lbldesc, int FeatureID, int SectionID, int ControlID, int UserId, int CustomFieldID, int UnitFlag, int MinValue, int MaxValue, string UnitsNum);
        int DeleteCustomFields(int CustomFieldID, int DFlag);
        DataSet SaveCodeDecode(string Name, string DName,int SRNO, int UserId);
        //Custom fields implement form page 
        DataSet GetCustomList(int CodeID);
        DataSet GetCustomFieldListforAForm(int FormID);
        DataSet GetDecodeValues(Int32 codeId);
        int SaveCustomFieldValues(string sqlstr);
        int SaveUpdateCustomFieldValues(string[] sqlstr);
        //DataSet GetCustomFieldValues(string tablename, string fields, Int32 ptnID, Int32 visitID, Int32 labID, Int32 ptn_pharmacy_pk, Int32 FeatureID);
        DataSet GetCustomFieldValues(string TableName, string fields, Int32 ptnID, Int32 HomeVisitId, Int32 visitpk, Int32 labID, Int32 ptn_pharmacy_pk, Int32 FeatureID);
        //Patient Visit ID, Date
        DataSet GetPatientVisit(Int32 ptnID, Int32 locationID,int visitType);
        DataSet GetVisit(int VisitId);
        int RearrangeCustomFields(DataTable dtCustomFields, int SystemId);
        DataSet GetRearrangeCustomFields(int SystemId);
    }
}
