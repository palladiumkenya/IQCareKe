using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IinitialFollowupVisit
    {
        DataSet GetInitialFollowupVisitData(int patientID, int locationID);
        DataSet SaveUpdateInitialFollowupVisitData(Hashtable hashTable, DataSet dataSet, bool isUpdate, DataTable theCustomDataDT);
        int DeleteInitialFollowupVisitForm(string FormName, int OrderNo, int PatientId, int UserID);
        DataSet GetInitialFollowupVisitInfo(int patientID, int locationID, int visitID);
        DataSet GetExistInitialFollowupVisitbydate(int PatientID, DateTime VisitdByDate, int locationID);
        /// <summary>
        /// Gets the z score values.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        DataSet GetZScoreValues(int PatientID, string gender, string height);
    }
}
