using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.ModelBinding;

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

        public List<PatientMasterVisit> GetByDate(DateTime date)
        {
            try
            {
                return _patientMasterVisitManager.GetByDate(date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int PatientMasterVisitCheckin(int patientId,int userId)
        {


            var objPpatientMasterVisit=new PatientMasterVisit
            {
                PatientId = patientId,
                ServiceId = 1,
                Status = 1,
                Start = DateTime.Now,
                VisitDate = DateTime.Now,
                CreatedBy =userId
            };

            if (patientId > 0)
            {
                _result = _patientMasterVisitManager.PatientMasterVisitCheckin(patientId,objPpatientMasterVisit);
            }

            return _result> 0 ?_result:0;
        }

        public int PatientMasterVisitCheckout(int id,int patientId, int visitSchedule, int visitBy, int visitType, DateTime visitDate)
        {
            _result = _patientMasterVisitManager.PatientMasterVisitCheckout(patientId, id,visitSchedule,visitBy,visitType,visitDate);
            return _result > 0 ? _result : 0;
        }
    }
}
