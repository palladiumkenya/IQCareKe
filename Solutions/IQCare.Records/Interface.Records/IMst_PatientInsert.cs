using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records
{
    public interface IMst_PatientInsert
    {
        int AddMstPatient(string firstName, string lastName, string middleName, int locationID, string patientEnrollmentID,
            int referredFrom, DateTime registrationDate, int sex, DateTime dob, int dobPrecision, int maritalStatus,
            string address, string phone, int userID, string posId, int moduleId, DateTime startDate, DateTime createDate);

        void AddOrdVisit(int ptnPk, int locationID, DateTime visitDate, int visitType, int userID, DateTime createDate, int moduleId);
    }
}
