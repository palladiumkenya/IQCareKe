using System.Collections.Generic;
using Application.Presentation;
using Interface.CCC;
using Entities.PatientCore;
using System;

namespace IQCare.CCC.UILogic
{
    public class PersonOvcStatusManager
    {
        private IPatientOvcStatusmanager _mgr = (IPatientOvcStatusmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.PatientOvcStatusManager, BusinessProcess.CCC");
        private int _result;

        public int AddPatientOvcStatus(int personid, int guardianId, Boolean orphan, Boolean inSchool, int userId)
        {
            PatientOVCStatus patientOvcStatus = new PatientOVCStatus()
            {
                PersonId = personid,
                GuardianId = guardianId,
                Orphan = orphan,
                InSchool = inSchool,
                Active = true,
                CreatedBy = userId
            };
            return _result=  _mgr.AddPatientOvcStatus(patientOvcStatus);
        }

        public int DeletePatientOvcStatus(int id)
        {
          return _result= _mgr.DeletePatientOvcStatus(id);
        }

        public List<PatientOVCStatus> GetPatientOvcStatus(int id)
        {
            var myList = _mgr.GetPatientOvcStatus(id);
            return myList;
        }

        public int UpdatePatientOvcStatus(bool orphan, bool inschool)
        {
            PatientOVCStatus patientOvcStatus=new PatientOVCStatus()
            {
                Orphan = orphan,
                InSchool = inschool
            };

          return _result= _mgr.UpdatePatientOvcStatus(patientOvcStatus);
        }
    }
}
