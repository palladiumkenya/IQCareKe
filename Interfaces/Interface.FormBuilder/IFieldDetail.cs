using System;
using System.Data;
using System.Collections;

namespace Interface.FormBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFieldDetail 
    {
        /// <summary>
        /// Gets the type of the drug.
        /// </summary>
        /// <returns></returns>
      DataSet GetDrugType();
      /// <summary>
      /// Gets the business rule.
      /// </summary>
      /// <returns></returns>
      DataSet GetBusinessRule();
      /// <summary>
      /// Gets the custom fields.
      /// </summary>
      /// <param name="strFieldName">Name of the string field.</param>
      /// <param name="iModuleId">The i module identifier.</param>
      /// <param name="flag">The flag.</param>
      /// <returns></returns>
      DataSet GetCustomFields(string strFieldName, int iModuleId, int flag);
      /// <summary>
      /// Gets the custom fields.
      /// </summary>
      /// <param name="strFieldName">Name of the string field.</param>
      /// <param name="iModuleId">The i module identifier.</param>
      /// <param name="flag">The flag.</param>
      /// <param name="isgridview">The isgridview.</param>
      /// <returns></returns>
      DataSet GetCustomFields(string strFieldName, int iModuleId, int flag ,int isgridview);
      /// <summary>
      /// Resets the custom field rules.
      /// </summary>
      /// <param name="fieldID">The field identifier.</param>
      /// <param name="flag">The flag.</param>
      /// <param name="predefine">The predefine.</param>
      /// <param name="FieldName">Name of the field.</param>
      /// <returns></returns>
      int ResetCustomFieldRules(int fieldID, int flag, int predefine, string FieldName);
      /// <summary>
      /// Checks the predefine field.
      /// </summary>
      /// <param name="fieldID">The field identifier.</param>
      /// <returns></returns>
      DataSet CheckPredefineField(int fieldID);
      /// <summary>
      /// Checks the custom fields.
      /// </summary>
      /// <param name="fieldID">The field identifier.</param>
      /// <returns></returns>
      DataSet CheckCustomFields(int fieldID);
      /// <summary>
      /// Deletes the custom field.
      /// </summary>
      /// <param name="fieldID">The field identifier.</param>
      /// <param name="flag">The flag.</param>
      /// <returns></returns>
      int DeleteCustomField(int fieldID, int flag);
      /// <summary>
      /// Gets the duplicate custom fields.
      /// </summary>
      /// <param name="id">The identifier.</param>
      /// <param name="fieldName">Name of the field.</param>
      /// <param name="ModuleId">The module identifier.</param>
      /// <param name="flag">The flag.</param>
      /// <returns></returns>
      DataSet GetDuplicateCustomFields(int id, string fieldName, int ModuleId, int flag);
      /// <summary>
      /// Saves the update cusomt field.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <param name="FieldName">Name of the field.</param>
      /// <param name="ControlID">The control identifier.</param>
      /// <param name="DeleteFlag">The delete flag.</param>
      /// <param name="UserID">The user identifier.</param>
      /// <param name="CareEnd">The care end.</param>
      /// <param name="flag">The flag.</param>
      /// <param name="SelectList">The select list.</param>
      /// <param name="business">The business.</param>
      /// <param name="Predefined">The predefined.</param>
      /// <param name="SystemID">The system identifier.</param>
      /// <param name="dtconditionalFields">The dtconditional fields.</param>
      /// <param name="dtICD10Fields">The dt ic D10 fields.</param>
      /// <returns></returns>
      int SaveUpdateCusomtField(int ID, string FieldName, int ControlID, int DeleteFlag, int UserID, int CareEnd, int flag, 
          string SelectList,DataTable business, int Predefined, int SystemID, DataTable dtconditionalFields, DataTable dtICD10Fields);
      /// <summary>
      /// Gets the conditional fieldslist.
      /// </summary>
      /// <param name="Codeid">The codeid.</param>
      /// <param name="FID">The fid.</param>
      /// <param name="flag">The flag.</param>
      /// <returns></returns>
      DataSet GetConditionalFieldslist(Int32 Codeid, int FID, int flag);
      /// <summary>
      /// Gets the conditional fields details.
      /// </summary>
      /// <param name="ConfieldID">The confield identifier.</param>
      /// <param name="CareEndconFlag">The care endcon flag.</param>
      /// <returns></returns>
      DataSet GetConditionalFieldsDetails(Int32 ConfieldID, Int32 CareEndconFlag);
      //int SaveConditionalFields(DataTable dtconditionalFields);
      /// <summary>
      /// Saves the mod de code.
      /// </summary>
      /// <param name="dtModDeCode">The dt mod de code.</param>
      /// <returns></returns>
      int SaveModDeCode(DataTable dtModDeCode);
      /// <summary>
      /// Gets the icd list.
      /// </summary>
      /// <returns></returns>
      DataSet GetICDList();
      /// <summary>
      /// Gets the ic D10 values.
      /// </summary>
      /// <param name="FieldId">The field identifier.</param>
      /// <returns></returns>
      DataSet GetICD10Values(int FieldId);

      DataTable GetFieldLookUpValues(int fieldId, bool predefined, int systemId);

      DataTable GetFieldControlTypes();
    }
}
