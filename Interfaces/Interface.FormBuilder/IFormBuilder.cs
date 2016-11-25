using System;
using System.Data;

namespace Interface.FormBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public interface  IFormBuilder
    {
        /// <summary>
        /// Retrieves the maximum identifier.
        /// </summary>
        /// <param name="strSearchIn">The string search in.</param>
        /// <returns></returns>
        int RetrieveMaxId(String strSearchIn);
        /// <summary>
        /// Saves the form detail.
        /// </summary>
        /// <param name="dsSaveFormData">The ds save form data.</param>
        /// <param name="dtFieldDetails">The dt field details.</param>
        /// <returns></returns>
        int SaveFormDetail(DataSet dsSaveFormData, DataTable dtFieldDetails);
        /// <summary>
        /// Saves the form detail.
        /// </summary>
        /// <param name="dsSaveFormData">The ds save form data.</param>
        /// <param name="dtFieldDetails">The dt field details.</param>
        /// <param name="dtBusinessRules">The dt business rules.</param>
        /// <returns></returns>
        int SaveFormDetail(DataSet dsSaveFormData, DataTable dtFieldDetails,DataTable dtBusinessRules);
        /// <summary>
        /// Updates the form detail seq.
        /// </summary>
        /// <param name="dtFieldDetails">The dt field details.</param>
        /// <returns></returns>
        int UpdateFormDetailSeq(DataTable dtFieldDetails);
        /// <summary>
        /// Checks the duplicate.
        /// </summary>
        /// <param name="strSearchTable">The string search table.</param>
        /// <param name="strSearchColumn">The string search column.</param>
        /// <param name="strSearchValue">The string search value.</param>
        /// <param name="iDeleteFlagCheck">The i delete flag check.</param>
        /// <param name="iModuleId">The i module identifier.</param>
        /// <returns></returns>
        bool CheckDuplicate(string strSearchTable, string strSearchColumn, string strSearchValue, String iDeleteFlagCheck, int iModuleId);
        /// <summary>
        /// Checks the duplicate.
        /// </summary>
        /// <param name="strSearchTable">The string search table.</param>
        /// <param name="strSearchColumn1">The string search column1.</param>
        /// <param name="strSearchValue1">The string search value1.</param>
        /// <param name="strSearchColumn2">The string search column2.</param>
        /// <param name="strSearchValue2">The string search value2.</param>
        /// <param name="iDeleteFlagCheck">The i delete flag check.</param>
        /// <param name="iModuleId">The i module identifier.</param>
        /// <returns></returns>
        bool CheckDuplicate(string strSearchTable, string strSearchColumn1, string strSearchValue1, string strSearchColumn2, string strSearchValue2, String iDeleteFlagCheck, int iModuleId);
        /// <summary>
        /// Gets the form detail.
        /// </summary>
        /// <param name="iFeatureId">The i feature identifier.</param>
        /// <returns></returns>
        DataSet GetFormDetail(Int32 iFeatureId);
        /// <summary>
        /// Saves the custom registration form detail.
        /// </summary>
        /// <param name="dsSaveFormData">The ds save form data.</param>
        /// <param name="dtFieldDetails">The dt field details.</param>
        /// <returns></returns>
        int SaveCustomRegistrationFormDetail(DataSet dsSaveFormData, DataTable dtFieldDetails);
    }
}
