using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class MstPatientLogic
    {
        private IMst_PatientInsert _mgr =
            (IMst_PatientInsert)
            ObjectFactory.CreateInstance("BusinessProcess.CCC.BMstPatientInsert, BusinessProcess.CCC");

        public int InsertMstPatient(string firstName, string lastName, string middleName, int locationId, string patientEnrollmentId,
            int referredFrom, DateTime registrationDate, int sex, DateTime dob, int dobPrecision, int maritalStatus, string address,
            string phone, int userId, string posId, int moduleId, DateTime startDate, DateTime createDate)
        {
            return _mgr.AddMstPatient(firstName, lastName , middleName , locationId, patientEnrollmentId,
                referredFrom, registrationDate, sex, dob, dobPrecision, maritalStatus, address, phone, userId, posId,
                moduleId, startDate, createDate);
        }

        public int AddOrdVisit(int ptnPk, int locationId, DateTime visitDate, int visitType, int userId, DateTime createDate, int moduleId)
        {
            return _mgr.AddOrdVisit(ptnPk, locationId, visitDate, visitType, userId, createDate, moduleId);
        }

        public void UpdateBlueCardCCCNumber(int ptn_pk, string patientEnrollmentID)
        {
            _mgr.UpdateBlueCardCCCNumber(ptn_pk, patientEnrollmentID);
        }
    }
}
