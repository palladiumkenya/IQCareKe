using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient; 
namespace Interface.Administration
{
    public interface IDeletePatient
    {
        //DataTable GetPatientDetails(int flag,string Id);
        //int DeletePatientForms(DataTable theDT, int PatientId, int DeleteFlag);
        //DataTable GetPatientDetails(string theEnrollID);
        DataTable GetPatientDetails(int PatientID);
        int DeletePatient(int PatientId, int UserID);
        //DataTable GetPatientDetailsByEnrollment(string theEnrollID);

    }
}
