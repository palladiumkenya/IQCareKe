using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IPatientClassification
    {
        DataTable SaveUpdatePatientClassification(int Ptn_pk, int LocationID, int Visit_pk, int ARTSponsorID, int UserId, DateTime DateEffective, int DeleteFlag);
        int DeletePatientClassification(int Ptn_pk, int ARTSponsorID, DateTime DateEffective);
        DataSet GetClassification(int SystemId);
        DataSet GetAllPatientClassificationData(int PatientId);
    }
}
