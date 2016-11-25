using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomForm
    {
        /// <summary>
        /// Validates the specified form name.
        /// </summary>
        /// <param name="FormName">Name of the form.</param>
        /// <param name="Date">The date.</param>
        /// <param name="PatientId">The patient identifier.</param>
        /// <returns></returns>
        DataSet Validate(string formName, string date, int pateintId, int moduleId);
        /// <summary>
        /// Gets the name of the form.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="countryID">The country identifier.</param>
        /// <returns></returns>
        DataSet GetFormName(int moduleId, int countryId);
        /// <summary>
        /// Gets the field name_and_ label.
        /// </summary>
        /// <param name="FeatureId">The feature identifier.</param>
        /// <param name="PatientId">The patient identifier.</param>
        /// <returns></returns>
        DataSet GetFieldName_and_Label(int featureId, int patientId);
        /// <summary>
        /// Common_s the get save update.
        /// </summary>
        /// <param name="Insert">The insert.</param>
        /// <returns></returns>
        DataSet Common_GetSaveUpdate(string insert);
        /// <summary>
        /// Gets the PMTCT decode table.
        /// </summary>
        /// <param name="CodeID">The code identifier.</param>
        /// <returns></returns>
        DataSet GetPmtctDecodeTable(string codeId);
        /// <summary>
        /// Saves the update.
        /// </summary>
        /// <param name="Insert">The insert.</param>
        /// <param name="DS">The ds.</param>
        /// <param name="TabId">The tab identifier.</param>
        /// <returns></returns>
        DataSet SaveUpdate(String insert, DataSet ds, int tabId);
        /// <summary>
        /// Deletes the form.
        /// </summary>
        /// <param name="FormName">Name of the form.</param>
        /// <param name="VisitID">The visit identifier.</param>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        int DeleteForm(string formName, int visitId, int patientId, int userId);
        /// <summary>
        /// Gets the system time.
        /// </summary>
        /// <param name="Format">The format.</param>
        /// <returns></returns>
        String GetSystemTime(int format);

    }

}
