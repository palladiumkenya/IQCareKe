using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC
{
    public interface IMst_PatientInsert
    {
        int AddMstPatient(string firstName, string lastName, string middleName, int locationID, string patientEnrollmentID,
            int referredFrom, DateTime registrationDate, int sex, DateTime dob, int dobPrecision, int maritalStatus,
            string address, string phone, int userID, string posId, int moduleId, DateTime startDate, DateTime createDate);

        int AddOrdVisit(int ptnPk, int locationID, DateTime visitDate, int visitType, int userID, DateTime createDate, int moduleId);

        void UpdateBlueCardCCCNumber(int ptn_pk, string patientEnrollmentID);

        void UpdateBlueCardEnrollmentDate(int ptn_pk, DateTime enrollmentDate);
    }
}
