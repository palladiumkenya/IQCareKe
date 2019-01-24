using Application.Presentation;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic
{
    public class MstPatientLogic
    {
        private IMst_PatientInsert _mgr =
            (IMst_PatientInsert)
            ObjectFactory.CreateInstance("BusinessProcess.Records.BMstPatientInsert, BusinessProcess.Records");

        public int InsertMstPatient(string firstName, string lastName, string middleName, int locationId, string patientEnrollmentId,
            int referredFrom, DateTime registrationDate, int sex, DateTime dob, int dobPrecision, int maritalStatus, string address,
            string phone, int userId, string posId, int moduleId, DateTime startDate, DateTime createDate)
        {
            return _mgr.AddMstPatient(firstName, lastName, middleName, locationId, patientEnrollmentId,
                referredFrom, registrationDate, sex, dob, dobPrecision, maritalStatus, address, phone, userId, posId,
                moduleId, startDate, createDate);
        }

        public void AddOrdVisit(int ptnPk, int locationId, DateTime visitDate, int visitType, int userId, DateTime createDate, int moduleId)
        {
            _mgr.AddOrdVisit(ptnPk, locationId, visitDate, visitType, userId, createDate, moduleId);
        }
    }
}
