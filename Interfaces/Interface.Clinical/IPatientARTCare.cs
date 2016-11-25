using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Interface.Clinical
{
    public interface IPatientARTCare
    {
        DataSet GetPatientARTCare(int patientid, int LocationId);
        int Save_Update_ARTCare(int patientID, int VisitID, int LocationID, Hashtable ht, int userID, int DataQualityFlag, DataTable theCustomDataDT);

        #region "Kenya Technical Area"
        //*************//
        //john macharia start
        DataSet GetPatientARVTherapy(int patientid, int LocationId);
        int Save_Update_ARVTherapy(int patientID, int VisitID, int LocationID, Hashtable ht, int userID, int DataQualityFlag, DataTable theCustomFieldData);
        //john end


        #endregion
    }
}
