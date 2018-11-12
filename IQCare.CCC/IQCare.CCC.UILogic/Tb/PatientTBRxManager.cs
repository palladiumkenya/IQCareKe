using System;
using Application.Presentation;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;
using IQCare.Web.UILogic;

namespace IQCare.CCC.UILogic.Tb
{
    public class PatientTBRxManager
    {
        private IPatientTBRx _patientTBRx = (IPatientTBRx)ObjectFactory.CreateInstance("BusinessProcess.CCC.Tb.BPatientTBRx, BusinessProcess.CCC");
        public int AddPatientTBRx(PatientTBRx p)
        {
            PatientTBRx patientTBRx = new PatientTBRx()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                TBRxStartDate = p.TBRxStartDate,
                TBRxEndDate = p.TBRxEndDate,
                RegimenId = p.RegimenId,
                CreatedBy = p.CreatedBy
            };
            return _patientTBRx.AddPatientTBRx(patientTBRx);
        }
        public List<PatientTBRx> GetByPatientId(int patientId)
        {
            var patientTBRx = _patientTBRx.GetByPatientId(patientId);
            return patientTBRx;
        }
        public int UpdatePatientTBRx(PatientTBRx p)
        {
            PatientTBRx patientTBRx = new PatientTBRx()
            {
                Id = p.Id,
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                TBRxStartDate = p.TBRxStartDate,
                TBRxEndDate = p.TBRxEndDate,
                RegimenId = p.RegimenId,
                CreatedBy = p.CreatedBy
            };
            return _patientTBRx.UpdatePatientTBRx(patientTBRx);
        }
    }
}
