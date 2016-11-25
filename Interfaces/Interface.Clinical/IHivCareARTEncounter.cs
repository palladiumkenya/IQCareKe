using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IHivCareARTEncounter
    {
        //HIVCare ART Encounter Section
        DataSet GetHIVCareARTPatientFormData(int patientID, int locationID);
        DataSet GetHIVCareARTPatientVisitInfo(int patientID, int locationID, int visitID);
        DataSet SaveUpdateHIVCareARTPatientVisit(Hashtable hashTable, DataSet dataSet, bool isUpdate,DataTable theCustomDataDT);
        int DeleteHIVCareEncounterForms(string FormName, int OrderNo, int PatientId, int userID);
        DataSet GetExistHIVArtCareEncounterbydate(int PatientID, DateTime VisitdByDate, int locationID);
    }
}
