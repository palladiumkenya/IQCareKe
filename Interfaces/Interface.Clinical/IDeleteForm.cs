using System;
using System.Data;

    namespace Interface.Clinical
    {
    public interface IDeleteForm
    {
        DataSet GetPatientForms(int PatientId);
        int DeletePatientForms(DataTable theDT, int PatientId);
     }
    }
