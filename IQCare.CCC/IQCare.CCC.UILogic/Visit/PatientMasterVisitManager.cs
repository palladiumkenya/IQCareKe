using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using System;

namespace IQCare.CCC.UILogic.Visit
{
    public class PatientMasterVisitManager
    {
       private readonly IPatientMasterVisitManager _patientMasterVisitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");
        private int _result=0;

        public int AddPatientMasterVisit(PatientMasterVisit patientMasterVisit)
        {
            try
            {
                return _result = _patientMasterVisitManager.AddPatientmasterVisit(patientMasterVisit);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int PatientMasterVisitCheckin(int patientId)
        {
            var objPpatientMasterVisit=new PatientMasterVisit
            {
                PatientId = patientId,
                ServiceId = 1,
                Start =Convert.ToDateTime(DateTime.Now.ToShortTimeString()),
                Status = 1

            };

            if (patientId > 0)
            {
                _result = _patientMasterVisitManager.PatienMasterVisitCheckin(patientId,objPpatientMasterVisit);
            }

            return _result> 0 ?_result:0;
        }

        public int PatientMasterVisitCheckout(int patientId,int visitSchedule,int visitBy,int visitType,DateTime visitDate )
        {
            var objPatientMasterVisit = new PatientMasterVisit
            {
                PatientId = patientId,
                Status = 2,
                VisitSchedule = visitSchedule,
                VisitBy = visitBy,
                VisitType = visitBy,
                VisitDate = visitDate
            };
            if (patientId > 0)
            {
                _result = _patientMasterVisitManager.UpdatePatientMasterVisit(objPatientMasterVisit);
            }
            return _result > 0 ? _result : 0;
        }
    }
}
